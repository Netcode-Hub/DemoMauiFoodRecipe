using DemoMauiFoodRecipe.ViewModels;

namespace DemoMauiFoodRecipe.Views;

public partial class ReadRecipeDetailsPage : ContentPage
{
    public ReadRecipeDetailsPage(ReadRecipeDetailsPageViewModel readRecipeDetailsPageViewModel)
    {
        InitializeComponent();
        BindingContext = readRecipeDetailsPageViewModel;
    }
}