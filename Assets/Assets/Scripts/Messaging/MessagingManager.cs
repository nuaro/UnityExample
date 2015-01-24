using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MessagingManager : MonoBehaviour {

	//static singleton property
	public static MessagingManager Instance {
		get;
		private set;
	}
	
	// public property for manager
	private List<Action> subscribers = new List<Action>();

	void Awake(){
		Debug.Log("Mesagin manager started");
		// first we check if there are any other intances conflicting
		if(Instance != null && Instance != this){
			//destroy other instances if it's not the same
			Destroy(gameObject);
		}

		//Save our current singleton instance
		Instance = this;

		//Make sure the instance is not destroyed betwen scenes
		DontDestroyOnLoad(gameObject);
	}

	// the subcribe method for manager
	public void Subscribe(Action subscriber){
		Debug.Log("Subscriber register");
		subscribers.Add(subscriber);
	}

	// the unsubcribe method for manager
	public void UnSubscribe(Action subscriber){
		Debug.Log("unSubscriber register");
		subscribers.Remove(subscriber);
	}

	public void ClearAllSubscribers(){

		subscribers.Clear();
	}

	public void Broadcast(){
		Debug.Log("Bradcaste requested, no of subscribers = " + subscribers.Count);
		foreach ( var subscriber in subscribers){
			subscriber();
		}
	}
}
