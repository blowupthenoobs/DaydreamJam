using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.InputSystem;

[CreateAssetMenu]
public class PlayerID : ScriptableObject
{
    public Action<float> Attack;
    public Action<float> Dash;
    public Action Shoot;

    public int maxHp;
    public int currentHP;
}
