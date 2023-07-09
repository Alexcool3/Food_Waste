using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace FoodWasteApp.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {

        [ObservableProperty, NotifyCanExecuteChangedFor(nameof(SetIsNotBusyCommand))] bool isBusy;
        [ObservableProperty] string text;

        public bool IsNotBusy
        {
            get => !IsBusy;

            set
            {
                if(SetProperty(ref isBusy, value)) SetIsNotBusyCommand.NotifyCanExecuteChanged();
            }
        }

        [RelayCommand] public void SetIsNotBusy() => IsBusy = true;

        public BaseViewModel()
        {

        }
    }
}
