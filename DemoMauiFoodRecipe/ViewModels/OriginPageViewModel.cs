
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DemoMauiFoodRecipe.Models;
using DemoMauiFoodRecipe.Services;
using MvvmHelpers;

namespace DemoMauiFoodRecipe.ViewModels
{
    public partial class OriginPageViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {

        private readonly IRecipeService recipeService;
        public OriginPageViewModel(IRecipeService recipeService)
        {
            this.recipeService = recipeService;
            ShowPopup = false;
        }

        [ObservableProperty]
    private string title;

    [ObservableProperty]
    private Origin originObject;

    [ObservableProperty]
    private bool showPopup;

    [ObservableProperty]
    private string headerTitle;

        public ObservableRangeCollection<Origin> OriginObjects { get; set; } = new();

        [ObservableProperty]
        private Origin selectedRowData;

        partial void OnSelectedRowDataChanged(Origin oldValue, Origin newValue)
        {
            if (oldValue != newValue)
                ManageSelectedData(newValue);
        }

        [RelayCommand]
        private void ShowDialog()
        {
            HeaderTitle = "Add Origin";
            OriginObject = new Origin();
            ShowPopup = true;
        }

        [RelayCommand]
        private async Task SaveObject()
        {
            if (OriginObject == null) return;

            int result;
            if (OriginObject.Id > 0)
                result = await recipeService.UpdateAsync(OriginObject);
            else
                result = await recipeService.AddAsync(OriginObject);

            if (result > 0)
            {
                await ToastModel.MakeToast("Process completed successfully");
                ShowPopup = false;
                OriginObject = new Origin();
                await LoadOrigins();
            }

        }

        [RelayCommand]
        private async Task LoadOrigins()
        {
            var results = await recipeService.GetAsync();
            if (OriginObjects.Count > 0)
                OriginObjects.Clear();

            if (results.Count > 0)
                OriginObjects.ReplaceRange(results.OrderByDescending(_ => _.Id));
        }

        private async Task ManageSelectedData(Origin selectedOriginModel)
        {
            if (selectedOriginModel is null) return;
            string action = await Shell.Current.DisplayActionSheet("Action: Choose an Option", "Cancel", null, "Edit", "Delete");
            if (string.IsNullOrEmpty(action) || string.IsNullOrWhiteSpace(action)) return;

            if (action.Equals("Cancel")) return;

            if (action.Equals("Edit"))
            {
                OriginObject = new Origin()
                {
                    Id = selectedOriginModel.Id,
                    Name = selectedOriginModel.Name
                };
                HeaderTitle = "Update Origin";
                ShowPopup = true;
            }

            if (action.Equals("Delete"))
            {
                bool answer = await Shell.Current.DisplayAlert("Confirm Operation", "Are you sure you wanna do this?", "Yes", "No");
                if (!answer) return;

                int result = await recipeService.DeleteAsync(selectedOriginModel);
                if (result > 0)
                {
                    await ToastModel.MakeToast("Origin deleted successfully");
                    await LoadOrigins();
                    selectedOriginModel = new Origin();
                    return;
                }

                await Shell.Current.DisplayAlert("Alert", "Error occured", "Ok");
                return;

            }

        }
    }
}
