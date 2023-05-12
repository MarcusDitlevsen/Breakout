/*
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
using Breakout;
using System;
using BreakoutEntities;
namespace BreakoutTests;

public class TestPlayer {
    private Player player;
    private float prevPositionX;
    private float currPositionX;


    [SetUp]
    public void Setup(){
        Window.CreateOpenGLContext();

        player = new Player(
            new DynamicShape(new Vec2F(0.4f, 0.1f), new Vec2F(0.2f, 0.3f)),
            new Image(Path.Combine("..", "Breakout", "Assets", "Images", "player.png")));
        
        BreakoutBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);
    }

    [Test]
    public void TestCenterPosition() {
        Assert.AreEqual(player.GetPosition().X == 0.4f, player.GetPosition().Y == 0.1f);
    }

    [Test]
    public void TestMove(){
        prevPositionX = player.GetPosition().X;
        
        BreakoutBus.GetBus().RegisterEvent(
            new GameEvent{
                EventType = GameEventType.PlayerEvent,
                Message = "MOVE_LEFT"
            }
        );

        BreakoutBus.GetBus().ProcessEventsSequentially();

        player.Move();

        currPositionX = player.GetPosition().X;
        Assert.AreNotEqual(prevPositionX, currPositionX);
    }

    [Test]
    public void TestOutOfBounds(){
        player.Shape.SetPosition(new Vec2F (0.0f, 0.0f));

        prevPositionX = player.GetPosition().X;

        BreakoutBus.GetBus().RegisterEvent(
            new GameEvent{
                EventType = GameEventType.PlayerEvent,
                Message = "MOVE_LEFT"
            }
        );

        BreakoutBus.GetBus().ProcessEventsSequentially();

        player.Move();

        currPositionX = player.GetPosition().X;
        Assert.AreEqual(prevPositionX, currPositionX);

    }

    [Test]
    public void TestEntity(){
        Assert.That(player, Is.InstanceOf<Entity>());
    }

    [Test]
    public void TestIsBottomHalf(){
        Assert.LessOrEqual(player.GetPosition().Y, 0.5f);
    }
}
*/