using DIKUArcade.Entities;
using DIKUArcade.Math;

namespace Galaga {
    public class NoMove : IMovementStrategy {
        public NoMove() {
        }

        public void MoveEnemy(Enemy enemy) {
            enemy.GetShape().Move(new Vec2F(0.0f, 0.0f));
        }

        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            foreach (Enemy enemy in enemies) {
                MoveEnemy(enemy);
            }
        }
    }
}