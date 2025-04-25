
namespace mau_assignment_4;


public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddTransient<MainPageModel>();
		builder.Services.AddTransient<IAlertService, AlertService>();
		builder.Services.AddTransient<IAnimalService, AnimalService>();		
		builder.Services.AddTransient<IFoodScheduleService, FoodScheduleService>();
		builder.Services.AddTransient<IListService<Animal>, ListService<Animal>>();
		builder.Services.AddTransient<IListService<FoodSchedule>, ListService<FoodSchedule>>();
		builder.Services.AddTransient<IPropertyValidator, PropertyValidator>();
		builder.Services.AddSingleton<IFileSaver>(FileSaver.Default);
		builder.Services.AddTransient<ISaveSettings, SaveSettings>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
