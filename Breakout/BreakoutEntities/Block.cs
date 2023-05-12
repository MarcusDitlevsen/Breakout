using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using Breakout;
using DIKUArcade.Math;
using Breakout.BreakoutUtils;

namespace BreakoutEntities {
    public abstract class Block : Entity, IGameEventProcessor {
        public int value {get; private set;}
        public int maxHealth {get; private set;}
        public int health {get; private set;}
        public Entity entity {get; private set;}
        public StationaryShape shape {get; private set;}
        public string name {get; private set;}
        public BlockTypes.BlockType blockType {get; private set;}
        public PowerUp.PowerUpType powerUpType {get; private set;}
        private Image powerUpImage;
        private PowerUp newPowerUp;
        private IImageDatabase imageDatabase;

        public Block(StationaryShape shape, IBaseImage image, string name,
                BlockTypes.BlockType blockType, int value, PowerUp.PowerUpType powerUp)
                    : base(shape, image) {
        
            this.value = value;
            this.health = 2;
            this.maxHealth = health;
            this.shape = shape;
            this.entity = new Entity(shape, image);
            this.name = name;
            this.blockType = blockType;
            this.powerUpType = powerUp;
            imageDatabase = new ImageDatabase(Path.Combine("Assets", "Images"));
        }

        ///<summary> Fetches the position of the given block </summary>
        public virtual Vec2F GetPosition() {
            return shape.Position;
        }

        ///<summary> Makes the block take damage </summary>
        ///<param name = "amount"> Value used for the block's hit points </param>
        public virtual void TakeDamage(int amount) {
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

        ///<summary> Changes the block's image when damaged </summary>
        ///<param name = "path"> Finds the file path to the image 
        /// of a damaged block </param>
        public virtual void ChangeStateToDamaged() {
            //Assigns a name to the block
            string damagedPngName = name + "-damaged.png";
            Image = imageDatabase.GetImage(damagedPngName);
        }

        ///<summary> Destroys the block </summary>
        public virtual void Destroy() {
            this.DeleteEntity();
        }

        ///<summary> Gives info on how many points a block gives </summary>
        ///<returns> The points that a block gives </returns>
        public int GetValue() {
            return this.value;
        }

        ///<summary> Processes a given event </summary>
        ///<param name = "gameEvent"> Specifies which event is happening</param>
        public virtual void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.StatusEvent) {
                switch (gameEvent.Message) {
                    case "TAKE_DAMAGE":
                        TakeDamage(gameEvent.IntArg1);
                        break;
                    
                    case "DESTROY":
                        Destroy();
                        break;
                }
            }
        }
    }
}