using UnityEngine;

[CreateAssetMenu]
public class WeaponData : ScriptableObject
{
    public Sprite WeaponSprite;
    public DamageHealth DamageObject1;
    public DamageHealth DamageObject2;
    public DamageHealth DamageObject3;
    public WeaponType WeaponType;
    public float Damage = 1;
    public float Range = 3;
    public float Knockback = 250;
    public float SpeedModifier = -0.5f;
    public bool CanBeDualWielded;
}