using NUnit.Framework;
using System.IO;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using System.Security.Principal;
using System.Collections.Generic;
using DIKUArcade.Events;
using DIKUArcade.Physics;
using System;
using Galaga;

namespace galagaTests;

public class TestMovementStrategy {
    ISquadron? formation;
    IMovementStrategy? moveStratNoMove;
    IMovementStrategy? moveStratDown;
    IMovementStrategy? moveStratZigZagDown;
    List<Image>? enemyStridesGreen;
    List<Image>? enemyStrides;
    EntityContainer<Enemy>? enemies;

    [SetUp]
    public void Setup() {
        Window.CreateOpenGLContext();
        formation = new ShieldWall();
        moveStratNoMove = new NoMove();
        moveStratDown = new Down();
        moveStratZigZagDown = new ZigZagDown();

        enemyStrides = ImageStride.CreateStrides(4, Path.Combine("Assets", "Images",
            "BlueMonster.png"));
        enemyStridesGreen = ImageStride.CreateStrides(2, Path.Combine("Assets", "Images",
            "GreenMonster.png"));
        
        formation.CreateEnemies(enemyStrides, enemyStridesGreen);
        enemies = formation.Enemies;
    }

    [Test]
    public void TestNoMove() {
        Vec2F[] positions = new Vec2F[enemies.CountEntities()];
        int i = 0;

        foreach (Enemy enemy in enemies) {
            positions[i] = enemy.GetShape().Position;
            i++;
        }

        moveStratNoMove?.MoveEnemies(enemies);
        i = 0;

        foreach (Enemy enemy in enemies) {
            Assert.True(MathF.Abs(enemy.GetShape().Position.X) == MathF.Abs(positions[i].X)
                && MathF.Abs(enemy.GetShape().Position.Y) == MathF.Abs(positions[i].Y));
            i++;
        }
    }

    [Test]
    public void TestDown() {
        Vec2F[] positions = new Vec2F[enemies.CountEntities()];
        int i = 0;

        foreach (Enemy enemy in enemies) {
            positions[i] = enemy.GetShape().Position;
            i++;
        }

        moveStratDown?.MoveEnemies(enemies);
        i = 0;

        foreach (Enemy enemy in enemies) {
        // Assert call cannot properly get the correct values
        // unless it was changed before the Assert check.
        positions[i].Y -= enemy.GetSpeed();

        Assert.True(MathF.Abs(enemy.GetShape().Position.X) == MathF.Abs(positions[i].X)
            && MathF.Abs(enemy.GetShape().Position.Y) == MathF.Abs(positions[i].Y));
        i++;
        }
    }

    [Test]
    public void TestZigZagDown() {
        Vec2F[] positions = new Vec2F[enemies.CountEntities()];
        int i = 0;
        float p = 0.045f;
        float a = 0.035f;

        foreach (Enemy enemy in enemies) {
            positions[i] = enemy.GetShape().Position;
            i++;
        }

        moveStratZigZagDown?.MoveEnemies(enemies);
        i = 0;

        foreach (Enemy enemy in enemies) {
        positions[i].Y -= enemy.GetSpeed();
        positions[i].X -= (a * (MathF.Sin((2.0f*MathF.PI *
            (enemy.startPositionY- enemy.GetPosition().Y)) / p)));

        Assert.True(MathF.Abs(enemy.GetShape().Position.X) == MathF.Abs(positions[i].X)
            && MathF.Abs(enemy.GetShape().Position.Y) == MathF.Abs(positions[i].Y));
        i++;
        }
    }


}