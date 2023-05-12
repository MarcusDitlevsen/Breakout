using DIKUArcade.Entities;
namespace Galaga {
    public class Down : IMovementStrategy {
        public Down() {
        }

        ///<summary>Moves an enemy</summary>
        ///<param name = "enemy">An enemy to move</param>
        public void MoveEnemy(Enemy enemy) {
            if (enemy.GetShape().Position.Y >= 0.0f && enemy.GetShape().Position.Y < 1.0f) {
                enemy.GetShape().MoveY(-enemy.GetSpeed());
            }
            else {
                enemy.DeleteEntity();
            }
        }

        ///<summary>Moves every enemy in an EntityContainer</summary>
        ///<param name = "enemies">An EntityContainer of enemies to move</param>
        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            foreach (Enemy enemy in enemies) {
                MoveEnemy(enemy);
            }
        }
    }
}