using UnityEngine;

[CreateAssetMenu(fileName = "NewHead", menuName = "Custom/Head")]
public class Head : ScriptableObject
{
	public Sprite Image;
	public string Name;
	public float PhysicalResist;
	public float MagicalResist;
}
