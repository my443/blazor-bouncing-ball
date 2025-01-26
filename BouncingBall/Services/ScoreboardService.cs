namespace BouncingBall.Services
{
    public class ScoreboardService
    {
        public event Action OnChange;
        private int _ballx;

        public int BallX
        {
            get => _ballx;
            set
            {
                _ballx = value;
                NotifyStateChanged();
            }
        }

        public int Score { get; internal set; }

        private void NotifyStateChanged() => OnChange?.Invoke();

    }
}
