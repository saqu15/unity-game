using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsDisplayer : MonoBehaviour
{
    public static StatsDisplayer Instance;
    public Text DamageValue;

    private GameObject player;
    
    private void Awake()
    {
        Instance = this;
        player = GameObject.Find("Player");
        UpdateStats();
    }

    public void UpdateStats()
    {
        int damage = player.GetComponent<PlayerCombat>().GetAttackDamage();
        DamageValue.text = damage.ToString();
    }
}
