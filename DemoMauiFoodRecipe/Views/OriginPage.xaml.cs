using DemoMauiFoodRecipe.ViewModels;

namespace DemoMauiFoodRecipe.Views;

public partial class OriginPage : ContentPage
{
    private readonly OriginPageViewModel originPageViewModel;

    public OriginPage(OriginPageViewModel originPageViewModel)
    {
        InitializeComponent();
        BindingContext = originPageViewModel;
        this.originPageViewModel = originPageViewModel;
    }

    protected override void OnAppearing()
    {
        originPageViewModel.LoadOriginsCommand.Execute(this);
    }
}