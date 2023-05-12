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
using GalagaStates;
using Galaga;
using System;


namespace galagaTests;

public class TestStateMachine {
    private StateMachine? stateMachine;

    [SetUp]
    public void Setup() {
        Window.CreateOpenGLContext();

        stateMachine = new StateMachine();


        GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
    }

    [Test]
    public void TestInitialState() {
        Assert.That(stateMachine?.ActiveState, Is.InstanceOf<MainMenu>());
    }

    [Test]
    public void TestSwitchState() {
        GalagaBus.GetBus().RegisterEvent(
            new GameEvent{
                EventType = GameEventType.GameStateEvent,
                Message = "CHANGE_STATE",
                StringArg1 = "GAME_RUNNING"
            }
        );
        
        GalagaBus.GetBus().ProcessEventsSequentially();
        Assert.That(stateMachine?.ActiveState, Is.InstanceOf<GameRunning>());
    }
}