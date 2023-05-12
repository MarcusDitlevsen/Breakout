using DIKUArcade;
using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using DIKUArcade.Input;
using Breakout;
using DIKUArcade.Math;

namespace BreakoutStates {
    public class GameOver : IGameState {
        private static GameOver instace = null;
        private Entity backgroundImage;
        private IBaseImage image;
        private Text gameOverText;
        private Text[] menuButtons;
        public int activeMenuButton {get; private set;}
        private int maxMenuButtons;
        private Vec3I buttonBaseColor;
        private Vec3I buttonSelectedColor;
        private Text mainMenuButton;
        private Text quitButton;

        public static GameOver GetInstance() {
            if (GameOver.instace == null) {
                GameOver.instace = new GameOver();
                GameOver.instace.InitializeGameState();
            }
            return GameOver.instace;
        }

        ///<summary>Initializes the current GameOver state</summary>
        private void InitializeGameState() {
            image = new Image(Path.Combine("Assets", "Images", "SpaceBackground.png"));
            backgroundImage = new Entity(new StationaryShape(0.0f, 0.0f, 1.0f, 1.0f), image);

            buttonSelectedColor = new Vec3I(255, 0, 0);
            buttonBaseColor = new Vec3I(255, 255, 255);

            gameOverText = new Text("Game\nOver!", new Vec2F(0.4f, 0.4f), new Vec2F(0.4f, 0.4f));
            gameOverText.SetColor(buttonSelectedColor);

            activeMenuButton = 0;
            maxMenuButtons = 2;

            mainMenuButton = new Text("Menu", new Vec2F(0.43f, 0.2f), new Vec2F(0.3f, 0.4f));

            quitButton = new Text("Quit", new Vec2F(0.43f, 0.1f), new Vec2F(0.3f, 0.4f));

            menuButtons = new Text[maxMenuButtons];
            menuButtons[0] = mainMenuButton;
            menuButtons[1] = quitButton;
        }

        ///<summary>Resets the current GameOver state back to it's original state</summary>
        public void ResetState() {
            InitializeGameState();
        }

        ///<summary>Renders the objects used in the current GameOver state</summary>
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

            gameOverText.RenderText();

            mainMenuButton.RenderText();
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

        ///<summary>Handles a KeyPress event</summary>
        ///<param name = "key">Key which is pressed</param>
        private void KeyPress(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Escape:
                    System.Environment.Exit(1);
                    break;
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
                                    StringArg1 = "MAIN_MENU"
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