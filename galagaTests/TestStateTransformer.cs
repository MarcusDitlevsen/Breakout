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
using System;


namespace galagaTests;

public class TestStateTransformer {
    private StateTransformer.GameStateType[]? gameStates;
    private string[]? gameStatesString;

    [SetUp]
    public void Setup() {
        Window.CreateOpenGLContext();

        gameStates = new StateTransformer.GameStateType[3] {
            StateTransformer.GameStateType.GameRunning,
            StateTransformer.GameStateType.GamePaused,
            StateTransformer.GameStateType.MainMenu};
        
        gameStatesString = new string[3] {
            "GAME_RUNNING",
            "GAME_PAUSED",
            "MAIN_MENU"};
    }

    [TestCase("GAME_RUNNING", 0)]
    [TestCase("GAME_PAUSED", 1)]
    [TestCase("MAIN_MENU", 2)]
    public void TestTransformToState(string state, int i) {
        
        Assert.AreEqual(StateTransformer.TransformStringToState(state),
            gameStates?[i]);
    }

    [TestCase(StateTransformer.GameStateType.GameRunning, 0)]
    [TestCase(StateTransformer.GameStateType.GamePaused, 1)]
    [TestCase(StateTransformer.GameStateType.MainMenu, 2)]
    public void TestTransformToState(StateTransformer.GameStateType state, int i) {
        
        Assert.AreEqual(StateTransformer.TransformStateToString(state),
            gameStatesString?[i]);
    }
}