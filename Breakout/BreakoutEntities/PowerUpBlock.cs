using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using Breakout;
using DIKUArcade.Math;

namespace BreakoutEntities {
    public class PowerUpBlock : Block, IGameEventProcessor {
        public int value {get; private set;}
        public int maxHealth {get; private set;}
        public int health {get; private set;}
        public Entity entity {get; private set;}
        public StationaryShape shape {get; private set;}
        public string name {get; private set;}
        public BlockTypes.BlockType blockType;
        public PowerUp.PowerUpType powerUpType {get; private set;}

        ///<summary> Defines the values of the default block</summary>
        ///<param name = "shape"> Gives the block a hitbox </param>
        ///<param name = "Image"> The image of the default block </param>
        ///<param name = "name"> The given name of the block </param>
        public PowerUpBlock(StationaryShape shape, IBaseImage image, string name,
                BlockTypes.BlockType blockType, PowerUp.PowerUpType powerUpType) : base(shape, image, name, blockType, 1, powerUpType) {
            
            this.health = 2;
            this.maxHealth = health;
            this.shape = shape;
            this.entity = new Entity(shape, image);
            this.name = name;
            this.blockType = blockType;
            this.powerUpType = powerUpType;
        }
    }
}