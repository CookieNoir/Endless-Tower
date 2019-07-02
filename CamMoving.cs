using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamMoving : MonoBehaviour {
    private Game game;
    private float rot = 0;
    public int targetSection = 0;
    public int currentSection = 0;
    private Text currentFloorText;
    private GameObject scroller;
    private RectTransform height;
    private GameObject upFloorButton;
    private GameObject buildMenu;
  
    private bool changed = false;
    private bool reached = true;

    void Start() {
        game = GameObject.FindWithTag("Player").GetComponent<Game>();
        currentFloorText = GameObject.FindWithTag("CurrentFloor").GetComponent<Text>();
        scroller = GameObject.FindWithTag("Scroller");
        height = GameObject.FindWithTag("ScrollerHeight").GetComponent<RectTransform>();

        upFloorButton = GameObject.FindWithTag("UpFloor");
        buildMenu = GameObject.FindWithTag("BuildMenu");
        upFloorButton.SetActive(false);

        scroller.GetComponent<Scrollbar>().value = 0;
    }

    void Update() {
        if (!reached) {
            if (Mathf.Abs(targetSection * 120 - rot) < 6)
            {
                scroller.GetComponent<Scrollbar>().value = (targetSection * 120f) / (game.GetMaxFloor() * 360);
                reached = true;
            }
            else scroller.GetComponent<Scrollbar>().value += Mathf.Sign(targetSection * 120 - rot) * 6f / (game.GetMaxFloor() * 360);
            changed = true;
        }

        if (changed)
        {
            currentSection = (int)Mathf.Floor((rot + 60) / 120);
            if (currentSection == game.GetMaxFloor() * 3) currentFloorText.text = "Top";
            else currentFloorText.text = (currentSection/3+1)+" | "+(currentSection%3+1);

            gameObject.transform.rotation = Quaternion.Euler(0, -rot, 0);
            int sector = (int)Mathf.Floor((rot + 30f) / 60f);
            if (sector % 2 == 1) gameObject.transform.position = new Vector3(0f, (sector / 2) * Game.FLOOR_SIZE / 3f + ((rot + 30) - 60 * sector) * Game.FLOOR_SIZE / 180f, 0f);
            else gameObject.transform.position = new Vector3(0f, (sector / 2) * Game.FLOOR_SIZE / 3f, 0f);
            height.sizeDelta = new Vector2(height.sizeDelta.x, rot / (game.GetMaxFloor() * 360) * scroller.GetComponent<RectTransform>().sizeDelta.y);
            if (!game.GetLost())
            {
                if (rot >= game.GetMaxFloor() * 360 - 30) { upFloorButton.SetActive(true); buildMenu.SetActive(false); }
                else { upFloorButton.SetActive(false); buildMenu.SetActive(true); }
            }
            changed = false;
        }
    }

    public void IncreaseMaxFloor()
    {
        if (game.GetGold() >= 500)
        {
            game.ChangeGold(-500);
            game.UpMaxFloor();
            scroller.GetComponent<Scrollbar>().value = rot / (game.GetMaxFloor() * 360);
            height.sizeDelta = new Vector2(height.sizeDelta.x, rot / (game.GetMaxFloor() * 360) * scroller.GetComponent<RectTransform>().sizeDelta.y);
        }
    }
    public void CamScroll(float value) {
        rot = value * game.GetMaxFloor() * 360f;
        changed = true;
    }

    public void MoveLeft() {
        targetSection--;
        if (targetSection < 0) targetSection = 0;
        reached = false;
    }
    public void MoveRight() {
        targetSection++;
        if (targetSection > game.GetMaxFloor() * 3) targetSection = game.GetMaxFloor() * 3;
        reached = false;
    }
    public void OnScrollerUp() {
        targetSection = (int)Mathf.Floor((rot + 60) / 120);
        reached = false;
    }
    public void ReachTop() {
        targetSection = game.GetMaxFloor() * 3;
        reached = false;
    }
    public int GetCurrentSection() {
        return currentSection;
    }
    public void Lost() {
        upFloorButton.SetActive(false);
        buildMenu.SetActive(false);
    }
}