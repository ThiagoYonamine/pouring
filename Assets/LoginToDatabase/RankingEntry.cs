using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RankingEntry : MonoBehaviour {

	public GameObject rankingEntry;
	public GameObject rankingEntry2;
	List<LeaderboardEntry> result;

	RankingController rc;
	private bool refresh;

	// Use this for initialization
	void Start () {
 		rc = GameObject.FindObjectOfType<RankingController>();
		result = DatabaseHandler.GetRanking();
		refresh = true;

	}
	// Update is called once per frame
	void Update () {
		if(result.Count > 0 && refresh){
			refreshRanking();
		}
	}

	void refreshRanking(){
		int position = 1;
		result.Reverse();
		foreach(LeaderboardEntry rank in result){
			GameObject go;
			if((position%2)==0){
			 go = Instantiate(rankingEntry);
			}
			else{
			 go = Instantiate(rankingEntry2);
			}

			go.transform.SetParent(this.transform,false);
			go.transform.Find("email").GetComponent<Text>().text = rank.email;
			go.transform.Find("score").GetComponent<Text>().text = rank.score.ToString();
			go.transform.Find("Rank").GetComponent<Text>().text = position.ToString();
			position++;
		}
		refresh = false;
	}

}
