using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[CreateAssetMenu]
public class PlayerID : ScriptableObject
{
    public bool selected;
    public Action<float> Attack;
    public Action<float> Dash;
}
