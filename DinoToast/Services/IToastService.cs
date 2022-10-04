namespace DinoToast.Services
{
    public interface IToastService
    {
        event Action OnHide;
        event Action<string, ToastType> OnShow;

        void ShowToast(string message, ToastType toastType);
    }
}