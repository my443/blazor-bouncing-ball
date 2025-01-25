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
        private void NotifyStateChanged() => OnChange?.Invoke();

    }
}
