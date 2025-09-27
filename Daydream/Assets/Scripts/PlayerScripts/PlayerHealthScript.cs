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
    }

    public void UpdateHealthBar()
    {
        float fillPercent = (float)playerID.currentHP / (float)playerID.maxHp;
        healthBar.fillAmount = fillPercent;
    }
}
