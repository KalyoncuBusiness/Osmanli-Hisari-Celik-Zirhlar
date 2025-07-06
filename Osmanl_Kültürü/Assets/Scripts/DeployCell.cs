using KalyoncuBusiness.Utils;
using UnityEngine;

public class DeployCell : MonoBehaviour
{
    private Soldier _deployedSoldier;
    private SpriteRenderer _spriteRenderer;

    public static bool _isDragging = false;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if (_isDragging)
        {
            return;
        }

        var selectedSoldier = InventoryManager.Instance.SelectedBuilding;

        if (_deployedSoldier == null && selectedSoldier != null)
        {
            if (MoneyManager.Instance.Money < selectedSoldier.cost)
            {
                UtilsClass.CreateWorldTextPopup(null, "Yeterince Paran Yok!", transform.position, 400, Color.white, new Vector3(transform.position.x, transform.position.y + 2), 2f);
                return;
            }

            _deployedSoldier = Soldier.Create(new Vector3(transform.position.x, transform.position.y, transform.position.z + 1), selectedSoldier);

            _deployedSoldier.transform.SetParent(this.transform);
        }
        else if (_deployedSoldier != null)
        {
            SetSelected();
        }
    }

    //private void OnMouseDrag()
    //{
    //    if (_deployedSoldier == null)
    //    {
    //        return;
    //    }

    //    _isDragging = true;

    //    _deployedSoldier.transform.position = UtilsClass.GetMouseWorldPosition();
    //}

    //private void OnMouseUp()
    //{

    //    if (_deployedSoldier == null)
    //    {
    //        _isDragging = false;
    //        return;
    //    }

    //    _deployedSoldier.GetComponent<Collider2D>().enabled = false;

    //    var rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    //    RaycastHit2D hitInfo = Physics2D.Raycast(rayOrigin, Vector2.zero);

    //    if (hitInfo)
    //    {
    //        var deployCell = hitInfo.transform.GetComponent<DeployCell>();
    //        if (deployCell != null && deployCell.IsFree())
    //        {
    //            _deployedSoldier.transform.position = hitInfo.transform.position + new Vector3(0, 0, 0.01f);
    //            _deployedSoldier.transform.SetParent(deployCell.transform);
    //            deployCell.SetSoldier(_deployedSoldier);
    //            deployCell.SetSelected();
    //            _deployedSoldier = null;
    //        }
    //        else
    //        {

    //            _deployedSoldier.transform.position = transform.position + new Vector3(0, 0, 0.01f);
    //        }
    //    }

    //    _isDragging = false;
    //}

    public void ResetSelection()
    {
        _spriteRenderer.color = Color.white;
    }

    public bool IsFree()
    {
        return _deployedSoldier == null;
    }

    public void SetSoldier(Soldier soldier)
    {
        _deployedSoldier = soldier;

        _deployedSoldier.GetComponent<Collider2D>().enabled = true;
    }

    public void SetSelected()
    {
        GridManager.Instance.ResetSelectedCells();

        _spriteRenderer.color = Color.red;

        Stats.Instance.ShowStats(_deployedSoldier);
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
