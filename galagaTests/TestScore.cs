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


namespace galagaTests;

public class TestScore {
    Score? gameScore;

    [SetUp]
    public void Setup() {
        Window.CreateOpenGLContext();
        gameScore = new Score(new Vec2F(0.0f, 0.0f), new Vec2F(0.0f, 0.0f));
    }

    [Test]
    public void TestScoreIncrement() {
        gameScore?.AddPoint();
        int points = gameScore.score;
        Assert.AreEqual(points, 1);
    }
}