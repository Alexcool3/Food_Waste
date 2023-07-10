using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FoodWasteApp.Services;
using FoodWasteApp.Services.Auth0;
using FoodWasteApp.Views;
using FoodWasteClassLibrary.Models;

namespace FoodWasteApp.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        private readonly Auth0Client auth0Clienct;

        [ObservableProperty] User user = new User();

        [ObservableProperty] bool stayLoggedIn;

        public MainViewModel(Auth0Client auth0Client) 
        {
            auth0Clienct = auth0Client;
        }

        [RelayCommand]
        public void CheckedChanged()
        {
            // Store or remove the "StayLoggedIn" property according to checkbox value.
            Preferences.Default.Set(nameof(StayLoggedIn), StayLoggedIn);
        }

        [RelayCommand]
        public async Task LoginAsync()
        {
            var loginResult = await auth0Clienct?.LoginAsync();

            if (!loginResult.IsError)
            {
                // Redirect to logged in page (Food Categories Page)
                await Shell.Current.GoToAsync($"///{nameof(FoodInputPage)}", true);
            }
        }

        private void Current_NavigatedTo(object sender, NavigatedToEventArgs e)
        {
            throw new NotImplementedException();
        }

        [RelayCommand]
        public async Task LogoutAsync()
        {
            await auth0Clienct.LogoutAsync();
        }

        public async Task AutomaticLogin()
        {
            if (StayLoggedIn = Preferences.Default.Get(nameof(StayLoggedIn), false))
            {
                var user = await auth0Clienct.GetAuthenticatedUser();

                if (user != null)
                {
                    // Redirect to logged in page (Food Categories Page)
                    await Shell.Current.GoToAsync(nameof(FoodInputPage));
                }
            }
        }

        public void SetWebViewBrowser(WebView webView)
        {
            auth0Clienct.Browser = new WebViewBrowserAuthenticator(webView);
        }
    }
}
