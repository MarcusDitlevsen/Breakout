using DIKUArcade.Entities;
using System;

namespace Galaga {
    public class ZigZagDown : IMovementStrategy {
        private float p = 0.045f;
        private float a = 0.035f;


        public ZigZagDown() {
        }

        public void MoveEnemy(Enemy enemy) {
            float nextY = -enemy.GetSpeed();
            float nextX = -(a * (MathF.Sin((2.0f*MathF.PI *
                (enemy.startPositionY- enemy.GetPosition().Y)) / p)));
            
            enemy.GetShape().MoveY(nextY);
            enemy.GetShape().MoveX(nextX);

        }

        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            foreach (Enemy enemy in enemies) {
                MoveEnemy(enemy);
            }
        }
    }
}