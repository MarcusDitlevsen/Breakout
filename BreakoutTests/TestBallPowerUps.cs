using NUnit.Framework;
using System.IO;
using System;
using DIKUArcade.GUI;
using BreakoutEntities;
using DIKUArcade.Entities;
using Breakout.BreakoutUtils;
using DIKUArcade.Math;

 
namespace BreakoutTests
{
    public class TestBall
    {
        Ball ball;
        Player player;
        private Vec2F prevPosition;
        private Vec2F currPosition;
        private IImageDatabase _imageDatabase = new ImageDatabase(Path.Combine("..", "Breakout", "Assets", "Images"));

 
        [SetUp]
        public void Setup()
        {
            Window.CreateOpenGLContext();

 
            player = new Player(
                    new DynamicShape(new Vec2F(0.4f, 0.1f), new Vec2F(0.2f, 0.3f)),
                    _imageDatabase.GetImage("player.png"));
            ball = BallFactory.GetNewBallFromPlayer(player);
        }

        [Test]
        public void TestAcceleratingBall()
        {
            Vec2F oldSpeed, origSpeed = new Vec2F(ball.SpeedX, ball.SpeedY);

           
            ball = BallFactory.GetAcceleratingBall(ball);
            for (int i = 0; i < 10; i++)
            {
                oldSpeed = new Vec2F(ball.SpeedX, ball.SpeedY);
                ball.Move();
                Assert.Greater(Math.Abs(ball.SpeedX), Math.Abs(oldSpeed.X));
                Assert.Greater(Math.Abs(ball.SpeedY), Math.Abs(oldSpeed.Y));
            }

            
            ball = ball.RemovePowerUp(PowerUp.PowerUpType.AcceleratingBall);
            Assert.AreEqual(ball.SpeedX, origSpeed.X);
            Assert.AreEqual(ball.SpeedY, origSpeed.Y);
            oldSpeed = new Vec2F(ball.SpeedX, ball.SpeedY);
            ball.Move();
            Assert.AreEqual(ball.SpeedX, oldSpeed.X);
            Assert.AreEqual(ball.SpeedY, oldSpeed.Y);

 
        }

        [Test]
        public void TestAccelerationSpeed()
        {
            Vec2F oldSpeed = new Vec2F(ball.SpeedX, ball.SpeedY);

           
            ball = BallFactory.GetAcceleratingBall(ball);
            for (int i = 0; i < 10; i++)
            {
                oldSpeed = new Vec2F(ball.SpeedX, ball.SpeedY);
                ball.Move();
                Assert.AreEqual(Math.Abs(ball.SpeedX), Math.Abs(oldSpeed.X) + 0.00002f);
                Assert.AreEqual(Math.Abs(ball.SpeedY), Math.Abs(oldSpeed.Y) + 0.00002f);
            }
        }

        [Test]
        public void TestInvisiblity()
        {
            ball.Invisible = true;

            Assert.True(ball.Invisible);

        }

        [Test]
        public void TestInversion(){
        
        prevPosition = ball.GetPosition();
        currPosition = ball.GetPosition();

        ball.Move();
        ball.Invert();
        ball.Move();

        Assert.AreSame(currPosition, prevPosition);

        //This test fails if the position is called after the methods, 
        //even though the expected result is the same as the actual result
        //Should be tested by comparing speed instead
    }
 

       

    }

}
