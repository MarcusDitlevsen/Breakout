using System.IO;
using DIKUArcade;
using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.Math;
using System;
using Galaga;

namespace GalagaStates {
    public class GamePaused : IGameState {
        private static GamePaused instace = null;
        private Text[] menuButtons;
        public int activeMenuButton {get; private set;}
        private int maxMenuButtons;
        private Vec3I buttonBaseColor;
        private Vec3I buttonSelectedColor;
        private Text pausedText;
        private Text continueButton;
        private Text mainMenuButton;
        
        public static GamePaused GetInstance() {
            if (GamePaused.instace == null) {
                GamePaused.instace = new GamePaused();
                GamePaused.instace.InitializeGameState();
            }
            return GamePaused.instace;
        }

        public void RenderState() {
            pausedText.SetText("Game Paused");
            pausedText.RenderText();

            continueButton.SetText("Continue");
            continueButton.RenderText();

            mainMenuButton.SetText("Main Menu");
            mainMenuButton.RenderText();
        }

        private void InitializeGameState() {
            activeMenuButton = 0;
            maxMenuButtons = 2;
            
            buttonBaseColor = new Vec3I(0, 0, 0);
            buttonSelectedColor = new Vec3I(255, 0, 0);

            pausedText = new Text("Game Paused", new Vec2F(0.3f, 0.35f), new Vec2F(0.4f, 0.5f));
            pausedText.SetColor(buttonBaseColor);

            continueButton = new Text("Continue", new Vec2F(0.3f, 0.2f), new Vec2F(0.4f, 0.5f));
            continueButton.SetColor(buttonSelectedColor);

            mainMenuButton = new Text("Main Menu", new Vec2F(0.3f, 0.1f), new Vec2F(0.4f, 0.5f));
            mainMenuButton.SetColor(buttonBaseColor);

            menuButtons = new Text[maxMenuButtons];
            menuButtons[0] = continueButton;
            menuButtons[1] = mainMenuButton;
        }

        public void ResetState() {
            activeMenuButton = 0;
            continueButton.SetColor(buttonSelectedColor);
            mainMenuButton.SetColor(buttonBaseColor);
        }

        public void UpdateState() {
        }

        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Up:
                    activeMenuButton = activeMenuButton == 0 ? 0 : activeMenuButton-1;
                    menuButtons[activeMenuButton].SetColor(buttonSelectedColor);
                    menuButtons[activeMenuButton + 1].SetColor(buttonBaseColor);
                    break;
                
                case KeyboardKey.Down:
                    activeMenuButton = activeMenuButton == 1 ? 1 : activeMenuButton+1;
                    menuButtons[activeMenuButton].SetColor(buttonSelectedColor);
                    menuButtons[activeMenuButton - 1].SetColor(buttonBaseColor);
                    break;
                
                case KeyboardKey.Enter:
                    if (activeMenuButton == 1) {
                        GalagaBus.GetBus().RegisterEvent(
                            new GameEvent {
                                EventType = GameEventType.GameStateEvent,
                                Message = "CHANGE_STATE",
                                StringArg1 = "MAIN_MENU"
                            }
                        );
                    }
                    else {
                        GalagaBus.GetBus().RegisterEvent(
                            new GameEvent {
                                EventType = GameEventType.GameStateEvent,
                                Message = "CHANGE_STATE",
                                StringArg1 = "GAME_RUNNING"
                            }
                        );
                    }
                    break;
                
                default:
                    break;
            }
        }
    }
}