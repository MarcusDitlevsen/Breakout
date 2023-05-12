using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class Score {
    public int score {get; private set;}
    private Text display;
    public Score (Vec2F position, Vec2F extent) {
        score = 0;
        display = new Text (score.ToString(), position, extent);
    }

    ///<summary>Adds a point to the total score</summary>
    public void AddPoint() {
        score++;
    }

    ///<summary>Renders the score</summary>
    public void RenderScore() {
        display.SetText($"Score: {score}");
        display.SetColor(new Vec3I(0, 255, 0));
        display.RenderText();
    }
}
