using FoodWasteApp.ViewModels;

namespace FoodWasteApp.Views;

public partial class FoodInputPage : ContentPage
{
	public FoodInputPage(FoodInputViewModel foodInputViewModel)
	{
		InitializeComponent();

		BindingContext = foodInputViewModel;
	}
}