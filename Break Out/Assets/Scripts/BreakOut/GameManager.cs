using UnityEngine;
using EasyH;

public delegate void ScoreAct(int score);

public class GameManager : Singleton<GameManager>
{
    public IPlayground Playground { get; private set; }

    public int Score { get; private set; }

    public void SetPlayground(IPlayground playground) {
        Playground = playground;
    }

}
