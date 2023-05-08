using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapons", menuName = "ScriptableObjects/Weapons", order = 0)]
public class Weapon : ScriptableObject
{
    public Sprite sprite;
    public float knockBack;
    public Vector3 ShootPoint;
    public float bulletSpeed;
    public float shootingTime;

    
}
