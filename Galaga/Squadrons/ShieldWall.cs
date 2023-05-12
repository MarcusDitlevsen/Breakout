using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.Collections.Generic;

namespace Galaga {
    public class ShieldWall : ISquadron {
        public EntityContainer<Enemy> Enemies {get;}
        public int MaxEnemies {get;}

        public ShieldWall() {
            MaxEnemies = 6;
            Enemies = new EntityContainer<Enemy>(MaxEnemies);
        }

        public void CreateEnemies(List<Image> enemyStride,
            List<Image> alternativeEnemyStride) {
            Enemies.AddEntity(new Enemy(
                new DynamicShape(new Vec2F(0.35f + 1.0f * 0.1f, 0.9f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStride), new ImageStride(80, alternativeEnemyStride)));
            Enemies.AddEntity(new Enemy(
                new DynamicShape(new Vec2F(0.35f + 2.0f * 0.1f, 0.9f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStride), new ImageStride(80, alternativeEnemyStride)));
            Enemies.AddEntity(new Enemy(
                new DynamicShape(new Vec2F(0.35f + 3.0f * 0.1f, 0.9f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStride), new ImageStride(80, alternativeEnemyStride)));
            Enemies.AddEntity(new Enemy(
                new DynamicShape(new Vec2F(0.35f + 1.0f * 0.1f, 0.8f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStride), new ImageStride(80, alternativeEnemyStride)));
            Enemies.AddEntity(new Enemy(
                new DynamicShape(new Vec2F(0.35f + 2.0f * 0.1f, 0.8f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStride), new ImageStride(80, alternativeEnemyStride)));
            Enemies.AddEntity(new Enemy(
                new DynamicShape(new Vec2F(0.35f + 3.0f * 0.1f, 0.8f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStride), new ImageStride(80, alternativeEnemyStride)));
            }
    }
}