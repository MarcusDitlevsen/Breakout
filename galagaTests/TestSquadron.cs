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

public class TestSquadron {
    ISquadron? squadWedge;
    ISquadron? squadReverseWedge;
    ISquadron? squadShieldWall;
    List<Image>? enemyStridesGreen;
    List<Image>? enemyStrides;
    EntityContainer<Enemy>? enemies;


    [SetUp]
    public void Setup() {
        Window.CreateOpenGLContext();
        squadWedge = new Wedge();
        squadReverseWedge = new ReverseWedge();
        squadShieldWall = new ShieldWall();

        enemyStrides = ImageStride.CreateStrides(4, Path.Combine("Assets", "Images",
            "BlueMonster.png"));
        enemyStridesGreen = ImageStride.CreateStrides(2, Path.Combine("Assets", "Images",
            "GreenMonster.png"));
        
        squadWedge.CreateEnemies(enemyStrides, enemyStridesGreen);
        squadReverseWedge.CreateEnemies(enemyStrides, enemyStridesGreen);
        squadShieldWall.CreateEnemies(enemyStrides, enemyStridesGreen);
    }

    [Test]
    public void TestWedgeEnemyCount() {
        Assert.AreEqual(squadWedge?.MaxEnemies, 5);
    }

    [Test]
    public void TestReverseWedgeEnemyCount() {
        Assert.AreEqual(squadReverseWedge?.MaxEnemies, 5);
    }

    [Test]
    public void TestShieldWallEnemyCount() {
        Assert.AreEqual(squadShieldWall?.MaxEnemies, 6);
    }
}