using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryStructures : MonoBehaviour
{
    [SerializeField] private Transform currentStructure;
    [SerializeField] private int currentStructureIndex;
    [SerializeField] private Transform[] structures;    
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        List<Transform> filteredChildren = new List<Transform>();
        foreach(Transform child in GetComponentsInChildren<Transform>())
        {
            if(child.GetComponent<MeshRenderer>())
            {
                filteredChildren.Add(child);
            }
        }        
        structures = filteredChildren.ToArray();
        DisableItems();
        EnableItem(0);
    }

    public void OnClick()
    {        
        if(gameManager.treeQuantity > 2)
        {
            gameManager.treeQuantity = gameManager.treeQuantity - 2;
            Transform newStructure = Instantiate(currentStructure, currentStructure.position, currentStructure.rotation);
            newStructure.GetComponent<Collider>().enabled = true;
        }        
    }

    public void DisableItems()
    {
        foreach(Transform structure in structures)
        {
            structure.gameObject.SetActive(false);
        }
    }

    public void EnableItem(int indexOfItem)
    {
        currentStructureIndex = indexOfItem;
        currentStructure = structures[currentStructureIndex];
        SetActiveCurrentItem();
    }

    public void ChangeItem(int index)
    {
        index--;
        if (index < structures.Length && index >= 0)
        {
            DisableItems();
            EnableItem(index);
        }
    }
    public void SetActiveCurrentItem()
    {
        currentStructure.gameObject.SetActive(true);
    }
}
