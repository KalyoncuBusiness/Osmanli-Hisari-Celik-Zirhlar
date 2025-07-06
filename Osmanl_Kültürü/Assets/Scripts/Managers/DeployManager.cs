using KalyoncuBusiness.Utils;
using UnityEngine;

public class DeployManager : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (InventoryManager.Instance.SelectedBuilding == null) return;

            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();

            //GridManager.Instance.Build(InventoryManager.Instance.SelectedBuilding, mouseWorldPosition);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();

            //GridManager.Instance.Demolish(mouseWorldPosition);
        }
    }

}
