using DemoMauiFoodRecipe.Views;

namespace DemoMauiFoodRecipe
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(OriginPage), typeof(OriginPage));
            Routing.RegisterRoute(nameof(RecipePage), typeof(RecipePage));
            Routing.RegisterRoute(nameof(ProcedurePage), typeof(ProcedurePage));
            Routing.RegisterRoute(nameof(ReadRecipeDetailsPage), typeof(ReadRecipeDetailsPage));
        }
    }
}
