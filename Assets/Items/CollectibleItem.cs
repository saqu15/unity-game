using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectibleItem : MonoBehaviour
{
    public string itemName;
    public int itemID;
    public Sprite itemIcon;

    public virtual void Awake()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = itemIcon;
        spriteRenderer.sortingLayerName = "Collectible";
    }
    public virtual void PickUp()
    {
        InventoryController.Instance.AddItem(this);
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