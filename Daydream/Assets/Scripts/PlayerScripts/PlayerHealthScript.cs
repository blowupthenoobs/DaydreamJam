using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class PlayerHealthScript : PlayerSystem
{
    [SerializeField] Image healthBar;
    
    public void Hurt()
    {
        playerID.currentHP--;
    }

    public void UpdateHealthBar()
    {
        float fillPercent = (float)playerID.currentHP / (float)playerID.maxHp;
    }
}
