using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Level: 1")]
    [SerializeField] List<EnemyDeploy> deployList1;

    [Header("Level: 2")]
    [SerializeField] List<EnemyDeploy> deployList2;

    [Header("Level: 3")]
    [SerializeField] List<EnemyDeploy> deployList3;

    [Header("Level: 4")]
    [SerializeField] List<EnemyDeploy> deployList4;

    private bool _isDeployed;

    void Start()
    {
        GameManager.OnFightEnd += OnFightEnd;

        DeployEnemies(0);
        _isDeployed = true;
    }

    private void OnFightEnd(object sender, GameManager.OnFightEndEventArgs e)
    {
        _isDeployed = false;

        DeployEnemies(e.nextLevel);
    }

    private void DeployEnemies(int level)
    {
        if (_isDeployed)
        {
            return;
        }

        if (level == 0)
        {
            foreach (EnemyDeploy enemyDeploy in deployList1)
            {
                EnemyGridManager.Instance.DeployEnemy(enemyDeploy.position, enemyDeploy.soldier);
            }
            _isDeployed = true;
        }
        else if (level == 1)
        {
            foreach (EnemyDeploy enemyDeploy in deployList2)
            {
                EnemyGridManager.Instance.DeployEnemy(enemyDeploy.position, enemyDeploy.soldier);
            }
            _isDeployed = true;
        }
        else if (level == 2)
        {
            foreach (EnemyDeploy enemyDeploy in deployList3)
            {
                EnemyGridManager.Instance.DeployEnemy(enemyDeploy.position, enemyDeploy.soldier);
            }
            _isDeployed = true;
        }
        else if (level == 3)
        {
            foreach (EnemyDeploy enemyDeploy in deployList4)
            {
                EnemyGridManager.Instance.DeployEnemy(enemyDeploy.position, enemyDeploy.soldier);
            }
            _isDeployed = true;
        }
    }
}

[System.Serializable]
public class EnemyDeploy
{
    public SoldierTypeSO soldier;
    public Vector2Int position;
}
