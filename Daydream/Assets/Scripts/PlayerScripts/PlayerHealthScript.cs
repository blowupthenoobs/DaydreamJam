using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthScript : PlayerSystem
{
    [SerializeField] Image healthBar;

    public void Start()
    {
        playerID.currentHP = playerID.maxHp;
        playerID.Shoot += () => Hurt();
    }

    public void Hurt(int damage = 1)
    {
        playerID.currentHP -= damage;
        UpdateHealthBar();

        if(playerID.currentHP <= 0)
            GameManagerScript.Instance.PlayerDeath();
    }
    public void Heal(int hp = 1)
    {
        playerID.currentHP += hp;
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        float fillPercent = (float)playerID.currentHP / (float)playerID.maxHp;
        healthBar.fillAmount = fillPercent;
    }
}
