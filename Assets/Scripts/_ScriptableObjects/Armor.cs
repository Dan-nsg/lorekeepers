using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Armor : ScriptableObject
{
    public int armorID;
    public string armorName;
    public string armorDescription;
    public Sprite armorImage;
    public int armorDefense;
    public string collectArmorMessage;
}
