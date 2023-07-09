using FoodWasteApp.Services.Auth0;
using FoodWasteApp.ViewModels;
using FoodWasteApp.Views;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Http;
using CommunityToolkit.Maui;

namespace FoodWasteApp;

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

#if DEBUG
		builder.Logging.AddDebug();
#endif

		// Add Pages DI
		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<FoodInputPage>();

		// Add ViewModels DI

		builder.Services.AddSingleton<FoodInputViewModel>();
		builder.Services.AddSingleton<MainViewModel>();

		// Add Services DI
		builder.Services.AddSingleton(new Auth0Client(new()
		{
			Domain = "dev-nw60en5uln7ga8bc.us.auth0.com",
			ClientId = "pHiowP3RQXC57hq2RvwUmKcTWRfGtCjP",
			Scope = "openid profile offline_access",
			Audience = "https://FoodWasteApi",
#if WINDOWS
			RedirectUri = "http://localhost/callback"
#else 
			RedirectUri = "myapp://callback"
#endif
		}));

        builder.Services.AddSingleton<TokenHandler>();
		builder.Services.AddHttpClient("DemoAPI", client => client.BaseAddress = new Uri("https://localhost:6061")).AddHttpMessageHandler<TokenHandler>();
		builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("DemoAPI"));

		return builder.Build();
	}
}
