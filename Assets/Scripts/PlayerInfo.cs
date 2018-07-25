using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Custom/PlayerInstance")]
public class PlayerInfo : ScriptableObject {
	[Header("Name and Image")]
	public Sprite sprite;
    public string Name;
	[Header ("Attributes")]
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
	public List<ScriptableObject> Inventory;

	[Header("Inerent Stats")]
	[SerializeField] private int _MaxHealth;
	[SerializeField] private int _MaxMana;
	[SerializeField] private int _MaxStamina;

	[SerializeField] private int _Level;
	[SerializeField] private float _Experience;
	[SerializeField] private float _ExperienceCap;

	[SerializeField] private float _CurrentHealth;
	[SerializeField] private float _CurrentMana;
	[SerializeField] private float _CurrentStamina;
	
	public void PlayerAttributes(string name,int strength, int wisdom, int dexterity){
		Name = name;

		_Level = 1;
		_Experience = 0.0f;
		_ExperienceCap = 90.0f;

		Strength = strength;
		Wisdom = wisdom;
		Dexterity = dexterity;

		Speed = ((Strength + Dexterity) / 2) * 3;

		_MaxHealth = (Strength * 2) * (_Level * 2);
		_MaxMana = (Wisdom * 2) * (_Level * 2);
		_MaxStamina = (Dexterity * 2) * (_Level * 2);

		_CurrentHealth = _MaxHealth;
		_CurrentMana = _MaxMana;
		_CurrentStamina = _MaxStamina;
	}

	private void UpdateStatsOnLevelUp(){
		Strength ++;
		Wisdom ++;
		Dexterity ++;
		_MaxHealth = (Strength * 2) * (_Level * 2);
		_MaxMana = (Wisdom * 2) * (_Level * 2);
		_MaxStamina = (Dexterity * 2) * (_Level * 2); 
		
		_CurrentHealth = _MaxHealth;
		_CurrentMana = _MaxMana;
		_CurrentStamina = _MaxStamina;
	}

	public int Speed { get; private set; }

	public string GetName{ get{ return Name; } }

	public float AddExp{ 
		set{ 
			_Experience += value; 
			if (_Experience >= _ExperienceCap){ 
				_Level++;
				_Experience -= _ExperienceCap;
				UpdateStatsOnLevelUp();
				_ExperienceCap *= 1.20f; 
			} 
		} 
	}

    public float ModifyHealth{
        set {
				if (value > 0) { _CurrentHealth += value; }
				if (value < 0) { _CurrentHealth -= value; }
				if (_CurrentHealth <= 0) { Die(); }
            }
    }

    public float CurHealth{
        set { _CurrentHealth = value; if (_CurrentHealth <= 0) { Die(); } }
        get { return _CurrentHealth; }
    }

    public float ModifyMana{
        set {
				if (value > 0) { _CurrentMana = value; }
				if (value < 0) { _CurrentMana -= value; }
            }
	}

	public float CurMana{
        set {_CurrentMana = value; }
        get { return _CurrentMana; }
	}

	public float ModifyStamina{
        set {
				if (value > 0) { _CurrentStamina += value; }
				if (value < 0) { _CurrentStamina -= value; }
            }
	}

	public float CurStamina{
        set {
				_CurrentStamina = value;
            }
        get { return _CurrentStamina; }
	}

	public float MaxStamina { get { return _MaxStamina; } }

	public float MaxMana { get { return _MaxMana; } }

	public float MaxHealth{ get{ return _MaxHealth; } }

	private void Die(){ Debug.Log("Morri!"); }
}
