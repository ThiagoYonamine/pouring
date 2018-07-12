using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class PlayerController : MonoBehaviour {
	private Animator anim;
	private int idle = Animator.StringToHash("idle");
	private int hit = Animator.StringToHash("mix");
	public Button pouring_btn;
	public Text gold_txt;

	private User player;

	[SerializeField]
	public Stat healthBar;

	[SerializeField]
	public Stat energyBar;

	public EmojiUtils emoji;

	public GameObject coin;

	//consumes energy
	private int mintuesBetweenEnergy = 1;
	//consumes water
	private int mintuesBetweenWater = 1;

	DatabaseHandler db;
	// Use this for initialization
	void Start () {
		//refreshEnergy();
		//refreshWater();
		//saveUser();
		InvokeRepeating("refreshEnergy", 1f, 1f);
		InvokeRepeating("refreshWater", 1f, 1f);
	}

	void Awake(){
		if(!PlayerPrefs.HasKey("userId")){
			SceneManager.LoadScene("scene_01");
		}

		db = new DatabaseHandler();
		anim = GetComponent<Animator> ();
		pouring_btn.onClick.AddListener (pouringClick);
		player = new User();
		
		player.gold = PlayerPrefs.GetInt("gold");
		player.waterDay = PlayerPrefs.GetFloat("waterDay");
		player.health = PlayerPrefs.GetFloat("health");
		player.energy = PlayerPrefs.GetFloat("energy");
		player.current_energy = PlayerPrefs.GetFloat("current_energy");
		player.score = PlayerPrefs.GetInt("score");
		player.lastEnergyConsume = PlayerPrefs.GetString("lastEnergyConsume");
		player.lastWaterConsume = PlayerPrefs.GetString("lastWaterConsume");
		healthBar.MaxValue = player.waterDay;
		healthBar.CurrentValue = player.health;
		energyBar.MaxValue = player.energy;
		energyBar.CurrentValue = player.current_energy;
		updateGold(player.gold);
	}

	public void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
		//Application.Quit();
		return;
		}
	}
	
	//Consumes energy, executed every minute
	private void refreshEnergy(){
		TimeSpan timeSinceEnergyFill = (DateTime.Now - DateTime.Parse(player.lastEnergyConsume));
		if (timeSinceEnergyFill > TimeSpan.FromMinutes(mintuesBetweenEnergy)){
			int ammount = timeSinceEnergyFill.Minutes/mintuesBetweenEnergy;
			int relativeAmount = Mathf.RoundToInt(player.energy/100);
			relativeAmount *= ammount;
			updateEnergy(-relativeAmount);
			//DateTime nextEnergy = DateTime.Parse(player.lastEnergyConsume) - TimeSpan.FromMinutes(mintuesBetweenEnergy*ammount);
			player.lastEnergyConsume = DateTime.Now.ToString();
			PlayerPrefs.SetString("lastEnergyConsume", player.lastEnergyConsume.ToString());
		}
	}

	//Consumes water, executed every hour 
	//TODO: could be better if i reuse method above
	private void refreshWater(){
		TimeSpan timeSinceWaterFill = (DateTime.Now - DateTime.Parse(player.lastWaterConsume));
		if (timeSinceWaterFill > TimeSpan.FromMinutes(mintuesBetweenWater)){
			int ammount = timeSinceWaterFill.Minutes/mintuesBetweenWater;
			int relativeAmount = Mathf.RoundToInt(player.waterDay/24);
			relativeAmount *= ammount;
			updateLife(-relativeAmount);
			//DateTime nextEnergy = DateTime.Parse(player.lastEnergyConsume) - TimeSpan.FromMinutes(mintuesBetweenEnergy*ammount);
			player.lastWaterConsume = DateTime.Now.ToString();
			PlayerPrefs.SetString("lastWaterConsume", player.lastWaterConsume.ToString());
		}
	}
	public void pouringClick(){
		anim.Play (hit);
		GameObject newCoin = Instantiate(coin);
		newCoin.GetComponent<Rigidbody2D>().AddRelativeForce(
			new Vector3(UnityEngine.Random.Range(190, 250), UnityEngine.Random.Range(-100, 100),0));
		player.gold++;
		player.score++;
		updateGold(player.gold);
		updateEnergy(1);
	}

	void updateGold(int gold){
		gold_txt.text = gold.ToString();
	}

	public void updateLife(int water){
		if(water>0){
			emoji.play(1);
			player.score += 100;
		}

		player.health += water;
		player.health = Mathf.Max(player.health, 0);
		player.health = Mathf.Min(player.health, player.waterDay);
		healthBar.CurrentValue = player.health;
		saveUser();
	}

	void updateEnergy(int value){
		player.current_energy += value;
		player.current_energy = Mathf.Max(player.current_energy, 0);
		player.current_energy = Mathf.Min(player.current_energy, player.energy);
		energyBar.CurrentValue = player.current_energy;
	}

	/*
		Executed every 1 min, or when user drinks a cup
	*/
	private void saveUser(){
		db.writeUser(player);
		db.WriteNewScore(player.score);
	}
}
