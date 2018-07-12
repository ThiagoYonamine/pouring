using UnityEngine;
using System;

public class User  {
    //All atributes needs to be public, because JsonUtility doesnt work with properties
    public string username;
    public string email;
    public int gold;
    public float waterDay; //amount of water to drink per day
    public float health; // current water day
    public float energy; // max energy
    public float current_energy; // current energy

    //ATENTION this var is about POURING! it is last time when pouring consumed water/energy
    //used by decrement energy and water
    public String lastEnergyConsume; 
    public String lastWaterConsume;
    public int score;
    public User() {
    }

    public User(string username, string email) {
        this.username = username;
        this.email = email;
    }
}