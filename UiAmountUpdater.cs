using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiAmountUpdater : MonoBehaviour {
    private Game game;
    public int id;
    private Text text;
	void Start () {
        game = GameObject.FindWithTag("Player").GetComponent<Game>();
        text = gameObject.GetComponent<Text>();
	}
	
	void Update () {
        text.text = game.resources[id].ToString();
	}
}
