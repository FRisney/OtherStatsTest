using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNPC", menuName = "Custom/NPC")]
public class NPCInfo : ScriptableObject {
	[Header("Name and Image")]
	public Sprite sprite;
	public string Name;

	[Header("Attributes")]
	public int Strength;
    public int Wisdom;
    public int Dexterity;

	[Header("Equipment")]
	public Weapon ActiveWeapon;
	public Ring LeftRing;
	public Ring RightRing;
	public Head ActiveHead;
	public Body ActiveBody;
	public Legs ActiveLegs;

	[Header("Inventory")]
	public List<ScriptableObject> inventory;
	public int gold;

	//[Header("Inerent Stats")]
	private int _MaxHealth;
	private int _MaxMana;
	private int _MaxStamina;
	
	public int Level;

    private float _CurrentHealth;
	private float _CurrentMana;
	private float _CurrentStamina;

	private float _PhysicalDamage;
	private float _MagicalDamage;
	private float _PhysicalResist;
	private float _MagicalResist;

	public void SetUp(){
		Speed = ((Strength + Dexterity) / 2) * 3;

		_MaxHealth = (Strength * 2) * (Level * 2);
		_MaxMana = (Wisdom * 2) * (Level * 2);
		_MaxStamina = (Dexterity * 2) * (Level * 2);

		_CurrentHealth = _MaxHealth;
		_CurrentMana = _MaxMana;
		_CurrentStamina = _MaxStamina;
	}
	
	public int Speed { get; private set; }

	public string GetName{ get{ return Name; } }

	// Health Encapsulators
	public float AddHealth{ set { _CurrentHealth += value; if (_CurrentHealth > _MaxHealth) { _CurrentHealth = _MaxHealth; } } }

    public float ReduceHealth{ set { _CurrentHealth -= value; if (_CurrentHealth <= 0) { Die(); _CurrentHealth = 0; } } }

    public float CurHealth{
        set { _CurrentHealth = value; if (_CurrentHealth <= 0) { Die(); } }
        get { return _CurrentHealth; }
    }

	public float GetMaxHealth{ get{ return _MaxHealth; } }

	// Mana Encapsulators
    public float AddMana{ set { _CurrentMana += value; if (_CurrentMana > _MaxMana) { _CurrentMana = _MaxMana; } } }

    public float ReduceMana { set { _CurrentMana -= value; if (_CurrentMana <= 0) { _CurrentMana = 0; } } }

    public float CurMana{
        set { _CurrentMana = value; }
        get { return _CurrentMana; }
    }

	// Stamina Encapsulators
	public float GetMaxMana { get { return _MaxMana; } }

    public float AddStamina { set { _CurrentStamina += value; if (_CurrentStamina > _MaxStamina) { _CurrentStamina = _MaxStamina; } } }

    public float ReduceStamina { set { _CurrentStamina -= value; if (_CurrentStamina <= 0) { _CurrentStamina = 0; } } }

    public float CurStamina{
        set { _CurrentStamina = value; }
        get { return _CurrentStamina; }
    }

	public float GetMaxStamina { get { return _MaxStamina; } }

	private void Die(){ Debug.Log("Morri!"); }
}
