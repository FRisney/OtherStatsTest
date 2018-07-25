using UnityEngine;

[CreateAssetMenu(fileName = "NewLegs", menuName = "Custom/Legs")]
public class Legs : ScriptableObject
{
	public Sprite Image;
	public string Name;
	public float PhysicalResist;
	public float MagicalResist;
}