using System.Collections.Generic;
using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using System;
using Breakout.BreakoutUtils;

namespace BreakoutEntities
{
    public class ConcreteBall : Ball
    {
        public override float SpeedX { get; set; }
        public override float SpeedY { get; set; }
        public override int Damage { get;  set; }
        public override bool IsOutOfBounds { get; set; }
        public ConcreteBall(DynamicShape shape, IBaseImage image) : base(shape, image)
        {
            this.shape = shape;

            SpeedX = 0.01f;
            SpeedY = 0.01f;

            this.shape.Direction.X = SpeedX;
            this.shape.Direction.Y = SpeedY;

            Damage = 1;

            IsOutOfBounds = false;
        }


        ///<summary> Changes the direction of the ball after colliding 
        /// with another object </summary>
        ///<param name = "player"> Checks if the ball collides with the player </param>
        ///<param name = "message">Used to check if the ball should change
        /// direction, by saving the event as being true </param>
        ///<param name = "direction"> Changes the direction of the ball </param>
        public override void ChangeDirection(Player player, string message, string direction)
        {
            // Collision with player
            if (CollisionDetection.Aabb(shape, player.Shape).Collision)
            {
                SpeedY *= -1;
            }

            // Collision with sides
            else if (shape.Position.X <= 0.0f || shape.Position.X >= 1.0f - shape.Extent.X)
            {
                SpeedX *= -1;
            }

            // Collision with top
            else if (shape.Position.Y >= 1.0f - shape.Extent.Y)
            {
                SpeedY *= -1;
            }

            // If ball leaves bottom of screen
            else if (shape.Position.Y < 0.0f)
            {
                IsOutOfBounds = true;
            }

            // Reverse X speed
            else if (message == "TRUE" && direction == "X")
            {
                SpeedX *= -1;
            }

            // Reverse Y speed
            else if (message == "TRUE" && direction == "Y")
            {
                SpeedY *= -1;
            }
        }


        ///<summary> Moves the ball along its current directional vectors </summary>
        public override void Move()
        {
            shape.Direction.X = SpeedX;
            shape.Direction.Y = SpeedY;
            shape.Move();
        }

        ///<summary> Fetches the position of the ball </summary>
        public Vec2F GetPosition()
        {
            return shape.Position;
        }

        ///<summary> Resets the ball's speed </summary>
        public void ResetBall()
        {
            SpeedX = 0.01f;
            SpeedY = 0.01f;
            IsOutOfBounds = false;
        }

        public void DoubleSpeed()
        {
            if (!SpeedDoubled)
            {
                SpeedX *= 2;
                SpeedY *= 2;
                SpeedDoubled = true;
            }
        }

        public void ResetToNormalSpeed()
        {
            if (SpeedDoubled)
            {
                SpeedX /= 2;
                SpeedY /= 2;
                SpeedDoubled = false;
            }
        }
        
        public override Ball RemovePowerUp(PowerUp.PowerUpType powerUpType){
            switch (powerUpType){
                case PowerUp.PowerUpType.DoubleSpeed : 
                ResetToNormalSpeed();
                break;

                case PowerUp.PowerUpType.HardBall :
                Damage = 1;
                break;

                case PowerUp.PowerUpType.InvisibleBall : 
                Invisible = false;
                break;

            }
            return this;
        }
    }
}
 