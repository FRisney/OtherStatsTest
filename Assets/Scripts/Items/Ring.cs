using UnityEngine;

[CreateAssetMenu(fileName = "NewRing", menuName = "Custom/Ring")]
public class Ring : ScriptableObject
{
	public Sprite Image;
	public string Name;
	public float HealthMod;
	public float ManaMod;
	public float StaminaMod;
}
