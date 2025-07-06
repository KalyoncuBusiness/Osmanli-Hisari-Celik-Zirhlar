using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public class OnFightStartEventArgs : EventArgs
    {
        public int level;
    }

    public class OnFightEndEventArgs : EventArgs
    {
        public bool isWin;
        public int nextLevel;
    }

    public class OnGameEndEventArgs : EventArgs
    {
        public bool isWin;
    }

    public static event EventHandler<OnFightStartEventArgs> OnFightStart;
    public static event EventHandler<OnFightEndEventArgs> OnFightEnd;
    public static event EventHandler<OnGameEndEventArgs> OnGameEnd;

    private bool _fightIsStarted = false;

    private int _level = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void StartLevel()
    {
        if (!_fightIsStarted)
        {
            if (OnFightStart != null)
            {
                OnFightStart(this, new OnFightStartEventArgs { level = _level });

                _fightIsStarted = true;

                TimeTickManager.OnTick += OnTick;
            }
        }
    }

    private void OnTick(object sender, TimeTickManager.OnTickEventArgs e)
    {
        if (!_fightIsStarted)
        {
            return;
        }

        var soldiers = FindObjectsOfType<Soldier>();

        var enemyCount = 0;
        var allyCount = 0;

        foreach (var soldier in soldiers)
        {
            if (soldier.SoldierTypeSO.isEnemy && soldier.SoldierTypeSO.id != "2e6fedde-a0f6-4e17-8177-5d66f3ab0b32")
            {
                enemyCount++;
            }
            else if (!soldier.SoldierTypeSO.isEnemy && soldier.SoldierTypeSO.id != "cddb4010-0814-480d-9942-9274e17a6dfc")
            {
                allyCount++;
            }
        }

        var isWin = false;

        Debug.Log("enemies: " + enemyCount + ", allies: " + allyCount);

        if (enemyCount > 0 && allyCount <= 0)
        {

        }
        else if (enemyCount <= 0 && allyCount > 0)
        {
            _level++;
            isWin = true;
        }
        else if (enemyCount <= 0 && allyCount <= 0)
        {
            _level++;
            isWin = true;
        }
        else
        {
            return;
        }

        _fightIsStarted = false;

        if (!isWin)
        {
            SoundEffectPlayer.Instance.Lose();
        }
        else
        {
            SoundEffectPlayer.Instance.Win();
        }

        StartCoroutine(DelayedNotify(isWin));
    }

    private IEnumerator DelayedNotify(bool isWin)
    {
        yield return new WaitForSeconds(2);

        var soldiers = FindObjectsOfType<Soldier>();

        foreach (var soldier in soldiers)
        {
            soldier.DestroySelf(false);
        }

        if (_level >= 4 || !isWin)
        {
            OnGameEnd(this, new OnGameEndEventArgs { isWin = isWin });
        }

        OnFightEnd(this, new OnFightEndEventArgs { isWin = isWin, nextLevel = _level });


    }
}
