using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public enum DamageType { LowFall = 40, HighFall = 60, Enemy = 5, BoundaryRejection = 100 };
    [SerializeField]
    private int maxHealth;
    public int currentHealth { get; private set; }
    [SerializeField]
    private GameStateControl gameStateControl;
    [SerializeField]
    private RectTransform healthBar;
    [SerializeField]
    private Image redFlash;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            gameStateControl.SetGameState(GameStateControl.GameState.LevelFailed);
            GetComponentInChildren<Animator>().SetBool("Dead", true);
        }
        if (redFlash.color.a > 0.0f)
        {
            redFlash.color = new Color(1.0f, 0.0f, 0.0f, Mathf.Clamp(redFlash.color.a, 0.0f, 1.0f) - Time.deltaTime);
        }
    }

    public void TakeDamage(DamageType damageType)
    {
        currentHealth -= (int)damageType;
        redFlash.color = new Color(1.0f, 0.0f, 0.0f, (int)damageType * 0.01f);
        ModifyHealthBar();
    }

    private void ModifyHealthBar()
    {
        healthBar.localScale = new Vector3(1.0f, currentHealth * 0.01f, 1.0f);
        healthBar.GetComponent<Image>().color = new Color(1.0f, 0.0f, 0.0f, (100.0f - currentHealth) * 0.01f);
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        ModifyHealthBar();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PelletTracker pellet))
        {
            TakeDamage(HealthManager.DamageType.Enemy);
            other.enabled = false;
        }
    }
}
