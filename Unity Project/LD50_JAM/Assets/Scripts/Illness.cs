using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Illness", menuName ="AdjAni/Illness")]
public class Illness : ScriptableObject
{
    public int Difficulty;
    public Sprite PopupSprite;
    public string IllnessName;
    public CureType CuredBy = CureType.NONE;
}