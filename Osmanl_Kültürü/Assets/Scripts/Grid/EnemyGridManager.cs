using UnityEngine;

public class EnemyGridManager : MonoBehaviour
{
    public static EnemyGridManager Instance;

    private GameObject[,] cells = new GameObject[6, 4];

    [Header("Prefab")]
    [SerializeField] private GameObject cellPrefab;

    [Header("World Options")]

    [SerializeField] private Transform originPosition;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        CreateDeployCells();
    }

    private void Start()
    {
        GameManager.OnFightStart += OnFightStart;
        GameManager.OnFightEnd += OnFightEnd;
    }

    private void OnFightEnd(object sender, GameManager.OnFightEndEventArgs e)
    {
        CreateDeployCells();
    }

    private void OnFightStart(object sender, GameManager.OnFightStartEventArgs e)
    {
        DestroyDeployCells();
    }

    private void DestroyDeployCells()
    {
        foreach (GameObject cell in cells)
        {
            var deployCell = cell.GetComponent<EnemyDeployCell>();
            if (deployCell != null)
            {
                deployCell.ResetSelection();
            }
        }

        cells = new GameObject[6, 4];
    }

    private void CreateDeployCells()
    {
        for (int x = 0; x < 6; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                var newCell = Instantiate(cellPrefab, originPosition.position + new Vector3(x + (.1f * x), y + (.1f * y)), Quaternion.identity);
                newCell.name = "DeployCell - " + x + ", " + y;
                cells[x, y] = newCell;
            }
        }
    }

    public void ResetSelectedCells()
    {
        foreach (GameObject cell in cells)
        {
            cell.GetComponent<EnemyDeployCell>().ResetSelection();
        }
    }

    public void DeployEnemy(Vector2Int position, SoldierTypeSO soldierTypeSO)
    {
        cells[position.x, position.y].GetComponent<EnemyDeployCell>().DeployEnemy(soldierTypeSO);
    }
}
