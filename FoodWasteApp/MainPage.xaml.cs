using FoodWasteApp.Services;
using FoodWasteApp.ViewModels;
using System.Diagnostics;

namespace FoodWasteApp;

public partial class MainPage : ContentPage
{
	public MainViewModel mainViewModel;

	public MainPage(MainViewModel mainViewModel)
	{
        InitializeComponent();
        BindingContext = mainViewModel;
		this.mainViewModel = mainViewModel;
    }

    private async void OnLoaded(object sender, EventArgs e)
	{
		await mainViewModel.AutomaticLogin();
	}

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        await mainViewModel.LoginAsync();

        base.OnNavigatedTo(args);
    }

    protected override bool OnBackButtonPressed()
    {
        return true;
    }
}

