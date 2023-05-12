using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using Breakout;
using Breakout.BreakoutUtils;

namespace BreakoutEntities {
    public class HardenedBlock : Block, IGameEventProcessor {
        public int maxHealth {get; private set;}
        public int health {get; private set;}
        public Entity entity {get; private set;}
        public StationaryShape shape {get; private set;}
        public string name {get; private set;}
        public BlockTypes.BlockType blockType {get; private set;}
        public PowerUp.PowerUpType powerUpType {get; private set;}
        private IImageDatabase imageDatabase;

        ///<summary> Defines the values of the hardened block </summary>
        ///<param name = "shape"> Gives the block a hitbox </param>
        ///<param name = "Image"> The image of the hardened block </param>
        ///<param name = "name"> The given name of the block </param>
        public HardenedBlock(StationaryShape shape, IBaseImage image, string name,
                BlockTypes.BlockType blockType, PowerUp.PowerUpType powerUpType) : base(shape, image, name, blockType, 2, powerUpType) {
            
            this.health = 4;
            this.maxHealth = health;
            this.shape = shape;
            this.entity = new Entity(shape, image);
            this.name = name;
            this.blockType = blockType;
            this.powerUpType = powerUpType;
        }

        ///<summary> Makes the block take damage </summary>
        ///<param name = "amount"> Value used as the block's hit points </param>
        public override void TakeDamage(int amount) {
            if (amount >= 0) {
                health -= amount;
            }

            if (health <= 0) {
                Destroy();
            }
            else if (health <= maxHealth/2) {
                ChangeStateToDamaged();
            }
        }

    
    }
}