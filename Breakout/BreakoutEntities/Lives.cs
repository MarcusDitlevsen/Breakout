using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

public class Lives : IGameEventProcessor {
    public int lives {get; private set;}
    private Text display;

    ///<summary> How many lives the player has </summary>
    ///<param name = "position"> The position of the "lives" text on the screen </param>
    ///<param name = "extent"> The size of the text </param>
    public Lives (Vec2F position, Vec2F extent) {
        this.lives = 3;
        this.display = new Text (lives.ToString(), position, extent);
    }

    ///<summary> Removes a life point </summary>
    ///<param name = "value"> value for the lives removed </param>
    public void RemoveLife(int value) {
        lives = lives-- < 0 ? 0 : lives--;
    }

    ///<summary> Adds a life point </summary>
    ///<param name = "value"> value for the lives added </param>
    public void AddLife(int amount) {
        lives = lives + amount > 0 ? lives + amount : 0;
    }

    ///<summary> Resets the player's lives back to 3 </summary>
    public void ResetLives() {
        lives = 3;
    }

    ///<summary>Renders the lives</summary>
    public void RenderLives() {
        display.SetText($"Lives: {lives}");
        display.SetColor(new Vec3I(0, 255, 0));
        display.RenderText();
    }

    ///<summary>Processes a given game event</summary>
    ///<param name = "gameEvent">The given game event to process</param>
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.StatusEvent) {
            switch (gameEvent.Message) {
                case "REMOVE_LIFE":
                    RemoveLife(gameEvent.IntArg1);
                    break;
                
                case "RENDER_LIVES":
                    RenderLives();
                    break;

                default:
                    break;
            }
        }
    }
}
