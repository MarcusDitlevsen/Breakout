using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.Collections.Generic;

namespace Galaga {
    public class ReverseWedge : ISquadron {
        public EntityContainer<Enemy> Enemies {get;}
        public int MaxEnemies {get;}

        public ReverseWedge() {
            MaxEnemies = 5;
            Enemies = new EntityContainer<Enemy>(MaxEnemies);
        }

        public void CreateEnemies(List<Image> enemyStride,
            List<Image> alternativeEnemyStride) {
            Enemies.AddEntity(new Enemy(
                new DynamicShape(new Vec2F(0.25f + 1.0f * 0.1f, 0.7f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStride), new ImageStride(80, alternativeEnemyStride)));
            Enemies.AddEntity(new Enemy(
                new DynamicShape(new Vec2F(0.25f + 2.0f * 0.1f, 0.8f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStride), new ImageStride(80, alternativeEnemyStride)));
            Enemies.AddEntity(new Enemy(
                new DynamicShape(new Vec2F(0.25f + 3.0f * 0.1f, 0.9f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStride), new ImageStride(80, alternativeEnemyStride)));
            Enemies.AddEntity(new Enemy(
                new DynamicShape(new Vec2F(0.25f + 4.0f * 0.1f, 0.8f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStride), new ImageStride(80, alternativeEnemyStride)));
            Enemies.AddEntity(new Enemy(
                new DynamicShape(new Vec2F(0.25f + 5.0f * 0.1f, 0.7f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, enemyStride), new ImageStride(80, alternativeEnemyStride)));
            }
    }
}