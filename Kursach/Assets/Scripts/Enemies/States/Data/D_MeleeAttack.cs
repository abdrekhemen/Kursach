using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_MeleeAttack : ScriptableObject
{
    public float attackRadius = 0.5f;
    public float attackDamage = 10f;

    public LayerMask player;
}