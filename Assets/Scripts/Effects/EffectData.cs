using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectData : MonoBehaviour
{
    public bool Jump = false;
    public float JumpHeightScale = 1.0f;
    public float JumpPushScale = 1.0f;

    public bool Boost = false;
    public float BoostScale = 1.0f;

    public bool Damage = false;
    public int DamageValue = 100;

    public bool Persistant = true;

    //TODO: which effect add to car
    public bool AddEffect = false;


}
