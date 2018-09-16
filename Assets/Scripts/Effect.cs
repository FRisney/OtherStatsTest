using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEffect", menuName = "Custom/Effect")]
public class Effect : ScriptableObject {

	public enum EffectType{ 
		LifeDrain, 
		ManaDrain, 
		StaminaDrain, 
		PhysicalResistDrain, 
		MagicalResistDrain, 
		PhysicalDamageDrain,
		MagicalDamageDrain, 
		LifeAugment,
		ManaAugment, 
		StaminaAugment
	}

	public EffectType type;
	public Sprite sprite;
	public new string name;
	public string description;
	public float magnitude;
	public float duration;
	public float remainingTime = 0.0f;

	
}
