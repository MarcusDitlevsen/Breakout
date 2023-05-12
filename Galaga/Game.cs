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
using GalagaStates;
using System;
namespace Galaga
{
    public class Game : DIKUGame
    {
        private StateMachine stateMachine;

        public Game(WindowArgs windowArgs) : base(windowArgs) {        
            stateMachine = new StateMachine();
            GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
            window.SetKeyEventHandler(stateMachine.HandleKeyEvent);
        }

        // Move lines 221-231
        ///<summary>Renders the different entities and animations if game is not over</summary>
        public override void Render() {
            stateMachine.ActiveState.RenderState();
        }

        // Move lines 234-242
        ///<summary>Game loop</summary>
        public override void Update() {
            GalagaBus.GetBus().ProcessEventsSequentially();
            stateMachine.ActiveState.UpdateState();
        }
    }
}
