using UnityEngine;

public class User  {
    public string username;
    public string email;
    public int gold;
    public float waterDay; //amount of water to drink per day
    public float health; // current water day
    public float energy; // max energy
    public float current_energy; // current energy
    



    public int score;
    public User() {
    }

    public User(string username, string email) {
        this.username = username;
        this.email = email;
    }
}