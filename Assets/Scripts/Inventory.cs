using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private bool buildMode;
    [SerializeField] private InventoryGuns invGuns;
    [SerializeField] private InventoryStructures invStructures;    

    void Start()
    {
        invGuns = GetComponentInChildren<InventoryGuns>();
        invStructures = GetComponentInChildren<InventoryStructures>();     
    }

    public void Manage()
    {      

        if(Input.GetKeyDown(KeyCode.Q))
        {
            buildMode = !buildMode;            
        }

        if (!buildMode)
        {
            invStructures.DisableItems();
            invGuns.SetActiveCurrentItem();

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                invGuns.ChangeItem(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                invGuns.ChangeItem(2);
            }
            if (Input.GetMouseButton(0))
            {
                invGuns.OnClick();
            }

        }
        else
        {
            invGuns.DisableItems();
            invStructures.SetActiveCurrentItem();
            
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                invStructures.ChangeItem(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                invStructures.ChangeItem(2);
            }
            if (Input.GetMouseButtonDown(0))
            {
                invStructures.OnClick();
            }
        }
    }
}
