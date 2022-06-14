﻿#if __IOS__ || MACCATALYST
using PlatformView = MapKit.MKMapView;
#elif MONOANDROID
using PlatformView = Android.Gms.Maps.MapView;
#elif WINDOWS
using PlatformView = Microsoft.UI.Xaml.Controls.WebView2;
#elif TIZEN
using PlatformView = System.Object;
#elif (NETSTANDARD || !PLATFORM) || (NET6_0 && !IOS && !ANDROID && !TIZEN)
using PlatformView = System.Object;
#endif

namespace Microsoft.Maui.Handlers
{
	public partial class MapHandler : IMapHandler
	{
		public static IPropertyMapper<IMap, IMapHandler> Mapper = new PropertyMapper<IMap, IMapHandler>(ViewHandler.ViewMapper)
		{
			[nameof(IMap.MapType)] = MapMapType,
			[nameof(IMap.IsShowingUser)] = MapIsShowingUser,
			[nameof(IMap.HasScrollEnabled)] = MapHasScrollEnabled,
			[nameof(IMap.HasTrafficEnabled)] = MapHasTrafficEnabled,
			[nameof(IMap.HasZoomEnabled)] = MapHasZoomEnabled,
		};

		public static CommandMapper<IMap, IMapHandler> CommandMapper = new(ViewCommandMapper);

		public MapHandler() : base(Mapper, CommandMapper)
		{

		}

		public MapHandler(IPropertyMapper? mapper = null, CommandMapper? commandMapper = null)
		: base(mapper ?? Mapper, commandMapper ?? CommandMapper)
		{
		}

		IMap IMapHandler.VirtualView => VirtualView;

		PlatformView IMapHandler.PlatformView => PlatformView;
	}
}