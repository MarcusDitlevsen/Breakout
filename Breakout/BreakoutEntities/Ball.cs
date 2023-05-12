using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.BreakoutUtils;

namespace BreakoutEntities {
    public abstract class Ball : Entity {
        protected DynamicShape shape;
        public abstract float SpeedX { get; set; }
        public abstract float SpeedY { get; set; }
        public abstract int Damage { get; set; }
        public bool SpeedDoubled { get; set; }
        public abstract bool IsOutOfBounds {get; set;}
        public bool Invisible { get; set; } = false;
        protected readonly IImageDatabase imageDatabase = new ImageDatabase(Path.Combine("Assets", "Images"));

        public Ball(DynamicShape shape, IBaseImage image) : base(shape, image) {}

        ///<summary> Changes the direction of the ball after colliding 
        /// with another object </summary>
        ///<param name = "player"> Checks if the ball collides with the player </param>
        ///<param name = "message">Used to check if the ball should change
        /// direction, by saving the event as being true </param>
        ///<param name = "direction"> Changes the direction of the ball </param>
        public abstract void ChangeDirection(Player player, string message, string direction); 
        


        ///<summary> Moves the ball along its current directional vectors </summary>
        public abstract void Move();

        ///<summary> Fetches the position of the ball </summary>
        public Vec2F GetPosition() {
            return shape.Position;
        }

        ///<summary> Resets the ball's position </summary>
        public void ResetBall()
        {
            SpeedX = 0.01f;
            SpeedY = 0.01f;
            IsOutOfBounds = false;
        }

        ///<summary> Doubles the speed of the ball</summary>
        public void DoubleSpeed() {
            if (!SpeedDoubled)
            {
                SpeedX *= 2;
                SpeedY *= 2;
                SpeedDoubled = true;
            }
        }

        ///<summary> Resets the ball to its original speed </summary>
        public void ResetToNormalSpeed() {
            if (SpeedDoubled)
            {
                SpeedX /= 2;
                SpeedY /= 2;
                SpeedDoubled = false;
            }
        }

        ///<summary> Inverts the ball's current direction </summary>
        public void Invert(){
            SpeedX *= -1;
            SpeedY *= -1; 
        }

        ///<summary> Constantly renders the ball, unless Invisible is set to true </summary>
        public new void RenderEntity(){
            if(!Invisible){
                base.RenderEntity();
            }
        }

        ///<summary> Allows concrete implementation of a removal of any power-up </summary>
        public abstract Ball RemovePowerUp(PowerUp.PowerUpType powerUpType);
    }
}