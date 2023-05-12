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
using System;
using GalagaStates;
using Galaga;

namespace GalagaStates {
    public class StateMachine : IGameEventProcessor {
        public IGameState ActiveState { get; private set; }

        public StateMachine() {
            GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            ActiveState = MainMenu.GetInstance();
            GameRunning.GetInstance();
            GamePaused.GetInstance();
        }

        public void SwitchState(StateTransformer.GameStateType stateType) {
            switch (stateType) {
                case StateTransformer.GameStateType.GameRunning:
                    ActiveState = GameRunning.GetInstance();
                    break;
                
                case StateTransformer.GameStateType.GamePaused:
                    ActiveState = GamePaused.GetInstance();
                    break;
                
                case StateTransformer.GameStateType.MainMenu:
                    ActiveState = MainMenu.GetInstance();
                    break;
                    
                default:
                    throw new System.ArgumentException();
            }
        }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.GameStateEvent) {
                switch (gameEvent.Message) {
                    case "CHANGE_STATE":
                        SwitchState(StateTransformer.TransformStringToState(
                            gameEvent.StringArg1));
                        break;

                    default:
                        break;
                }
            }
        }

        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            switch (action) {
                case KeyboardAction.KeyPress:
                    ActiveState.HandleKeyEvent(KeyboardAction.KeyPress, key);
                    break;
                
                case KeyboardAction.KeyRelease:
                    ActiveState.HandleKeyEvent(KeyboardAction.KeyRelease, key);
                    break;
                }
        }
    }
}