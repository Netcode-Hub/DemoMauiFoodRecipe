using DemoMauiFoodRecipe.ViewModels;

namespace DemoMauiFoodRecipe.Views;

public partial class RecipePage : ContentPage
{
    private readonly RecipePageViewModel recipePageViewModel;

    public RecipePage(RecipePageViewModel recipePageViewModel)
    {
        InitializeComponent();
        BindingContext = recipePageViewModel;
        this.recipePageViewModel = recipePageViewModel;
    }

    protected override void OnAppearing()
    {
        recipePageViewModel.LoadRecipesCommand.Execute(this);
    }
}