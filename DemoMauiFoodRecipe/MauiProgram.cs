using CommunityToolkit.Maui;
using DemoMauiFoodRecipe.Services;
using DemoMauiFoodRecipe.ViewModels;
using DemoMauiFoodRecipe.Views;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;

namespace DemoMauiFoodRecipe
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureSyncfusionCore()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton<IRecipeService, RecipeService>();
            builder.Services.AddSingleton<OriginPageViewModel>();
            builder.Services.AddSingleton<OriginPage>();
            builder.Services.AddSingleton<RecipePageViewModel>();
            builder.Services.AddSingleton<RecipePage>();
            builder.Services.AddSingleton<ProcedurePageViewModel>();
            builder.Services.AddSingleton<ProcedurePage>();
            builder.Services.AddSingleton<HomePageViewModel>();
            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddSingleton<ReadRecipeDetailsPageViewModel>();
            builder.Services.AddSingleton<ReadRecipeDetailsPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
