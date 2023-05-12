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
using Breakout;
using Breakout.BreakoutUtils;

namespace BreakoutStates {
    public class MainMenu : IGameState {
        private static MainMenu instance = null;
        private Entity backgroundImage;
        private Text[] menuButtons;
        public int activeMenuButton {get; private set;}
        private int maxMenuButtons;
        private IBaseImage image;
        private Vec3I buttonBaseColor;
        private Vec3I buttonSelectedColor;
        private Text newGameButton;
        private Text quitButton;

        
        public static MainMenu GetInstance() {
            if (MainMenu.instance == null) {
                MainMenu.instance = new MainMenu();
                MainMenu.instance.InitializeGameState();
            }
            return MainMenu.instance;
        }

        ///<summary>Initializes the current MainMenu state</summary>
        private void InitializeGameState() {
            image = new Image(Path.Combine("Assets", "Images", "shipit_titlescreen.png"));
            backgroundImage = new Entity(
                new StationaryShape(0.0f, 0.0f, 1.0f, 1.0f),
                image);
            
            activeMenuButton = 0;
            maxMenuButtons = 2;
            
            buttonBaseColor = new Vec3I(255, 255, 255);
            buttonSelectedColor = new Vec3I(255, 0, 0);

            newGameButton = new Text("Start", new Vec2F(0.43f, 0.2f), new Vec2F(0.3f, 0.4f));

            quitButton = new Text("Quit", new Vec2F(0.43f, 0.1f), new Vec2F(0.3f, 0.4f));

            menuButtons = new Text[maxMenuButtons];
            menuButtons[0] = newGameButton;
            menuButtons[1] = quitButton;
        }

        ///<summary>Resets the current MainMenu state back to it's original state</summary>
        public void ResetState() {
            activeMenuButton = 0;
            RenderState();
        }

        ///<summary>Renders the objects used in the current MainMenu state</summary>
        public void RenderState() {
            backgroundImage.RenderEntity();

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

        ///<summary>Updates the current GameState every frame</summary>
        public void UpdateState() {
            UpdateColors();
        }

        ///<summary>Updates the colors of the bottons depending on which is selected</summary>
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

        ///<summary>Handles an event depending on what key has been pressed</summary>
        ///<param name = "action">The key action to consider (press or release)</param>
        ///<param name = "key">The KeyboardKey to handle</param>
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
                            BreakoutBus.GetBus().RegisterEvent(
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