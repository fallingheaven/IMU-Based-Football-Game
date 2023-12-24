
using System.Collections;

public class ScoreInfo: IComparer
{
    public string playerName;
    public Difficulty difficulty;
    public float score;

    public ScoreInfo(string name, Difficulty diff, float sco)
    {
        playerName = name;
        difficulty = diff;
        score = sco;
    }

    public int Compare(object x, object y)
    {
        var a = (ScoreInfo)x;
        var b = (ScoreInfo)y;

        if (a == null || b == null) return 0;
        
        if (a.difficulty != b.difficulty)
        {
            if (a.difficulty > b.difficulty)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
        else
        {
            if (a.score > b.score)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
    }
}
