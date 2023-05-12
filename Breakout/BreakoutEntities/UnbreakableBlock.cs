using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using Breakout;

namespace BreakoutEntities {
    public class UnbreakableBlock : Block, IGameEventProcessor {
        public int value {get; private set;}
        public int maxHealth {get; private set;}
        public int health {get; private set;}
        public Entity entity {get; private set;}
        public StationaryShape shape {get; private set;}
        public string name {get; private set;}
        public BlockTypes.BlockType blockType {get; private set;}
        public PowerUp.PowerUpType powerUpType {get; private set;}

        ///<summary> Defines the values of the unbreakable block </summary>
        ///<param name = "shape"> Gives the block a hitbox </param>
        ///<param name = "Image"> The image of the unbreakable block </param>
        ///<param name = "name"> The given name of the block </param>
        public UnbreakableBlock(StationaryShape shape, IBaseImage image, string name,
                BlockTypes.BlockType blockType, PowerUp.PowerUpType powerUpType) : base(shape, image, name, blockType, 0, powerUpType) {
            
            this.value = 0;
            this.health = 0;
            this.maxHealth = health;
            this.shape = shape;
            this.entity = new Entity(shape, image);
            this.name = name;
            this.blockType = blockType;
            this.powerUpType = powerUpType;
        }

        ///<summary> Makes the block take damage (or not, in this case)</summary>
        ///<param name = "amount"> Value used as the block's hit points </param>
        public override void TakeDamage(int amount) {
            // Empty so as to not take damage
        }
    }
}