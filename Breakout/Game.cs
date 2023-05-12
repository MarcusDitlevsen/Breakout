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
using DIKUArcade.State;
using BreakoutStates;
using System;


namespace Breakout {
    class Game : DIKUGame {
        private StateMachine stateMachine;

        public Game(WindowArgs windowArgs) : base(windowArgs) {
            stateMachine = new StateMachine();
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
            window.SetKeyEventHandler(stateMachine.HandleKeyEvent);
        }

        ///<summary>Renders the active state</summary>
        public override void Render() {
            stateMachine.ActiveState.RenderState();
        }

        ///<summary>Runs every frame and updates the current state</summary>
        public override void Update() {
            BreakoutBus.GetBus().ProcessEventsSequentially();
            stateMachine.ActiveState.UpdateState();
        }
    }
}