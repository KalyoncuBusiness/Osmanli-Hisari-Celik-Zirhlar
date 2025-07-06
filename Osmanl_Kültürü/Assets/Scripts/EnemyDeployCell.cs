using UnityEngine;

public class EnemyDeployCell : MonoBehaviour
{
    private Soldier _deployedSoldier;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if (_deployedSoldier != null)
        {
            EnemyGridManager.Instance.ResetSelectedCells();

            _spriteRenderer.color = Color.red;

            Stats.Instance.ShowStats(_deployedSoldier, false);

        }
    }

    public void DeployEnemy(SoldierTypeSO soldierTypeSO)
    {
        var selectedSoldier = soldierTypeSO;

        if (_deployedSoldier == null && selectedSoldier != null)
        {

            _deployedSoldier = Soldier.Create(new Vector3(transform.position.x, transform.position.y, transform.position.z + 1), selectedSoldier);

            _deployedSoldier.transform.SetParent(this.transform);
        }
    }

    public void ResetSelection()
    {
        _spriteRenderer.color = Color.white;
    }

    public void DestroySelf()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);

            child.SetParent(null);
        }

        Destroy(this.gameObject);
    }
}
