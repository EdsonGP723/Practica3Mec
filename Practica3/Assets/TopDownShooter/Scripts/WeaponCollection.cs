using UnityEngine;

[CreateAssetMenu(fileName = "WeaponsCollection", menuName = "ScriptableObjects/WeaponsCollection", order = 0)]

public class WeaponCollection : ScriptableObject
{
    public Weapon[] Weapons;
   
}
