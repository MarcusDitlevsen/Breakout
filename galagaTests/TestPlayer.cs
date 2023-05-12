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
using OpenTK.Graphics.OpenGL;
using Galaga;
using System;
using DIKUArcade.State;

namespace galagaTests;

public class TestPlayer {
    //private GameEventBus galagaBus;
    Player? player;
    private float prevPositionX;
    private float currPositionX;


    [SetUp]
    public void Setup() {
        Window.CreateOpenGLContext();
        
        player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "Player.png")));
        
        GalagaBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);
    }

    [Test]
    public void TestPosition() {
        Assert.True((player?.GetPosition().X == 0.45f) &&
            player.GetPosition().Y == 0.1f);
    }

    [Test]
    public void TestMove() {
        prevPositionX = player.GetPosition().X;
        
        GalagaBus.GetBus().RegisterEvent(
            new GameEvent{
                EventType = GameEventType.PlayerEvent,
                Message = "MOVE_LEFT"
            }
        );

        GalagaBus.GetBus().ProcessEventsSequentially();

        player.Move();

        currPositionX = player.GetPosition().X;
        Assert.AreNotEqual(prevPositionX, currPositionX);
    }
}