using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RankingController : MonoBehaviour {

	static Dictionary<string, Dictionary<string, int>> userScore;

	
	void Start()
	{
		setScore("thiago","score", 10);
	}
	void Init () {
		if(userScore != null)
			return;

		userScore = new Dictionary<string, Dictionary<string, int>>();
	}
	
	public int getScore(string email, string scoreType){
		Init ();
		if(userScore.ContainsKey(email) ==  false){
			//Not found
			return 0;
		}
		if(userScore[email].ContainsKey(scoreType) ==  false){
			//Not found
			return 0;
		}
		return userScore[email][scoreType];
	}

	public void setScore(string email, string scoreType, int value){
		Init ();
		if(userScore.ContainsKey(email) ==  false){
			userScore[email] = new Dictionary<string, int>();
		}
		userScore[email][scoreType] = value;
	}

	public string[] getRankingNames(){
		Init();
		return userScore.Keys.ToArray();
	}
}
