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
    public class MainMenu : IGameState {
        private static MainMenu instace = null;
        private Entity backGroundImage;
        private Text[] menuButtons;
        public int activeMenuButton {get; private set;}
        private int maxMenuButtons;
        private IBaseImage image;
        private Vec3I buttonBaseColor;
        private Vec3I buttonSelectedColor;
        private Text mainMenuText;
        private Text newGameButton;
        private Text quitButton;

        
        public static MainMenu GetInstance() {
            if (MainMenu.instace == null) {
                MainMenu.instace = new MainMenu();
                MainMenu.instace.InitializeGameState();
            }
            Console.WriteLine("MainMenu GetInstance() called");
            return MainMenu.instace;
        }

        public void RenderState() {
            backGroundImage.RenderEntity();

            mainMenuText.RenderText();

            if (activeMenuButton == 0) {
                menuButtons[0].SetColor(buttonSelectedColor);
                menuButtons[1].SetColor(buttonBaseColor);
            }
            else {
                menuButtons[1].SetColor(buttonSelectedColor);
                menuButtons[0].SetColor(buttonBaseColor);
            }

            newGameButton.RenderText();

            quitButton.RenderText();
        }

        private void InitializeGameState() {
            image = new Image(Path.Combine("Assets", "Images", "TitleImage.png"));
            backGroundImage = new Entity(
                new StationaryShape(0.0f, 0.0f, 1.0f, 1.0f),
                image);
            
            activeMenuButton = 0;
            maxMenuButtons = 2;
            
            buttonBaseColor = new Vec3I(255, 255, 255);
            buttonSelectedColor = new Vec3I(255, 0, 0);

            // Buttons currently cut-off on the right side.
            // This seems to be a bug with the engine,
            // but we will try to fix it for the next assignment.
            mainMenuText = new Text("Main Menu", new Vec2F(0.3f, 0.35f), new Vec2F(0.3f, 0.4f));
            mainMenuText.SetColor(buttonBaseColor);

            newGameButton = new Text("New Game", new Vec2F(0.3f, 0.2f), new Vec2F(0.3f, 0.4f));

            quitButton = new Text("Quit", new Vec2F(0.3f, 0.1f), new Vec2F(0.3f, 0.4f));

            menuButtons = new Text[maxMenuButtons];
            menuButtons[0] = newGameButton;
            menuButtons[1] = quitButton;
        }

        public void ResetState() {
            activeMenuButton = 0;
            RenderState();
        }

        public void UpdateState() {
            UpdateColors();
        }

        private void UpdateColors() {
            foreach (var button in menuButtons) {
                int i = 0;

                if (i == activeMenuButton) {
                    menuButtons[i].SetColor(buttonSelectedColor);
                }
                else {
                    menuButtons[i].SetColor(buttonBaseColor);
                }
                
                i++;
            }
        }

        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            if (action == KeyboardAction.KeyPress) {
                switch (key) {
                    case KeyboardKey.Up:
                        activeMenuButton = activeMenuButton == 0 ? 0 : activeMenuButton-1;
                        UpdateColors();
                        break;
                    
                    case KeyboardKey.Down:
                        activeMenuButton = activeMenuButton == 1 ? 1 : activeMenuButton+1;
                        UpdateColors();
                        break;
                    
                    case KeyboardKey.Enter:
                        if (activeMenuButton == 0) {
                            GalagaBus.GetBus().RegisterEvent(
                                new GameEvent {
                                    EventType = GameEventType.GameStateEvent,
                                    Message = "CHANGE_STATE",
                                    StringArg1 = "GAME_RUNNING"
                                }
                            );
                        }
                        else {
                            System.Environment.Exit(1);
                        }
                        break;
                    
                    default:
                        break;
                }
            }
        }
    }
}