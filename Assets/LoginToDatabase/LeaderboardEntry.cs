using System.Collections;
using System.Collections.Generic;
public class LeaderboardEntry {
    public string email;
    public int score = 0;

    public LeaderboardEntry() {
    }

    public LeaderboardEntry(string email, int score) {
        this.email = email;
        this.score = score;
    }

    public Dictionary<string, object> ToDictionary() {
        Dictionary<string, object> result = new Dictionary<string, object>();
        result["email"] = email;
        result["score"] = score;

        return result;
    }
}