using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterStatsModifier : ScriptableObject
{
    public abstract void AffectCharacter(GameObject character, int val);
}
