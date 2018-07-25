using UnityEngine;

[CreateAssetMenu(fileName = "NewBody", menuName = "Custom/Body")]
public class Body : ScriptableObject
{
	public Sprite Image;
	public string Name;
	public float PhysicalResist;
	public float MagicalResist;
}