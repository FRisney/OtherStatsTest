using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public PlayerInfo playerInstance;
	public Image healthBar;
	public Image manaBar;
	public Image staminaBar;
	public TextMeshProUGUI playerNameText;
	public SpriteRenderer sprite;


	// Use this for initialization
	void Start () {
		playerInstance.PlayerAttributes("RyeSin", 9, 9, 9);
		sprite = GetComponent<SpriteRenderer>();
		sprite.sprite = playerInstance.sprite;
		//healthBar = GetComponent<Image>();
	}
	
	void LateUpdate () {
		// Displays the Name, the Health, the Mana and the Stamina at the moment 
		healthBar.fillAmount = scale(0F, playerInstance.MaxHealth, 0f, 1f, playerInstance.CurHealth);
		manaBar.fillAmount = scale(0F, playerInstance.MaxMana, 0f, 1f, playerInstance.CurMana);
		staminaBar.fillAmount = scale(0F, playerInstance.MaxStamina, 0f, 1f, playerInstance.CurStamina);
		playerNameText.text = playerInstance.CurHealth.ToString();
		// --
	}

	public float scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue){ 
		return( ( ( ( OldValue - OldMin ) * ( NewMax - NewMin ) ) / ( OldMax - OldMin ) ) + NewMin );
	}
}
