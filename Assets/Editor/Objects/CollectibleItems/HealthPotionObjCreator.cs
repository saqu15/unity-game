using UnityEditor;
using UnityEngine;

public class HealthPotionObjCreator
{
    [MenuItem("GameObject/Collectible/HealthPotion")]
    public static void CreateHealthPotionObj(MenuCommand menuCommand)
    {
        GameObject healthPotionObj = new GameObject("HealthPotion");

        BoxCollider2D boxCollider = healthPotionObj.AddComponent<BoxCollider2D>();
        SpriteRenderer spriteRenderer = healthPotionObj.AddComponent<SpriteRenderer>();
        Rigidbody2D rigidbody2D = healthPotionObj.AddComponent<Rigidbody2D>();
        HealthPotion healthPotionScript = healthPotionObj.AddComponent<HealthPotion>();

        boxCollider.isTrigger = true;
        rigidbody2D.bodyType = RigidbodyType2D.Static;

        healthPotionScript.itemData = Resources.Load<CollectibleItemData>("HealthPotionData");

        GameObjectUtility.SetParentAndAlign(healthPotionObj, menuCommand.context as GameObject);

        Selection.activeObject = healthPotionObj;
    }
}