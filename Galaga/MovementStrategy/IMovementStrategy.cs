using DIKUArcade.Entities;

namespace Galaga {
public interface IMovementStrategy {
    
    ///<summary>Moves an enemy</summary>
    ///<param name = "enemy">An enemy to move</param>
    void MoveEnemy (Enemy enemy);

    ///<summary>Moves every enemy in an EntityContainer</summary>
    ///<param name = "enemies">An EntityContainer of enemies to move</param>
    void MoveEnemies (EntityContainer<Enemy> enemies);
    }
}