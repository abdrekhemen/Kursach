using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    public float maxHealth = 30f;

    public float damageHopSpeed = 3f;

    public float wallCheckDistance = 0.2f;
    public float ledgeCheckDistance = 0.4f;

    public float minAgroDistance = 3f;
    public float maxAgroDistance = 4;

    public float closeRangeActionDistance = 1f;

    public LayerMask ground;
    public LayerMask player;
}
