using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga {
    public class Enemy : Entity {
        private IBaseImage image;
        private DynamicShape shape;
        public int hitpoints {get; private set;}
        private IBaseImage enragedImg;
        private float speed;
        public float startPositionY {get; private set;}
        public bool isEnraged {get; private set;}



        public Enemy(DynamicShape shape, IBaseImage image, IBaseImage enragedImg)
            : base(shape, image) {
                this.shape = shape;
                this.image = image;
            
            hitpoints = 4;
            speed = 0.002f;
            this.enragedImg = enragedImg;
            startPositionY = shape.Position.Y;
            
            isEnraged = false;
            }

        ///<summary>Get the position of the object's shape</summary>
        ///<returns>A Vec2F of the shape's position</returns>
        public Vec2F GetPosition() {
            return shape.Position;
        }

        ///<summary>Get the object's shape</summary>
        ///<returns>A DynamicShape corresponding to the object's shape</returns>
        public DynamicShape GetShape() {
            return shape;
        }

        ///<summary>Registers a hit on the 'this' enemy object</summary>
        ///<param name = "amount">An integer of damage to take</param>
        ///<returns>An integer after being hit. 1 if the enemy is dead.
        ///0 if still alive. 2 if enraged</returns>
        public int Hit(int amount) {
            hitpoints -= amount;
            if (hitpoints <= 0) {
                DeleteEntity();
                return 1;
            }
            else if (hitpoints == 2) {
                isEnraged = true;
                Enrage();
                return 2;
            }
            else {
                return 0;
            }
        }

        ///<summary>Enrages and enemy by increasing speed and changing sprite</summary>
        private void Enrage() {
            if (isEnraged) {
                Image = enragedImg;
                ChangeSpeed(GetSpeed() * 2);
            }
        }

        ///<summary>Update speed to be paramter</summary>
        ///<param name ="amount">Float to changed speed variable into</param>
        public void ChangeSpeed(float amount) {
            speed = amount;
        }

        ///<summary>Get the enemy's current speed<summary>
        ///<returns>A float of the enemy's speed</returns>
        public float GetSpeed() {
            return speed;
        }
    }
}