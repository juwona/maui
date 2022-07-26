using Microsoft.Extensions.Logging;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;

namespace Maui.Controls.Sample
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp
				.CreateBuilder()
				.UseMauiApp<App>();

			builder.Logging.SetMinimumLevel(LogLevel.Trace);
			builder.Logging.AddDebug();

			return builder
				.Build();
		}
	}

	class App : Application
	{
		protected override Window CreateWindow(IActivationState activationState) =>
			new Window(new NavigationPage(new MainPage()));
	}
}