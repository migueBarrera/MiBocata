using Mibocata.Core;
using MiBocata;

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
			.ConfigurePages()
			.ConfigureViewModels()
			.ConfigureFonts(fonts =>
			{
                fonts.AddFont("SpaceMono-Bold.ttf", "SpaceMonoBold");
                fonts.AddFont("SpaceMono-Italic.ttf", "SpaceMonoItalic");
                fonts.AddFont("SpaceMono-Regular.ttf", "SpaceMonoRegular");
            });

		return builder.Build();
	}
}
