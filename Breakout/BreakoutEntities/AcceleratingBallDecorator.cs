namespace BreakoutEntities
{
    public class AcceleratingBallDecorator : BallDecorator
    {
        protected const float Acceleration = 0.00002f;
        public AcceleratingBallDecorator(Ball ball) : base(ball) {
        
        }

        ///<summary> Overrides the ball's speed with new values, 
        ///as to make it accelerate </summary>
        protected override void AdditionalBehavior() {
            SpeedX += SpeedX > 0 ? Acceleration : - Acceleration;
            SpeedY += SpeedY > 0 ? Acceleration : - Acceleration;    
        }

        ///<summary> Removes a power-up from the ball </summary>
        ///<param name = "powerUpType"> The specific power-up to be removed </param>
        ///<returns> A ball with a power-up removed </returns>
        public override Ball RemovePowerUp(PowerUp.PowerUpType powerUpType){
            if(powerUpType != PowerUp.PowerUpType.AcceleratingBall) {
                Component.RemovePowerUp(powerUpType);
                return this;
            } 
            if(Component.GetType() != typeof(AcceleratingBallDecorator)) {
                    Component.ResetBall();
                } 
            return Component;
        }
    }
}