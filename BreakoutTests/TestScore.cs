/*
using NUnit.Framework;
using System.IO;
using Breakout;
using BreakoutLevels;
using System;
using DIKUArcade.GUI;
using BreakoutStates;
using DIKUArcade.State;
using DIKUArcade.Events;
using DIKUArcade.Math;

namespace BreakoutTests;

public class TestScore {
    private Score score;

    [SetUp]
    public void Setup() {
        Window.CreateOpenGLContext();
        
        score = new Score(new Vec2F(0.0f, -0.15f), new Vec2F(0.2f, 0.2f));

        BreakoutBus.GetBus().Subscribe(GameEventType.StatusEvent, score);
    }

    [TestCase(0, 0)]
    [TestCase(5, 5)]
    [TestCase(10, 10)]
    [TestCase(100, 100)]
    [TestCase(-1, 0)]
    [TestCase(-5, 0)]
    [TestCase(-10, 0)]
    [TestCase(-100, 0)]
    public void TestAddPoint(int amount, int target) {
        BreakoutBus.GetBus().RegisterEvent(new GameEvent{
            EventType = GameEventType.StatusEvent,
            Message = "ADD_POINT",
            IntArg1 = amount
        });

        BreakoutBus.GetBus().ProcessEventsSequentially();
        
        Assert.AreEqual(score.score, target);
    }

    [Test]
    public void TestOnlyNegativeScore() {
        for (int i = -100; i < 0; i++) {
            BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                EventType = GameEventType.StatusEvent,
                Message = "ADD_POINT",
                IntArg1 = i
            });
        }

        BreakoutBus.GetBus().ProcessEventsSequentially();

        Assert.AreEqual(score.score, 0);
    }

    [Test]
    public void TestOnlyPositiveScore() {
        int total = 0;
        
        for (int i = 1; i < 101; i++) {
            total += i;
            BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                EventType = GameEventType.StatusEvent,
                Message = "ADD_POINT",
                IntArg1 = i
            });

            BreakoutBus.GetBus().ProcessEventsSequentially();
            
            Assert.AreEqual(score.score, total);
        }
    }
}
*/