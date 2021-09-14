using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGuns : MonoBehaviour
{
    [SerializeField] private GunLogic currentGun;
    [SerializeField] private int currentGunIndex;
    [SerializeField] private GunLogic[] guns;

    void Start()
    {
        guns = GetComponentsInChildren<GunLogic>();
        DisableItems();
        EnableItem(0);
    }

    public void OnClick()
    {
        currentGun.Shoot();
    }

    public void DisableItems()
    {
        foreach(GunLogic gun in guns)
        {
            gun.gameObject.SetActive(false);
        }
    }

    public void EnableItem(int indexOfItem)
    {
        currentGunIndex = indexOfItem;
        currentGun = guns[currentGunIndex];
        SetActiveCurrentItem();
    }

    public void ChangeItem(int index)
    {
        index--;
        if(index < guns.Length && index >= 0)
        {
            DisableItems();
            EnableItem(index);            
        }
    }

    public void SetActiveCurrentItem()
    {
        currentGun.gameObject.SetActive(true);
    }
}
