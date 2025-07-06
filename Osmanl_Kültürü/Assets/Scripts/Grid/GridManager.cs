using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    private List<GameObject> cells = new List<GameObject>();

    [Header("Prefab")]
    [SerializeField] private GameObject cellPrefab;

    [Header("World Options")]

    [SerializeField] private Transform originPosition;

    [SerializeField] private int horizontalSize = 6;
    [SerializeField] private int verticalSize = 4;

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

    public void ResetSelectedCells()
    {
        foreach (GameObject cell in cells)
        {
            var deployCell = cell.GetComponent<DeployCell>();
            if (deployCell != null)
            {
                deployCell.ResetSelection();
            }
        }
    }

    private void DestroyDeployCells()
    {
        foreach (var cell in cells)
        {
            var deployCell = cell.GetComponent<DeployCell>();
            if (deployCell != null)
            {
                deployCell.DestroySelf();
            }
        }

        cells = new List<GameObject>();
    }

    private void CreateDeployCells()
    {
        for (int x = 0; x < horizontalSize; x++)
        {
            for (int y = 0; y < verticalSize; y++)
            {
                var newCell = Instantiate(cellPrefab, originPosition.position + new Vector3(x + (.1f * x), y + (.1f * y)), Quaternion.identity);
                newCell.name = "DeployCell - " + x + ", " + y;
                cells.Add(newCell);
            }
        }
    }
}
