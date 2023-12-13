using UnityEngine;

[CreateAssetMenu(fileName = "CollectibleItemData", menuName = "ScriptableObjects/CollectibleItemData", order = 1)]
public class CollectibleItemData : ScriptableObject
{
    public string itemName;
    public int itemID;
    public Sprite itemIcon;
}