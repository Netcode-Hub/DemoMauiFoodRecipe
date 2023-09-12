using CommunityToolkit.Mvvm.ComponentModel;
using DemoMauiFoodRecipe.Models;
namespace DemoMauiFoodRecipe.ViewModels
{
    [QueryProperty(nameof(TappedRecipe), "SelectedRecipe")]
    public partial class ReadRecipeDetailsPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string title;

        [ObservableProperty]
        private Recipe tappedRecipe;

        public ReadRecipeDetailsPageViewModel()
        {
            Title = "Recipe Details";
        }
    }
}
