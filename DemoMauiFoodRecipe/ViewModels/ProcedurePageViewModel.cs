using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DemoMauiFoodRecipe.Models;
using DemoMauiFoodRecipe.Services;
using MvvmHelpers;
using System.Collections.ObjectModel;

namespace DemoMauiFoodRecipe.ViewModels
{
    public partial class ProcedurePageViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        private readonly IRecipeService recipeService;
        public ProcedurePageViewModel(IRecipeService recipeService)
        {
            this.recipeService = recipeService;
            Title = "Manage Procedures";
        }

        [ObservableProperty]
        private string title;

        [ObservableProperty]
        private Procedure procedureObject;

        [ObservableProperty]
        private bool showPopup;

        [ObservableProperty]
        private string headerTitle;

        public ObservableCollection<Recipe> RecipeObjects { get; set; } = new();
        public ObservableRangeCollection<Procedure> ProcedureObjects { get; set; } = new();


        [ObservableProperty]
        private Procedure selectedRowData;
        partial void OnSelectedRowDataChanged(Procedure oldValue, Procedure newValue)
        {
            if (oldValue != newValue)
                ManageSelectedData(newValue);
        }

        [ObservableProperty]
        private Recipe selectedRecipe;
        partial void OnSelectedRecipeChanged(Recipe oldValue, Recipe newValue)
        {
            try
            {
                if (newValue is null) return;
                if (newValue.Id <= 0) return;
                ProcedureObject.RecipeId = newValue.Id;
            }
            catch (Exception) { return; }
        }

        [ObservableProperty]
        private Recipe getProceduresByRecipe;

        partial void OnGetProceduresByRecipeChanged(Recipe value)
        {
            CallGetProceduresByRecipeMethod(value.Id);
        }

        [RelayCommand]
        private void ShowDialog()
        {
            HeaderTitle = "Add Procedure";
            ProcedureObject = new Procedure();
            ShowPopup = true;
        }


        [RelayCommand]
        private async Task SaveObject()
        {
            if (ProcedureObject.RecipeId <= 0 || ProcedureObject.Title is null) return;

            int result;
            if (ProcedureObject.Id > 0)
                result = await recipeService.UpdateProcedureAsync(ProcedureObject);
            else
                result = await recipeService.AddProcedureAsync(ProcedureObject);

            if (result > 0)
            {
                await ToastModel.MakeToast("Process completed successfully");
                ShowPopup = false;
                ProcedureObject = new Procedure();
                await LoadProcedures();
            }
        }

        [RelayCommand]
        private async Task LoadProcedures()
        {
            var results = await recipeService.GetProceduresAsync();
            if (ProcedureObjects.Count > 0)
                ProcedureObjects.Clear();

            if (results is not null)
            {
                if (results.Count > 0)
                    ProcedureObjects.AddRange(results.OrderByDescending(_ => _.Id));
            }


            // get origin as well
            var recipeResults = await recipeService.GetRecipesAsync();
            if (RecipeObjects.Count > 0)
                RecipeObjects.Clear();
            if (recipeResults is not null)
            {
                foreach (var recipeObject in recipeResults)
                    RecipeObjects.Add(recipeObject);
            }
        }

        private async Task ManageSelectedData(Procedure selectedProcedureModel)
        {
            if (selectedProcedureModel is null) return;
            string action = await Shell.Current.DisplayActionSheet("Action: Choose an Option", "Cancel", null, "Edit", "Delete");
            if (string.IsNullOrEmpty(action) || string.IsNullOrWhiteSpace(action)) return;

            if (action.Equals("Cancel")) return;

            if (action.Equals("Edit"))
            {
                ProcedureObject = new Procedure();
                ProcedureObject = selectedProcedureModel;
                HeaderTitle = "Update Procedure";
                ShowPopup = true;
            }

            if (action.Equals("Delete"))
            {
                bool answer = await Shell.Current.DisplayAlert("Confirm Operation", "Are you sure you wanna do this?", "Yes", "No");
                if (!answer) return;

                int result = await recipeService.DeleteProcedureAsync(selectedProcedureModel);
                if (result > 0)
                {
                    await ToastModel.MakeToast("Origin deleted successfully");
                    await LoadProcedures();
                    selectedProcedureModel = new Procedure();
                    return;
                }

                await Shell.Current.DisplayAlert("Alert", "Error occured", "Ok");
                return;
            } 
        }


        private async void CallGetProceduresByRecipeMethod(int recipeId)
        {

            var result = await recipeService.GetAllProceduresByRecipeId(recipeId);
            if (ProcedureObjects is not null)
                ProcedureObjects.Clear();

            if (result is not null)
                ProcedureObjects.AddRange(result);
        }
    }
}
