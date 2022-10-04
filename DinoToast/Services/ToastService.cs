using System.Timers;
using Timer = System.Timers.Timer;

namespace DinoToast.Services
{
    public class ToastService : IToastService, IDisposable
    {
        public event Action<string, ToastType> OnShow;
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

        public void ShowToast(string message, ToastType toastType)
        {
            OnShow?.Invoke(message, toastType);
            StartCountdown();
        }

        public void Dispose()
        {
            Countdown?.Dispose();
        }
    }
}
