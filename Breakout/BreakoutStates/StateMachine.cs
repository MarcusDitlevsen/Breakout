using DIKUArcade.Input;
using DIKUArcade.Events;
using DIKUArcade.State;
using Breakout;

namespace BreakoutStates {
    public class StateMachine : IGameEventProcessor {
        public IGameState ActiveState { get; private set; }

        public StateMachine() {
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            ActiveState = MainMenu.GetInstance();
            GameRunning.GetInstance();
            GamePaused.GetInstance();
        }

        ///<summary>Switches the current active state to another</summary>
        ///<param name = "stateType">The state type to switch to</param>
        public void SwitchState(StateTransformer.GameStateType stateType, int IntArg) {
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
                
                case StateTransformer.GameStateType.GameOver:
                    ActiveState = GameOver.GetInstance();
                    break;
                
                case StateTransformer.GameStateType.GameWon:
                    ActiveState = GameWon.GetInstance(IntArg);
                    break;
                    
                default:
                    throw new System.ArgumentException();
            }
        }

        ///<summary>Processes a given game event</summary>
        ///<param name = "gameEvent">The given game event to process</param>
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.GameStateEvent) {
                if (gameEvent.Message == "CHANGE_STATE" && gameEvent.StringArg1 == "GAME_WON") {
                    SwitchState(StateTransformer.TransformStringToState(
                            gameEvent.StringArg1), gameEvent.IntArg1);
                }
                switch (gameEvent.Message) {
                    case "CHANGE_STATE":
                        SwitchState(StateTransformer.TransformStringToState(
                            gameEvent.StringArg1), gameEvent.IntArg1);
                        break;

                    default:
                        break;
                }
            }
        }

        ///<summary>Handles a given key event</summary>
        ///<param name = "action">The action to handle</param>
        ///<param name = "key">The key to handle</param>
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