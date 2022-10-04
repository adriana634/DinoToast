using System.Timers;
using Timer = System.Timers.Timer;

namespace DinoToast.Services
{
    public sealed class ToastService : IToastService, IDisposable
    {
        public event Action<ToastType, string> OnShow;
        public event Action OnHide;

        private static readonly int INTERVAL = 5000;
        private Timer Countdown;

        private void SetCountdown()
        {
            if (Countdown == null)
            {
                Countdown = new Timer(INTERVAL);
                Countdown.Elapsed += HideToast;
                Countdown.AutoReset = false;
            }
        }

        private void HideToast(object? sender, ElapsedEventArgs e)
        {
            OnHide?.Invoke();
        }

        private void StartCountdown()
        {
            SetCountdown();

            if (Countdown.Enabled)
            {
                Countdown.Stop();
                Countdown.Start();
            }
            else
            {
                Countdown.Start();
            }
        }

        private void ShowToast(ToastType toastType, string message)
        {
            OnShow?.Invoke(toastType, message);
            StartCountdown();
        }

        public void ShowInfo(string message) => ShowToast(ToastType.Info, message);
        public void ShowSuccess(string message) => ShowToast(ToastType.Success, message);
        public void ShowWarning(string message) => ShowToast(ToastType.Warning, message);
        public void ShowError(string message) => ShowToast(ToastType.Error, message);

        public void Dispose()
        {
            Countdown?.Dispose();
        }
    }
}
