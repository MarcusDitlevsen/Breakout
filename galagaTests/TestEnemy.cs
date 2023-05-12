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
using Galaga;
using System;

namespace galagaTests;

public class TestEnemy {
    List<Image>? enemyStridesGreen;
    List<Image>? enemyStrides;
    DynamicShape shape;
    Enemy enemy;

    [SetUp]
    public void Setup() {
        Window.CreateOpenGLContext();

        enemyStrides = ImageStride.CreateStrides(4, Path.Combine("Assets", "Images",
            "BlueMonster.png"));
        enemyStridesGreen = ImageStride.CreateStrides(2, Path.Combine("Assets", "Images",
            "GreenMonster.png"));

        shape = new DynamicShape(new Vec2F(1.0f, 0.5f), new Vec2F(0.1f, 0.1f));

        enemy = new Enemy(shape,
            new ImageStride(80, enemyStrides), new ImageStride(80, enemyStridesGreen));
    }

    [Test]
    public void TestShape() {
        Assert.AreEqual(enemy.GetShape(), shape);
    }

    [TestCase(0.1f)]
    [TestCase(0.2f)]
    [TestCase(0.5f)]
    [TestCase(0.10f)]
    [TestCase(0.100f)]
    public void TestPosition(float amount) {
        float prevPositionY = enemy.GetPosition().Y;
        enemy.GetShape().MoveY(amount);
        float currPositionY = enemy.GetPosition().Y;

        Assert.AreNotEqual(prevPositionY, currPositionY);
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(15)]
    [TestCase(10)]
    [TestCase(100)]
    public void TestHit(int amount) {
        int prevHitpoints = enemy.hitpoints;
        enemy.Hit(amount);
        int currHitpoints = enemy.hitpoints;

        Assert.AreEqual(prevHitpoints - amount, currHitpoints);
    }

    [Test]
    public void TestEnrage() {
        float prevSpeed = enemy.GetSpeed();
        IBaseImage prevImg = enemy.Image;
        
        enemy.Hit(2);
        
        float currSpeed = enemy.GetSpeed();
        IBaseImage currImg = enemy.Image;
        
        Assert.AreEqual(enemy.isEnraged, true);
    }
    
    [TestCase(0.1f)]
    [TestCase(0.2f)]
    [TestCase(0.5f)]
    [TestCase(0.10f)]
    [TestCase(0.100f)]
    public void TestSpeed(float amount) {
        float prevSpeed = enemy.GetSpeed();
        enemy.ChangeSpeed(amount);
        
        Assert.AreEqual(enemy.GetSpeed(), amount);
    }
}