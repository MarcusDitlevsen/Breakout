using Breakout.BreakoutUtils;
using DIKUArcade.Entities;
using DIKUArcade.Math;

namespace BreakoutEntities
{
    public static class BallFactory
    {
        private readonly static IImageDatabase imageDatabase = new ImageDatabase(Path.Combine("Assets", "Images"));
        private const float BallSize = 0.04f;

        ///<Summary> Creates a new ball </summary>
        ///<param name = "player"> The player that the ball takes its position and direction from </param>
        public static Ball GetNewBallFromPlayer(Player player)
        {
            return new ConcreteBall(
                new DynamicShape(
                    new Vec2F(player.Shape.Position.X + player.Shape.Extent.X / 2.0f - BallSize / 2,
                            player.Shape.Position.Y + player.Shape.Extent.Y), new Vec2F(BallSize,BallSize)), 
                            imageDatabase.HasImage("ball2.png") ? imageDatabase.GetImage("ball2.png") : imageDatabase.GetImage("ball.png"));
        }

        ///<Summary> Fetches an accelerating ball </summary>
        ///<param name = "ball"> The ball that will be fetched/returned </param>
        ///<returns> An accelerating ball </returns>
        public static Ball GetAcceleratingBall(Ball ball){
            return new AcceleratingBallDecorator(ball);
        }
    }
}
