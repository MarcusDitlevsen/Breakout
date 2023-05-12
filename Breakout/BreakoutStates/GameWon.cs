using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;

namespace BreakoutStates {
    public class GameWon : IGameState {
        private static GameWon instace = null;
        private Entity backgroundImage;
        private IBaseImage image;
        private Vec3I textColor;
        private Text gameWonText;
        private int score;
        private Text scoreText;

        public static GameWon GetInstance(int score) {
            if (GameWon.instace == null) {
                GameWon.instace = new GameWon(score);
                GameWon.instace.InitializeGameState();
            }
            return GameWon.instace;
        }

        public GameWon(int score) {
            this.score = score;
        }

        ///<summary>Initializes the current GameWon state</summary>
        private void InitializeGameState() {
            image = new Image(Path.Combine("Assets", "Images", "SpaceBackground.png"));
            backgroundImage = new Entity(new StationaryShape(0.0f, 0.0f, 1.0f, 1.0f), image);

            textColor = new Vec3I(255, 0, 0);

            gameWonText = new Text("You\nWon!", new Vec2F(0.4f, 0.4f), new Vec2F(0.4f, 0.4f));
            scoreText = new Text("Score:" + score, new Vec2F(0.4f, 0.2f), new Vec2F(0.4f, 0.4f));
        }

        ///<summary>Resets the current GameWon state back to it's original state</summary>
        public void ResetState() {
            InitializeGameState();
        }

        ///<summary>Renders the objects used in the current GameWon state</summary>
        public void RenderState() {
            backgroundImage.RenderEntity();

            gameWonText.SetColor(textColor);
            scoreText.SetColor(textColor);
            
            gameWonText.RenderText();
            scoreText.RenderText();
        }

        ///<summary>Updates the current GameState every frame</summary>
        public void UpdateState() {
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
            switch (action) {
                case KeyboardAction.KeyPress:
                    KeyPress(key);
                    break;
            }
        }
    }
}