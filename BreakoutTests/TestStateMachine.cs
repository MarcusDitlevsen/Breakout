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

namespace BreakoutTests;

public class TestStateMachine {
    private StateMachine stateMachine;

    [SetUp]
    public void Setup() {
        Window.CreateOpenGLContext();
        
        stateMachine = new StateMachine();

        BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
    }

    [TestCase("GAME_RUNNING")]
    [TestCase("GAME_PAUSED")]
    [TestCase("MAIN_MENU")]
    public void TestSwitchState(string state) {
        // StateMachine starts with MainMenu initialized
        IGameState activeState = MainMenu.GetInstance();
    
        switch (state) {
            case "GAME_RUNNING":
                activeState = GameRunning.GetInstance();
                break;
            
            case "GAME_PAUSED":
                activeState = GamePaused.GetInstance();
                break;
            
            case "MAIN_MENU":
                activeState = MainMenu.GetInstance();
                break;
        }

        BreakoutBus.GetBus().RegisterEvent(new GameEvent{
            EventType = GameEventType.GameStateEvent,
            Message = "CHANGE_STATE",
            StringArg1 = state
        });

        BreakoutBus.GetBus().ProcessEventsSequentially();
        
        Assert.AreEqual(stateMachine.ActiveState, activeState);
    }

    
}
*/