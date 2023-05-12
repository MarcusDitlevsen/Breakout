

namespace BreakoutEntities
{
    public abstract class BallDecorator : Ball
    {
        protected Ball Component { get; set; }
        public override float SpeedX { get => Component.SpeedX; set{Component.SpeedX = value;}}
        public override float SpeedY { get => Component.SpeedY; set{Component.SpeedY = value;}}
        public override int Damage { get => Component.Damage; set{Component.Damage = value;}}
        public override bool IsOutOfBounds { get => Component.IsOutOfBounds; set{Component.IsOutOfBounds = value;}}

        public BallDecorator(Ball ball) : base(ball.Shape.AsDynamicShape(), ball.Image) {
            Component = ball;
        }

        ///<summary> Changes the direction of the ball after colliding 
        /// with another object </summary>
        ///<param name = "player"> Checks if the ball collides with the player </param>
        ///<param name = "message">Used to check if the ball should change
        /// direction, by saving the event as being true </param>
        ///<param name = "direction"> Changes the direction of the ball </param>
        public override void ChangeDirection(Player player, string message, string direction) {
            Component?.ChangeDirection(player, message, direction);
        }

        ///<summary> Moves the ball with its new behavior </summary>
        
        public override void Move() { //override extends the base class method
            AdditionalBehavior();
            Component.Move();
        }

        ///<summary> Doubles the speed of the ball</summary>
        public new void DoubleSpeed() { //new hides an accessible base class method
            Component.DoubleSpeed();
        }

        ///<summary> Resets the ball to its original speed </summary>
        public new void ResetToNormalSpeed() {
            Component.ResetToNormalSpeed();
        }

        ///<summary> Inverts the ball's current direction </summary>
        public new void Invert(){ 
            Component.Invert();
        }

        ///<summary> Allows for concrete implementations of additional behavior on the ball </summary>
        protected abstract void AdditionalBehavior();

    }
}
