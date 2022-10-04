namespace DinoToast.Services
{
    public interface IToastService
    {
        event Action OnHide;
        event Action<ToastType, string> OnShow;

        void ShowInfo(string message);
        void ShowSuccess(string message);
        void ShowWarning(string message);
        void ShowError(string message);
    }
}