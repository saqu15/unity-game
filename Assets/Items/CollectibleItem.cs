using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectibleItem : MonoBehaviour
{
    public CollectibleItemData itemData;

    public virtual void Awake()
    {
        
    }
    public virtual void PickUp()
    {
        InventoryManager.Instance.AddItem(this);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PickUp();
        }
    }
}