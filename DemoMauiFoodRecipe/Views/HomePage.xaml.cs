using DemoMauiFoodRecipe.ViewModels;

namespace DemoMauiFoodRecipe.Views;

public partial class HomePage : ContentPage
{
    private readonly HomePageViewModel homePageViewModel;

    public HomePage(HomePageViewModel homePageViewModel)
    {
        InitializeComponent();
        BindingContext = homePageViewModel;
        this.homePageViewModel = homePageViewModel;
    }

    protected override void OnAppearing()
    {
        homePageViewModel.LoadDataCommand.Execute(this);
    }
}