using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Campfire : MonoBehaviour
{
    public CampfireData itemData;
    public Text interactionText;
    public GameObject campfirePanel;
    public Image blackscreen;
    private SpriteRenderer spriteRenderer;

    private bool nearCampFire = false;
    private KeyCode interactionKey = KeyCode.E;
    private GameObject player;
    private Animator animator;

    private bool USED = false;
    
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
        animator = player.GetComponent<Animator>();
    }

    void Start()
    {
        HidePanel();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            nearCampFire = true;
            interactionText.text = "Naciśnij E";
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            nearCampFire = false;
            interactionText.text = "";
        }
    }

    void Update()
    {
        if(USED)
        {
            Destroy(gameObject);
        }
        if (nearCampFire)
        {
            if (Input.GetKeyDown(interactionKey))
            {   
                interactionText.text = "";
                ShowPanel();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                HidePanel();
            }
        }
        
    }
    void ShowPanel()
    {
        player.GetComponent<CharacterController2D>().enabled = false;
        player.GetComponent<PlayerCombat>().enabled = false;
        animator.SetBool("IsMovingRight", false);
        animator.SetBool("IsMovingLeft", false);
        animator.SetBool("IsJumping", false);
        campfirePanel.SetActive(true);
        Button restButton = campfirePanel.transform.Find("RestButton").GetComponent<Button>();
        restButton.onClick.RemoveAllListeners();
        restButton.onClick.AddListener(() =>
        {
            HidePanel();
            animator.SetTrigger("Rest");
            StartCoroutine(UseCampfire());
            interactionText.text = "";
            HealthBar.Instance.SetHealth(HealthBar.Instance.GetMaxHealth());
        });

        Button prayButton = campfirePanel.transform.Find("PrayButton").GetComponent<Button>();
        prayButton.onClick.RemoveAllListeners();
        prayButton.onClick.AddListener(() =>
        {
            HidePanel();
            animator.SetTrigger("Pray");
            StartCoroutine(UseCampfire());
            interactionText.text = "";
            int damage = player.GetComponent<PlayerCombat>().GetAttackDamage();
            damage = (int)(itemData.damageMulti * damage);
            player.GetComponent<PlayerCombat>().SetAttackDamage(damage);
            StatsDisplayer.Instance.UpdateStats();
        });
    }
    void HidePanel()
    {
        player.GetComponent<CharacterController2D>().enabled = true;
        player.GetComponent<PlayerCombat>().enabled = true;
        campfirePanel.SetActive(false);
        if (nearCampFire)
        {
            interactionText.text = "Naciśnij E";
        }
    }
    IEnumerator UseCampfire()
    {
        float timeDuration = 2f; 
        float timeStart = Time.time;
        blackscreen.GetComponent<UnityEngine.UI.Image>().enabled = true;
        player.GetComponent<CharacterController2D>().enabled = false;
        while (Time.time - timeStart < timeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, (Time.time - timeStart) / timeDuration);
            blackscreen.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }
        
        timeStart = Time.time;
        spriteRenderer.enabled = false;

        while (Time.time - timeStart < timeDuration * 2)
        {
            float alpha = Mathf.Lerp(1f, 0f, (Time.time - timeStart - timeDuration) / timeDuration);
            blackscreen.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }
        blackscreen.GetComponent<UnityEngine.UI.Image>().enabled = false;
        player.GetComponent<CharacterController2D>().enabled = true;
        USED = true;
    }
    
}
