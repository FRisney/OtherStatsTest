using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Player", menuName = "Custom/PlayerInstance")]
public class PlayerInfo : ScriptableObject {
	[Header("Name and Image")]
	public Sprite sprite;
    public new string name;

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

	[Header("Effects")]
	public List<ScriptableObject> effects;
	public RectTransform effectHolder;
	public GameObject effectElement;

	[Header("Inventory")]
	public List<BagItem> materials;
	public int gold;

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
	
	[SerializeField] private float _PhysicalDamage;
	[SerializeField] private float _MagicalDamage;
	[SerializeField] private float _PhysicalResist;
	[SerializeField] private float _MagicalResist;
	
	public void PlayerAttributes(string name,int strength, int wisdom, int dexterity){
		this.name = name;

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

	private void Die() { Debug.Log("Morri!"); }

	public int Speed { get; private set; }

	public string GetName{ get{ return name; } }

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

	public float GetExp{ get { return _Experience; } }

	public float GetExpCap{ get { return _ExperienceCap; } }

    public float AddHealth{ set { _CurrentHealth += value; if (_CurrentHealth > _MaxHealth) { _CurrentHealth = _MaxHealth; } } }

    public float ReduceHealth{ set { _CurrentHealth -= value; if (_CurrentHealth <= 0) { Die(); _CurrentHealth = 0; } } }

    public float CurHealth{
        set { _CurrentHealth = value; if (_CurrentHealth <= 0) { Die(); } }
        get { return _CurrentHealth; }
    }

    public float AddMana{ set { _CurrentMana += value; if (_CurrentMana > _MaxMana) { _CurrentMana = _MaxMana; } } }

    public float ReduceMana { set { _CurrentMana -= value; if (_CurrentMana <= 0) { _CurrentMana = 0; } } }

    public float CurMana{
        set { _CurrentMana = value; }
        get { return _CurrentMana; }
    }

    public float AddStamina { set { _CurrentStamina += value; if (_CurrentStamina > _MaxStamina) { _CurrentStamina = _MaxStamina; } } }

    public float ReduceStamina { set { _CurrentStamina -= value; if (_CurrentStamina <= 0) { _CurrentStamina = 0; } } }

    public float CurStamina{
        set { _CurrentStamina = value; }
        get { return _CurrentStamina; }
    }

	public float GetMaxStamina { get { return _MaxStamina; } }

	public float GetMaxMana { get { return _MaxMana; } }

	public float GetMaxHealth{ get{ return _MaxHealth; } }

	public float PhysicalDamage{ set { _PhysicalDamage = value; } get { return _PhysicalDamage; } }

	public float MagicalDamage{ set { _MagicalDamage = value; } get { return _MagicalDamage; } }

	public float PhysicalResist { set { _PhysicalResist = value; } get { return _PhysicalResist; } }

	public float MagicalResist { set { _MagicalResist = value; } get { return _MagicalResist; } }

	public void ManageMaterialInventory(){

	}
	
	public IEnumerator StartBuff(Effect effect){
		bool sera = effects.Contains(effect);
		if(!sera){
			effectHolder = (RectTransform)GameObject.Find("Content").transform;
			var uicoiso = Instantiate(effectElement,effectHolder.transform.position,effectHolder.transform.rotation) as GameObject;
			uicoiso.transform.SetParent(effectHolder.transform,false);
			var effectSprite = uicoiso.GetComponent<Image>();
			effectSprite.sprite = effect.sprite;
			float prBkp = this._PhysicalResist;
			float pdBkp = this._PhysicalDamage;
			float mrBkp = this._MagicalResist;
			float mdBkp = this._MagicalDamage;
			effect.remainingTime = effect.duration;
			effects.Add(effect);
		
			while(effect.remainingTime >= 0){
			
				effect.remainingTime -= Time.deltaTime;

				switch(effect.type){

					case Effect.EffectType.LifeDrain:
						ReduceHealth = effect.magnitude;
					break;
					case Effect.EffectType.LifeAugment:
						AddHealth = effect.magnitude;
					break;
					case Effect.EffectType.ManaDrain:
						ReduceMana = effect.magnitude;
					break;
					case Effect.EffectType.ManaAugment:
						AddMana = effect.magnitude;
					break;
					case Effect.EffectType.StaminaDrain:
						ReduceStamina = effect.magnitude;
					break;
					case Effect.EffectType.StaminaAugment:
						AddStamina = effect.magnitude;
					break;
					case Effect.EffectType.PhysicalResistDrain:
						this._PhysicalResist = prBkp - effect.magnitude;
					break;
					case Effect.EffectType.MagicalResistDrain:
						this._MagicalResist = mrBkp - effect.magnitude;
					break;
					case Effect.EffectType.PhysicalDamageDrain:
						this._PhysicalDamage = pdBkp - effect.magnitude;
					break;
					case Effect.EffectType.MagicalDamageDrain:
						this._MagicalDamage = mdBkp - effect.magnitude;
					break;
				}
				yield return new WaitForEndOfFrame();
			}
			effects.Remove(effect);
			Destroy(uicoiso);
			this._PhysicalResist = prBkp;
			this._PhysicalDamage = pdBkp;
			this._MagicalResist = mrBkp;
			this._MagicalDamage = mdBkp;
		}
	}
}
