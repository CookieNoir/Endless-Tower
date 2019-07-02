using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiShowHide : MonoBehaviour {
    private Game game;
    public GameObject target;

    void Start() {
        game = GameObject.FindWithTag("Player").GetComponent<Game>();
    }

    public void ShowHide() {
        target.SetActive(true);
        gameObject.SetActive(false);
    }

    public void TreasureShowHide()
    {
        target.SetActive(true);
        gameObject.SetActive(false);
        game.UpdateResourceList();
    }
}
