// Copyright 2016 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// Handler for UI buttons on the scene.  Also performs some
// necessary setup (initializing the firebase app, etc) on
// startup.
public class DatabaseHandler : MonoBehaviour {
  DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
  // When the app starts, check to make sure that we have
  // the required dependencies to use Firebase, and if not,
  // add them if possible.
  void Start() {
    FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
      dependencyStatus = task.Result;
      if (dependencyStatus == DependencyStatus.Available) {
        InitializeFirebase();
      } else {
        Debug.LogError(
          "Could not resolve all Firebase dependencies: " + dependencyStatus);
      }
    });
  }

  // Initialize the Firebase database:
  protected virtual void InitializeFirebase() {
    FirebaseApp app = FirebaseApp.DefaultInstance;
    app.SetEditorDatabaseUrl("https://pouring-223db.firebaseio.com/");
    if (app.Options.DatabaseUrl != null) app.SetEditorDatabaseUrl(app.Options.DatabaseUrl);
  }
  // Exit if escape (or back, on mobile) is pressed.
  void Update() {
    if (Input.GetKeyDown(KeyCode.Escape)) {
      Application.Quit();
    }
  }

  public static void writeUser(User player) {
   player.email = PlayerPrefs.GetString("email");
   string userId  = PlayerPrefs.GetString("userId");
   string json = JsonUtility.ToJson(player);
   PlayerPrefs.SetInt("gold", player.gold);
	 PlayerPrefs.SetFloat("waterDay", player.waterDay);
	 PlayerPrefs.SetFloat("energy", player.energy);
	 PlayerPrefs.SetInt("score", player.score);
   PlayerPrefs.SetFloat("health", player.health);
   PlayerPrefs.SetFloat("current_energy",player.current_energy);
   DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
   reference.Child("users").Child(userId).SetRawJsonValueAsync(json);
   
  }

  public static void WriteNewScore(int score) {
    string email = PlayerPrefs.GetString("email");
    string userId  =  PlayerPrefs.GetString("userId");
    LeaderboardEntry rank = new LeaderboardEntry(email, score);
    string json = JsonUtility.ToJson(rank);

    DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
    reference.Child("ranking").Child(userId).SetRawJsonValueAsync(json);
  }
  public static List<LeaderboardEntry> GetRanking(){
      List<LeaderboardEntry> response = new List<LeaderboardEntry>();
      FirebaseDatabase.DefaultInstance
      .GetReference("ranking").OrderByChild("score")
      .GetValueAsync().ContinueWith(task => {
        if (task.IsFaulted) {
          // Handle the error...
        }
        else if (task.IsCompleted) {
          DataSnapshot snapshot = task.Result;
          if (snapshot != null && snapshot.ChildrenCount > 0) {
            Debug.Log("RANKING2 = " + snapshot.ChildrenCount);
            foreach (var childSnapshot in snapshot.Children) {
              if (childSnapshot.Child("score") == null || childSnapshot.Child("score").Value == null) {
                Debug.LogError("Bad data in sample.");
                break;
                } else {
                  /*rc.setScore(childSnapshot.Child("email").Value.ToString(),
                  "score",
                  (int) childSnapshot.Child("score").Value);
                */  
                Debug.Log("Leaders entry : " +
                  childSnapshot.Child("email").Value.ToString() + " - " +
                  childSnapshot.Child("score").Value.ToString());
                LeaderboardEntry newScore = new LeaderboardEntry(childSnapshot.Child("email").Value.ToString(),
                Convert.ToInt32(childSnapshot.Child("score").Value));
                response.Add(newScore);
              }
            }
          }
        }
      });
    return response;
  }

  //Listener, now i'm not using this, but if i need it's done!
  static void HandleValueChanged(object sender, ValueChangedEventArgs args) {
     Debug.Log("RANKING " + args.Snapshot.ChildrenCount);
      if (args.DatabaseError != null) {
        Debug.LogError(args.DatabaseError.Message);
        return;
      }
      // Do something with the data in args.Snapshot
      if (args.Snapshot != null && args.Snapshot.ChildrenCount > 0) {
         Debug.Log("RANKING2");
        foreach (var childSnapshot in args.Snapshot.Children) {
           if (childSnapshot.Child("score") == null || childSnapshot.Child("score").Value == null) {
            Debug.LogError("Bad data in sample.");
            break;
          } else {
            Debug.Log("Leaders entry : " +
              childSnapshot.Child("email").Value.ToString() + " - " +
              childSnapshot.Child("score").Value.ToString());
          }
        }
      }
  }

  

}
