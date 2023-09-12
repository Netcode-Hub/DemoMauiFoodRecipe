using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DemoMauiFoodRecipe.Models;
using DemoMauiFoodRecipe.Services;
using DemoMauiFoodRecipe.Views;
using MvvmHelpers;

namespace DemoMauiFoodRecipe.ViewModels
{
    public partial class HomePageViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        private readonly IRecipeService recipeService;
        public HomePageViewModel(IRecipeService recipeService)
        {
            this.recipeService = recipeService;
            Title = "Recipes Home";
        }

        [ObservableProperty]
        private string title;

        public ObservableRangeCollection<Recipe> RecipeObjects { get; set; } = new();

        [RelayCommand]
        private async Task LoadData()
        {
            var result = await recipeService.GetRecipesAsync();
            RecipeObjects?.Clear();

            if (result is not null)
                RecipeObjects.AddRange(result);
        }

        [RelayCommand]
        private async void GotoDetails(Recipe recipeDTO)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "SelectedRecipe",  recipeDTO}
            };

            // Navigate to the Recipe Route with the Navigation Parameter
            await Shell.Current.GoToAsync(nameof(ReadRecipeDetailsPage), navigationParameter);
        }

    }
}
