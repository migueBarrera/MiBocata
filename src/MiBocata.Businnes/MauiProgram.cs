using Mibocata.Core;
using MiBocata.Businnes;
using MiBocata.Framework;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureEssentials()
			.ConfigureCoreServices()
			.ConfigureServices()
			.ConfigureViewModels()
			.ConfigurePages()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("SpaceMono-Bold.ttf", "SpaceMonoBold");
				fonts.AddFont("SpaceMono-Italic.ttf", "SpaceMonoItalic");
				fonts.AddFont("SpaceMono-Regular.ttf", "SpaceMonoRegular");
			});

		return builder.Build();
	}
}
