using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Custom/Weapon")]
public class Weapon : ScriptableObject
{
	public Sprite Image;
	public string Name;
	public float PhysicalDamage;
	public float StaminaConsume;
	public float MagicalDamage;
	public float ManaConsume;
}
