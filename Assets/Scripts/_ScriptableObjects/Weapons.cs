using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Weapons : ScriptableObject
{
    public int weaponID;
    public string weaponName;
    public string weaponDescription;
    public int weaponDamage;
    public Sprite weaponImage;
    public AnimationClip weaponAnimation;
    public string collectWeaponMessage;
}
