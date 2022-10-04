using DinoToast.Services;
using Microsoft.AspNetCore.Components;

namespace DinoToast
{
    public class ToastBase : ComponentBase, IDisposable
    {
        [Inject] IToastService ToastService { get; set; }

        public string Heading { get; set; }
        public string Message { get; set; }
        public bool IsVisible { get; set; }
        public string BackgroundCssClass { get; set; }
        public string IconCssClass { get; set; }

        private void BuildToastSettings(ToastType toastType, string message)
        {
            switch (toastType)
            {
                case ToastType.Info:
                    BackgroundCssClass = "bg-info";
                    IconCssClass = "info-circle";
                    Heading = "Info";
                    break;
                case ToastType.Success:
                    BackgroundCssClass = "bg-success";
                    IconCssClass = "check-circle";
                    Heading = "Success";
                    break;
                case ToastType.Warning:
                    BackgroundCssClass = "bg-warning";
                    IconCssClass = "exclamation-circle";
                    Heading = "Warning";
                    break;
                case ToastType.Error:
                    BackgroundCssClass = "bg-danger";
                    IconCssClass = "x-circle";
                    Heading = "Error";
                    break;
            }

            Message = message;
        }

        private async void ShowToast(ToastType toastType, string message)
        {
            BuildToastSettings(toastType, message);
            IsVisible = true;
            await InvokeAsync(StateHasChanged);
        }

        private async void HideToast()
        {
            IsVisible = false;
            await InvokeAsync(StateHasChanged);
        }

        protected override void OnInitialized()
        {
            ToastService.OnShow += ShowToast;
            ToastService.OnHide += HideToast;
        }

        public void Dispose()
        {
            ToastService.OnShow -= ShowToast;
            ToastService.OnHide -= HideToast;
        }
    }
}
