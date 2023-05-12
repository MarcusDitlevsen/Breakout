using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Galaga {
public interface ISquadron {
    EntityContainer<Enemy> Enemies {get;}
    int MaxEnemies {get;}

    ///<summary>Creates enemies in the squadron with the given images</summary>
    ///<param name = "enemyStride">Image strides to normal enemies</param>
    ///<param name = "alternativeEnemyStride">Image strides for enraged enemies</param>
    void CreateEnemies (List<Image> enemyStride,
        List<Image> alternativeEnemyStride);
    }
}