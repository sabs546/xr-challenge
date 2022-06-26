using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public enum DamageType { LowFall, HighFall, Enemy, BoundaryRejection };
    [SerializeField]
    private int maxHealth;
    public int currentHealth { get; private set; }
    [SerializeField]
    private GameStateControl gameStateControl;
    [SerializeField]
    private RectTransform healthBar;

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
    }

    public void TakeDamage(DamageType damageType)
    {
        switch (damageType)
        {
            case DamageType.LowFall:
                currentHealth -= 40;
                break;
            case DamageType.HighFall:
                currentHealth -= 60;
                break;
            case DamageType.Enemy:
                currentHealth -= 5;
                break;
            case DamageType.BoundaryRejection:
                currentHealth -= maxHealth;
                break;
        }
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
