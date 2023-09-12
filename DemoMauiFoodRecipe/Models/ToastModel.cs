using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
namespace DemoMauiFoodRecipe.Models
{
    public class ToastModel
    {
        public static async Task MakeToast(string message)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            ToastDuration duration = ToastDuration.Short;
            double fontSize = 14;
            var toast = Toast.Make(message, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
    }
}
