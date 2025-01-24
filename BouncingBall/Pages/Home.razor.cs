using BouncingBall.Models;


namespace BouncingBall.Pages
{
    public partial class Home
    {
        Ball ball = new Ball();
        Canvas canvas = new Canvas();

        private Timer _timer;

        protected override void OnInitialized()
        {
            ball.x = 20;
            ball.y = 20;
            ball.velocityX = 2;
            ball.velocityY = 2;
            ball.radius = 20;

            _timer = new Timer(MoveBall, null, 0, 1);

        }

        public void MoveBall(object state)
        {
            ChangeDirectionIfNeeded();
            ball.x += ball.velocityX;
            ball.y += ball.velocityY;
            StateHasChanged();
        }

        public void ChangeDirectionIfNeeded()
        {
          if (ball.y + ball.radius > canvas.Height || ball.y < 0)
            {
                ball.velocityY *= -1;
            }
            if (ball.x + ball.radius > canvas.Width || ball.x - ball.radius < 0)
            {
                ball.velocityX *= -1;
            }
        }

    }
}
