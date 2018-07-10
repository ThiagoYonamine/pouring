using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

	// Use this for initialization
	void Start () {

	}
	void Awake(){

		if(!PlayerPrefs.HasKey("userId")){
			SceneManager.LoadSceneAsync("scene_01");
		}
		anim = GetComponent<Animator> ();
		pouring_btn.onClick.AddListener (pouringClick);
		player = new User();
		player.gold = PlayerPrefs.GetInt("gold");
		player.waterDay = PlayerPrefs.GetFloat("waterDay");
		player.health = PlayerPrefs.GetFloat("health");
		player.energy = PlayerPrefs.GetFloat("energy");
		player.current_energy = PlayerPrefs.GetFloat("current_energy");
		player.score = PlayerPrefs.GetInt("score");
		healthBar.MaxValue = player.waterDay;
		healthBar.CurrentValue = player.health;
		energyBar.MaxValue = player.energy;
		energyBar.CurrentValue = player.current_energy;
		updateGold(player.gold);
		DatabaseHandler.writeUser(player);
	}
	// Update is called once per frame
	void Update () {
		
	}

	public void pouringClick(){
		anim.Play (hit);
		GameObject newCoin = Instantiate(coin);
		newCoin.GetComponent<Rigidbody2D>().AddRelativeForce(
			new Vector3(Random.Range(190, 250), Random.Range(-100, 100),0));
		player.gold++;
		player.score++;
		updateGold(player.gold);
		updateEnergy(1);
	}

	void updateGold(int gold){
		gold_txt.text = gold.ToString();
	}

	public void updateLife(int water){
		emoji.play(1);
		player.health += water;
		player.score += 100;
		healthBar.CurrentValue = player.health;
		DatabaseHandler.writeUser(player);
		DatabaseHandler.WriteNewScore(player.score);
	}

	void updateEnergy(int value){
		player.current_energy += value;
		energyBar.CurrentValue = player.current_energy;
	}
}
