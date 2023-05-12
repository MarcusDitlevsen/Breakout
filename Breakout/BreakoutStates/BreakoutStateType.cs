using System;
using DIKUArcade.Input;

namespace BreakoutStates {
    public class StateTransformer {
        public enum GameStateType {
            GameRunning,
            GameRunning2,
            GameRunning3,
            GameRunning4,
            GamePaused,
            MainMenu,
            GameOver,
            GameWon,
        }

        ///<summary>Transforms a string to a game state</summary>
        ///<param name = "state">A string representing a game state</param>
        ///<returns>A GameStateType converted from the given string</returns>
        public static GameStateType TransformStringToState(string state) {
            switch (state) {
                case "GAME_RUNNING":
                    return GameStateType.GameRunning;
                
                case "GAME_RUNNING2":
                    return GameStateType.GameRunning2;
                
                case "GAME_RUNNING3":
                    return GameStateType.GameRunning3;
                
                case "GAME_RUNNING4":
                    return GameStateType.GameRunning4;
                
                case "GAME_PAUSED":
                    return GameStateType.GamePaused;
                
                case "MAIN_MENU":
                    return GameStateType.MainMenu;
                
                case "GAME_OVER":
                    return GameStateType.GameOver;
                
                case "GAME_WON":
                    return GameStateType.GameWon;
                    
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
                
                case GameStateType.GameOver:
                    return "GAME_OVER";
                
                case GameStateType.GameWon:
                    return "GAME_WON";
                    
                default:
                    throw new System.ArgumentException();
            }
        }

        ///<summary>Transforms a given string to a corresponding KeyboardAction</summary>
        ///<param name = "acction">The action string to be converted<param>
        ///<returns>A corresponding KeyboardAction value</returns>
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

        ///<summary>Transforms a given KeyboardAction value to a string</summary>
        ///<param name = "action">The KeyboardAction to transform</param>
        ///<returns>A corresponding string</returns>
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

        ///<summary>Transform a given string to a KeyboardKey value</summary>
        ///<param name = "key">The string to transform</param>
        ///<returns>A correspondng KeyboardKey value</returns>
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

        ///<summary>Transforms a given KeyboardKey value to a string</summary>
        ///<param name = "key">The KeyboardKey value to transform</param>
        ///<returns>A string corresponding to the KeyboardKey</returns>
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

        ///<summary>Transforms a string to an integer</summary>
        ///<param name = "str">The string to transform</param>
        ///<returns>A correspondng integer value</returns>
        public static int TransformStringToInt(string str) {
            return Convert.ToInt32(str);
        }

        ///<summary>Transforms an integer to a string</summary>
        ///<param name = "num">The integer to transform</param>
        ///<returns>A correspondng string</returns>
        public static string TransformIntToString(int num) {
            return Convert.ToString(num);
        }
    }
}