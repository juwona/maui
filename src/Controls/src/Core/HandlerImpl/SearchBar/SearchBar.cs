﻿namespace Microsoft.Maui.Controls
{
	public partial class SearchBar
	{
		public static IPropertyMapper<ISearchBar, SearchBarHandler> ControlsSearchBarMapper = 
			new PropertyMapper<SearchBar, SearchBarHandler>(SearchBarHandler.SearchBarMapper)
		{
			[nameof(Text)] = MapText,
			[nameof(TextTransform)] = MapText,
		};

		internal static new void RemapForControls()
		{
			// Adjust the mappings to preserve Controls.SearchBar legacy behaviors
			SearchBarHandler.SearchBarMapper = ControlsSearchBarMapper;
		}
	}
}