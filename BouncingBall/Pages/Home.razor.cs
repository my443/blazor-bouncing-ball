using BouncingBall.Models;
using BouncingBall.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Toolbelt.Blazor.Gamepad;


namespace BouncingBall.Pages
{
    public partial class Home
    {
        [Inject]
        private ScoreboardService Scoreboard { get; set; }

        Ball ball = new Ball();
        Canvas canvas = new Canvas();
        Paddle paddle = new Paddle();

        private string key;
        private ElementReference divElement;

        private Timer _timer;
        private Timer _keyTimer;

        private Gamepad? _gamepad;
        private int _paddleY;

        protected override void OnInitialized()
        {
            ball.x = 20;
            ball.y = 20;
            ball.velocityX = 2;
            ball.velocityY = 2;
            ball.radius = 20;
            _paddleY = canvas.Height - 50;

            _timer = new Timer(MoveBall, null, 0, 1);
            _keyTimer = new Timer(CheckKeyPress, null, 0, 10);
        }

        public void MoveBall(object state)
        {
            gamepadMovement();
            ChangeDirectionIfNeeded();

            ball.x += ball.velocityX;
            ball.y += ball.velocityY;

            UpdateScoreboard();
            StateHasChanged();
        }

        private async void gamepadMovement()
        {
            var gamepads = await GamePadList.GetGamepadsAsync();
            _gamepad = gamepads.FirstOrDefault();
            if (_gamepad != null)
                if (_gamepad.Axes.Count > 2)
                {
                    if (Math.Round(_gamepad.Axes[2], 1) > 0)
                    {
                        moveRight();
                    }
                    if (Math.Round(_gamepad.Axes[2], 1) < 0)
                    {
                        moveLeft();
                    }
                }
            //else if (_gamepad.Axes[4] > 0)
            //{
            //    moveRight();
            //}
            await this.InvokeAsync(() => this.StateHasChanged());
        }

        public void ChangeDirectionIfNeeded()
        {
            if (ball.y + ball.radius > canvas.Height || ball.y < 0)
            {
                ball.velocityY *= -1;
            }
            if (ball.x + ball.radius > canvas.Width || ball.x < 0)
            {
                ball.velocityX *= -1;
            }
            if ((ball.x  > paddle.x && ball.x < paddle.x + paddle.width) && ball.y + ball.radius  > _paddleY)
            {
                ball.velocityY *= -1;
                ball.y = _paddleY - ball.radius-2;

            }
        }

        private async void CheckKeyPress(Object state)
        {
            if (key == "ArrowRight")
            {
                moveRight();
            }

            if (key == "ArrowLeft")
            {
                moveLeft();
            }
        }

        private void HandleKeyUp(KeyboardEventArgs e)
        {
            key = null;
        }

        private async void HandleKeyDown(KeyboardEventArgs e)
        {
            key = $"{e.Key}";
        }

        private void moveLeft()
        {
            paddle.x -= 10;
            if (paddle.x < 0)
            {
                paddle.x = 0;
            }
        }
        private void moveRight()
        {
            paddle.x += 10;
            if (paddle.x > canvas.Width - 100)
            {
                paddle.x = canvas.Width - 100;
            }
        }   
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await divElement.FocusAsync();
            }
        }

        private void UpdateScoreboard()
        {
            Scoreboard.BallX = ball.x;
        }

    }
}
