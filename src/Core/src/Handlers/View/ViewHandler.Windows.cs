#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using PlatformView = Microsoft.UI.Xaml.FrameworkElement;

namespace Microsoft.Maui.Handlers
{
	public partial class ViewHandler
	{
		partial void ConnectingHandler(PlatformView? platformView)
		{
			if (platformView != null)
			{
				platformView.GotFocus += OnPlatformViewGotFocus;
				platformView.LostFocus += OnPlatformViewLostFocus;

				//if (platformView is UIElement uiElement)
				//{
				//	if (VirtualView is IContextActionContainer contextActionContainer)
				//	{
				//		if (contextActionContainer.ContextActions?.Any() == true)
				//		{
				//			var newFlyout = new MenuFlyout();
				//			AddMenuItems(contextActionContainer.ContextActions, newFlyout.Items.Add);
				//			uiElement.ContextFlyout = newFlyout;
				//		}
				//	}
				//}
			}
		}

		//void AddMenuItems(IList<IMenuElement> menuItems, Action<MenuFlyoutItemBase> addMenuItem)
		//{
		//	foreach (var menuItem in menuItems)
		//	{
		//		var platformThing = menuItem.ToPlatform();
		//		addMenuItem(platformThing);
		//	}
		//}

		//private void UpdateNativeMenuItem(IMenuElement source, MenuFlyoutItem destination)
		//{
		//	// TODO: Respect these settings too, if possible
		//	//menuItem.Font

		//	destination.CharacterSpacing = source.CharacterSpacing.ToEm();
		//	destination.IsEnabled = source.IsEnabled;
		//	destination.Text = source.Text;
		//	destination.Icon = source.Source?.ToIconSource(MauiContext!)?.CreateIconElement();

		//	// TODO: How to expose this platform-specific property for Windows only? Maybe something like this: https://docs.microsoft.com/dotnet/maui/windows/platform-specifics/listview-selectionmode
		//	destination.KeyboardAccelerators.Add(
		//		new UI.Xaml.Input.KeyboardAccelerator
		//		{
		//			Modifiers = global::Windows.System.VirtualKeyModifiers.Control,
		//			Key = (global::Windows.System.VirtualKey)(char.ToUpperInvariant(source.Text[0])),
		//		});
		//}

		partial void DisconnectingHandler(PlatformView platformView)
		{
			UpdateIsFocused(false);

			platformView.GotFocus -= OnPlatformViewGotFocus;
			platformView.LostFocus -= OnPlatformViewLostFocus;
		}

		static partial void MappingFrame(IViewHandler handler, IView view)
		{
			// Both Clip and Shadow depend on the Control size.
			handler.ToPlatform().UpdateClip(view);
			handler.ToPlatform().UpdateShadow(view);
		}

		public static void MapTranslationX(IViewHandler handler, IView view)
		{
			handler.ToPlatform().UpdateTransformation(view);
		}

		public static void MapTranslationY(IViewHandler handler, IView view)
		{
			handler.ToPlatform().UpdateTransformation(view);
		}

		public static void MapScale(IViewHandler handler, IView view)
		{
			handler.ToPlatform().UpdateTransformation(view);
		}

		public static void MapScaleX(IViewHandler handler, IView view)
		{
			handler.ToPlatform().UpdateTransformation(view);
		}

		public static void MapScaleY(IViewHandler handler, IView view)
		{
			handler.ToPlatform().UpdateTransformation(view);
		}

		public static void MapRotation(IViewHandler handler, IView view)
		{
			handler.ToPlatform().UpdateTransformation(view);
		}

		public static void MapRotationX(IViewHandler handler, IView view)
		{
			handler.ToPlatform().UpdateTransformation(view);
		}

		public static void MapRotationY(IViewHandler handler, IView view)
		{
			handler.ToPlatform().UpdateTransformation(view);
		}

		public static void MapAnchorX(IViewHandler handler, IView view)
		{
			handler.ToPlatform().UpdateTransformation(view);
		}

		public static void MapAnchorY(IViewHandler handler, IView view)
		{
			handler.ToPlatform().UpdateTransformation(view);
		}

		public static void MapToolbar(IViewHandler handler, IView view)
		{
			if (view is IToolbarElement tb)
				MapToolbar(handler, tb);
		}

		internal static void MapToolbar(IElementHandler handler, IToolbarElement toolbarElement)
		{
			_ = handler.MauiContext ?? throw new InvalidOperationException($"{nameof(handler.MauiContext)} null");

			if (toolbarElement.Toolbar != null)
			{
				var toolBar = toolbarElement.Toolbar.ToPlatform(handler.MauiContext);
				handler.MauiContext.GetNavigationRootManager().SetToolbar(toolBar);
			}
		}

		public static void MapContextFlyout(IViewHandler handler, IView view)
		{
			if (view is IContextFlyoutContainer contextFlyoutContainer)
			{
				MapContextFlyout(handler, contextFlyoutContainer);
			}
		}

		internal static void MapContextFlyout(IElementHandler handler, IContextFlyoutContainer contextFlyoutContainer)
		{
			_ = handler.MauiContext ?? throw new InvalidOperationException($"The handler's {nameof(handler.MauiContext)} cannot be null.");

			if (contextFlyoutContainer.ContextFlyout != null)
			{
				// This will set the MauiContext and get everything created first
				var handler2 = contextFlyoutContainer.ContextFlyout.ToHandler(handler.MauiContext);


				//var platformView = contextFlyoutContainer.ContextFlyout.ToPlatform() ?? throw new InvalidOperationException($"Unable to convert view to {typeof(PlatformView)}");

				object? o;
				if (contextFlyoutContainer.ContextFlyout is IReplaceableView replaceableView && replaceableView.ReplacedView != contextFlyoutContainer.ContextFlyout)
					o = replaceableView.ReplacedView.ToPlatform();


				_ = contextFlyoutContainer.ContextFlyout.Handler ?? throw new InvalidOperationException($"{nameof(MauiContext)} should have been set on parent.");

				if (contextFlyoutContainer.ContextFlyout.Handler is IViewHandler viewHandler)
				{
					if (viewHandler.ContainerView is PlatformView containerView)
						o = containerView;

					if (viewHandler.PlatformView is PlatformView platformView)
						o = platformView;
				}

				o = contextFlyoutContainer.ContextFlyout.Handler?.PlatformView;

				if (handler.PlatformView is Microsoft.UI.Xaml.UIElement uiElement && o is FlyoutBase flyoutBase)
				{
					uiElement.ContextFlyout = flyoutBase;
				}
			}
		}

		public override void SetVirtualView(IElement view)
		{
			base.SetVirtualView(view);

			var contextContainer = (IContextFlyoutContainer)view;

			// TODO: From MenuBarHandler.Windows.cs - what do we need here?

			//foreach (var item in ((IMenuBar)view))
			//{
			//	Add(item);
			//}
		}


		public virtual bool NeedsContainer
		{
			get
			{
				if (VirtualView is IBorderView border)
					return border?.Shape != null || border?.Stroke != null;

				return false;
			}
		}

		void OnPlatformViewGotFocus(object sender, RoutedEventArgs args)
		{
			UpdateIsFocused(true);
		}

		void OnPlatformViewLostFocus(object sender, RoutedEventArgs args)
		{
			UpdateIsFocused(false);
		}

		void UpdateIsFocused(bool isFocused)
		{
			if (VirtualView == null)
				return;

			bool updateIsFocused = (isFocused && !VirtualView.IsFocused) || (!isFocused && VirtualView.IsFocused);

			if (updateIsFocused)
				VirtualView.IsFocused = isFocused;
		}
	}
}