using UnityEngine;

public class Core : Building
{
    private float _life;

    public float Life { get { return _life; } }

    private void Start()
    {
        _life = 100;
        Managers.Instance.Game.CoreHPChangeEvent?.Invoke(_life);
    }

    public void Hit(float damage)
    {
        _life--;

        if(_life <= 0 )
        {
            // 게임오버
        }
         
        Managers.Instance.Game.CoreHPChangeEvent?.Invoke(_life); 
    }
}
