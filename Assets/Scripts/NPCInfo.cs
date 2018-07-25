using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNPC", menuName = "Custom/NPC")]
public class NPCInfo : ScriptableObject {
    public string Name;

    public int Strength;
    public int Wisdom;
    public int Dexterity;

	public Weapon ActiveWeapon;
	public Ring LeftRing;
	public Ring RightRing;
	public Head ActiveHead;
	public Body ActiveBody;
	public Legs ActiveLegs;

	private int _MaxHealth;
	private int _MaxMana;
	private int _MaxStamina;

	private int _Level;
	private float _Experience;
	private float _ExperienceCap;

    private float _CurrentHealth;
	private float _CurrentMana;
	private float _CurrentStamina;
	
	public void DefineAtributes(){
		_Level = 1;
		_Experience = 0.0f;
		_ExperienceCap = 90.0f;

		SetSpeed = ((Strength + Dexterity) / 2) * 3;

		_MaxHealth = (Strength * 2) * (_Level * 2);
		_MaxMana = (Wisdom * 2) * (_Level * 2);
		_MaxStamina = (Dexterity * 2) * (_Level * 2);

		_CurrentHealth = _MaxHealth;
		_CurrentMana = _MaxMana;
		_CurrentStamina = _MaxStamina;
	}

	private void UpdateStatsOnLevelUp(){
		//Strenth += 2;
		//Wisdom += 2;
		//Dexterity += 2;
		_MaxHealth = (Strength * 2) * (_Level * 2);
		_MaxMana = (Wisdom * 2) * (_Level * 2);
		_MaxStamina = (Dexterity * 2) * (_Level * 2); 
		
		_CurrentHealth = _MaxHealth;
		_CurrentMana = _MaxMana;
		_CurrentStamina = _MaxStamina;
	}

	public int GetSpeed { get; private set; }
	private int SetSpeed{ set{ GetSpeed = value; } }

    public string GetName{
		get{
			return Name;
		}
	}

	public float AddExp{ 
		set{ 
			_Experience += value; 
			if (_Experience >= _ExperienceCap){ 
				_Level++;
				UpdateStatsOnLevelUp();
				_ExperienceCap *= 0.20f; 
			} 
		} 
	}

    public float CurHealth{
        set {
				if (value > 0) { _CurrentHealth += value; }
				if (value < 0) { _CurrentHealth -= value; }
				if (_CurrentHealth <= 0) { Die(); }
            }
        get { return _CurrentHealth; }
    }

	public float MaxHealth{ get{ return _MaxHealth; } }

	private void Die(){
		//Morreu
		Debug.Log("Morri!");
	}

    public float CurMana{
        set {
				if (value > 0) { _CurrentMana += value; }
				if (value < 0) { _CurrentMana -= value; }
            }
        get { return _CurrentMana; }
	}

	public float MaxMana { get { return _MaxMana; } }

	public float CurStamina{
        set {
				if (value > 0) { _CurrentStamina += value; }
				if (value < 0) { _CurrentStamina -= value; }
            }
        get { return _CurrentStamina; }
	}

	public float MaxStamina { get { return _MaxStamina; } }
}
