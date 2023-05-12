using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

public class Score : IGameEventProcessor {
    public int score {get; private set;}
    private Text display;
    public Score (Vec2F position, Vec2F extent) {
        this.score = 0;
        this.display = new Text (score.ToString(), position, extent);
    }

    ///<summary>Adds points to the total score based on value</summary>
    private void AddPoint(int value) {
        score = value < 0 ? score : score += value;
    }

    ///<summary>Renders the score</summary>
    public void RenderScore() {
        display.SetText($"Score: {score}");
        display.SetColor(new Vec3I(0, 255, 0));
        display.RenderText();
    }

    ///<summary>Processes a given game event</summary>
    ///<param name = "gameEvent">The given game event to process</param>
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.StatusEvent) {
            switch (gameEvent.Message) {
                case "ADD_POINT":
                    AddPoint(gameEvent.IntArg1);
                    break;
                
                case "RENDER_SCORE":
                    RenderScore();
                    break;

                default:
                    break;
            }
        }
    }
}
