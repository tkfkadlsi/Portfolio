using UnityEngine;

public class Core : Building
{
    private int _life;

    public int Life { get; private set; }

    public void Hit()
    {
        _life--;

        if(_life <= 0 )
        {
            // 게임오버
        }
    }
}
