using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Core : Building
{
    [SerializeField] private List<GameObject> _fires;

    private float _life;

    public float Life { get { return _life; } }


    private void OnEnable()
    {
        Managers.Instance.Game.BeatEvent += BeatHandler;
    }

    private void OnDisable()
    {
        if(Managers.Instance != null)
        {
            Managers.Instance.Game.BeatEvent -= BeatHandler;
        }
    }

    private void Start()
    {
        _life = 100;
        Managers.Instance.Game.CoreHPChangeEvent?.Invoke(_life);
        Managers.Instance.Game.LiveTime = 0f;
        Managers.Instance.Game.KillCount = 0;

        foreach (GameObject fire in _fires)
        {
            fire.SetActive(false);
        }
    }

    private void Update()
    {
        Managers.Instance.Game.LiveTime += Time.deltaTime;
    }

    public void Hit(float damage)
    {
        _life -= damage;

        CheckLife(_fires[0], 50f);
        CheckLife(_fires[1], 30f);
        CheckLife(_fires[2], 15f);

        if(_life <= 0 )
        {
            SceneManager.LoadSceneAsync("ResultScene");
        }
         
        Managers.Instance.Game.CoreHPChangeEvent?.Invoke(_life); 
    }

    private void CheckLife(GameObject fire, float life)
    {
        if(_life < life)
        {
            fire.SetActive(true);
        }
        else
        {
            fire.SetActive(false);
        }
    }

    private void BeatHandler()
    {
        Vector3 position = transform.position + Vector3.up * 2;
        Managers.Instance.Pool.PopObject(PoolType.BeatEffect, position);
    }
}
