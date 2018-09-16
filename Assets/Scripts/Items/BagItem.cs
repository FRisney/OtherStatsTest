using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Custom/Bag Item")]
public class BagItem : ScriptableObject {
	public Sprite sprite;
	public new string name;
	public int price;
}
