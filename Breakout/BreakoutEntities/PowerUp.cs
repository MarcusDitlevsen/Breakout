using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.BreakoutUtils;


namespace BreakoutEntities {
    public class PowerUp : Entity {
        public DynamicShape shape {get; private set;}
        public PowerUpType CurrentPowerUp {get; private set;}
        public PowerUp(DynamicShape shape, IBaseImage image, PowerUpType CurrentPowerUp) : base(shape, image) {
            this.shape = shape;
            this.CurrentPowerUp = CurrentPowerUp;
        }

        public enum PowerUpType {
            None,
            ExtraLife,
            WidePlayer,
            PlayerSpeed,
            HardBall,
            DoubleSpeed,
            AcceleratingBall,
            InvertedBall,
            InvisibleBall,
        }

        public static PowerUp RandomPowerUp(DynamicShape shape, IImageDatabase imageDatabase) {
            Random rnd = new Random ();
            PowerUp power;
            PowerUpType caseType = (PowerUpType)rnd.Next(Enum.GetNames(typeof(PowerUpType)).Length-1);
            switch (caseType) {
                case PowerUp.PowerUpType.ExtraLife:
                        return power = new PowerUp(shape, imageDatabase.GetImage("LifePickUp.png"), PowerUpType.ExtraLife);
                    
                    case PowerUp.PowerUpType.WidePlayer:
                        return power = new PowerUp(shape, imageDatabase.GetImage("WidePowerUp.png"), PowerUpType.WidePlayer);
                    
                    case PowerUp.PowerUpType.PlayerSpeed:
                        return power = new PowerUp(shape, imageDatabase.GetImage("DoubleSpeedPowerUp.png"), PowerUpType.PlayerSpeed);
                    
                    case PowerUp.PowerUpType.HardBall:
                        return power = new PowerUp(shape, imageDatabase.GetImage("DamagePickUp.png"), PowerUpType.HardBall);
                    
                    case PowerUp.PowerUpType.DoubleSpeed:
                        return power = new PowerUp(shape, imageDatabase.GetImage("SpeedPickUp.png"), PowerUpType.DoubleSpeed);

                    case PowerUp.PowerUpType.AcceleratingBall:
                        return power = new PowerUp(shape, imageDatabase.GetImage("BulletRed2.png"), PowerUpType.AcceleratingBall);

                    case PowerUp.PowerUpType.InvertedBall:
                        return power = new PowerUp(shape, imageDatabase.GetImage("SpreadPickUp.png"), PowerUpType.InvertedBall);
                    
                    case PowerUp.PowerUpType.InvisibleBall:
                        return power = new PowerUp(shape, imageDatabase.GetImage("E.png"), PowerUpType.InvisibleBall);
                    
                    default:
                        return power = new PowerUp(shape, imageDatabase.GetImage("white-square.png"), PowerUpType.None);
            }
        }

        ///<summary>Moves the power-up down with a fixed speed</summary>
        public void Move() {
            shape.MoveY(shape.Direction.Y);
            shape.MoveX(shape.Direction.X);
        }
    }
}