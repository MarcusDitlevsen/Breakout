/*
using NUnit.Framework;
using System.IO;
using Breakout;
using BreakoutLevels;
using System;
using DIKUArcade.GUI;
using BreakoutEntities;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using Breakout.BreakoutUtils;

namespace BreakoutTests;

public class TestBlock {
    private Block block;
    private string pathImages = Path.Combine("Assets", "Images");

    [SetUp]
    public void Setup() {
        Window.CreateOpenGLContext();

        block = new DefaultBlock(new StationaryShape(0.0f, 0.0f, 0.0f, 0.0f), new Image(
            Path.Combine(pathImages, "yellow-block.png")), "yellow-block",
            BlockTypes.BlockType.Default, PowerUp.PowerUpType.None);
        
        BreakoutBus.GetBus().Subscribe(GameEventType.StatusEvent, block);
    }

    [TestCase(0, 2)]
    [TestCase(1, 1)]
    [TestCase(2, 0)]
    [TestCase(3, -1)]
    [TestCase(-1, 2)]
    [TestCase(-5, 2)]
    public void TestTakeDamage(int amount, int target) {
        BreakoutBus.GetBus().RegisterEvent(
            new GameEvent{
                EventType = GameEventType.StatusEvent,
                Message = "TAKE_DAMAGE",
                IntArg1 = amount
            }
        );

        BreakoutBus.GetBus().ProcessEventsSequentially();

        Assert.AreEqual(target, block.health);
    }

    [TestCase(0, false)]
    [TestCase(1, false)]
    [TestCase(2, true)]
    [TestCase(3, true)]
    public void TestDestroy(int amount, bool target) {
        BreakoutBus.GetBus().RegisterEvent(
            new GameEvent{
                EventType = GameEventType.StatusEvent,
                Message = "TAKE_DAMAGE",
                IntArg1 = amount
            }
        );

        BreakoutBus.GetBus().ProcessEventsSequentially();

        Assert.AreEqual(block.IsDeleted(), target);
    }
}
*/