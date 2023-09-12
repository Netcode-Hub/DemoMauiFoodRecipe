using DemoMauiFoodRecipe.ViewModels;

namespace DemoMauiFoodRecipe.Views;

public partial class ProcedurePage : ContentPage
{
    private readonly ProcedurePageViewModel procedurePageViewModel;

    public ProcedurePage(ProcedurePageViewModel procedurePageViewModel)
    {
        InitializeComponent();
        BindingContext = procedurePageViewModel;
        this.procedurePageViewModel = procedurePageViewModel;
    }

    protected override void OnAppearing()
    {
        procedurePageViewModel.LoadProceduresCommand.Execute(this);
    }
}