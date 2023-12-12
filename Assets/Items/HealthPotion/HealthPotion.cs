using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : CollectibleItem
{
    public override void Awake()
    {
        itemName = "Health Potion";
        itemID = 1;
        itemIcon = Resources.Load<Sprite>("HealthPotion");
        base.Awake();
    }
}
