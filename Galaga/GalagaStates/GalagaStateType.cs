using System;
using DIKUArcade.Input;

namespace GalagaStates {
    public class StateTransformer {
        public enum GameStateType {
            GameRunning,
            GamePaused,
            MainMenu,
        }

        ///<summary>Transforms a string to a game state</summary>
        ///<param name = "state">A string representing a game state</param>
        ///<returns>A GameStateType converted from the given string</returns>
        public static GameStateType TransformStringToState(string state) {
            switch (state) {
                case "GAME_RUNNING":
                    return GameStateType.GameRunning;
                
                case "GAME_PAUSED":
                    return GameStateType.GamePaused;
                
                case "MAIN_MENU":
                    return GameStateType.MainMenu;
                    
                default:
                    throw new System.ArgumentException();
            }
        }

        ///<summary>Transforms a game state to a string</summary>
        ///<param name = "state">A game state</param>
        ///<returns>A string converted from the given game state</returns>
        public static string TransformStateToString(GameStateType state) {
            switch (state) {
                case GameStateType.GameRunning:
                    return "GAME_RUNNING";
                
                case GameStateType.GamePaused:
                    return "GAME_PAUSED";
                
                case GameStateType.MainMenu:
                    return "MAIN_MENU";
                    
                default:
                    throw new System.ArgumentException();
            }
        }

        public static KeyboardAction TransformStringToAction(string action) {
            switch (action) {
                case "KEY_PRESS":
                    return KeyboardAction.KeyPress;

                case "KEY_RELEASE":
                    return KeyboardAction.KeyRelease;

                default:
                    throw new System.ArgumentException();                
            }
        }

        public static string TransformActionToString(KeyboardAction action) {
            switch (action) {
                case KeyboardAction.KeyPress:
                    return "KEY_PRESS";

                case KeyboardAction.KeyRelease:
                    return "KEY_RELEASE";

                default:
                    throw new System.ArgumentException();                
            }
        }

        public static KeyboardKey TransformStringToKey(string key) {
            switch (key) {
                case "KEY_UP":
                    return KeyboardKey.Up;

                case "KEY_DOWN":
                    return KeyboardKey.Down;

                case "KEY_LEFT":
                    return KeyboardKey.Left;

                case "KEY_RIGHT":
                    return KeyboardKey.Right;
                
                case "KEY_ENTER":
                    return KeyboardKey.Enter;

                default:
                    throw new System.ArgumentException();                
            }
        }

        public static string TransformKeyToString(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Up:
                    return "KEY_UP";

                case KeyboardKey.Down:
                    return "KEY_DOWN";

                case KeyboardKey.Left:
                    return "KEY_LEFT";

                case KeyboardKey.Right:
                    return "KEY_RIGHT";
                
                case KeyboardKey.Enter:
                    return "KEY_ENTER";

                default:
                    throw new System.ArgumentException();                
            }
        }
    }
}