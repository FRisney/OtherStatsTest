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
	public Image expBar;
	public TextMeshProUGUI playerNameText;
	public SpriteRenderer sprite;
	public float t = 0.3f;

	// Use this for initialization
	void Start () {
		playerInstance.PlayerAttributes("RyeSin", 9, 9, 9);
		sprite = GetComponent<SpriteRenderer>();
		sprite.sprite = playerInstance.sprite;
		//healthBar = GetComponent<Image>();
	}
	
	void LateUpdate () {
		// Displays the Name, the Health, the Mana and the Stamina at the moment
		healthBar.fillAmount = Mathf.SmoothStep(healthBar.fillAmount, scale(0F, playerInstance.GetMaxHealth, 0f, 1f, playerInstance.CurHealth), t);
		manaBar.fillAmount = Mathf.SmoothStep(manaBar.fillAmount, scale(0F, playerInstance.GetMaxMana, 0f, 1f, playerInstance.CurMana), t);
		staminaBar.fillAmount = Mathf.SmoothStep(staminaBar.fillAmount, scale(0F, playerInstance.GetMaxStamina, 0f, 1f, playerInstance.CurStamina), t);
		expBar.fillAmount = Mathf.SmoothStep(expBar.fillAmount, scale(0F, playerInstance.GetExpCap, 0f, 1f, playerInstance.GetExp), t);
		playerNameText.text = Mathf.Floor(playerInstance.GetExp).ToString();
		// --
	}

	public float scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue){
		return ((((OldValue - OldMin) * (NewMax - NewMin)) / (OldMax - OldMin)) + NewMin);
	}
}
