﻿using System;
using Android.Views;
using Android.Views.InputMethods;
using AndroidX.AppCompat.Widget;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Maui.Handlers
{
	public partial class EditorHandler : AbstractViewHandler<IEditor, AppCompatEditText>
	{
		protected override AppCompatEditText CreateNativeView()
		{
			var editText = new AppCompatEditText(Context)
			{
				ImeOptions = ImeAction.Done
			};

			editText.SetSingleLine(false);
			editText.Gravity = GravityFlags.Top;
			editText.TextAlignment = Android.Views.TextAlignment.ViewStart;
			editText.SetHorizontallyScrolling(false);

			return editText;
		}

		public static void MapText(EditorHandler handler, IEditor editor)
		{
			handler.TypedNativeView?.UpdateText(editor);
		}

		public static void MapCharacterSpacing(EditorHandler handler, IEditor editor)
		{
			handler.TypedNativeView?.UpdateCharacterSpacing(editor);
		}

		public static void MapMaxLength(EditorHandler handler, IEditor editor)
		{
			handler.TypedNativeView?.UpdateMaxLength(editor);
		}

		public static void MapIsTextPredictionEnabled(EditorHandler handler, IEditor editor)
		{
			handler.TypedNativeView?.UpdateIsTextPredictionEnabled(editor);
		}
		
		public static void MapFont(EditorHandler handler, IEditor editor)
		{
			var services = handler.Services
				   ?? throw new InvalidOperationException($"Unable to find service provider, the handler.Services was null.");
			var fontManager = services.GetRequiredService<IFontManager>();

			handler.TypedNativeView?.UpdateFont(editor, fontManager);
		}
	}
}