using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable] public struct Resources {
    public int amount;
    public Text resourceTextField;
    public Resources(int value, Text field) {
        amount = value;
        resourceTextField = field;
    }
}

public class Game : MonoBehaviour {
    public const float FLOOR_SIZE=4.5f;

    private int maxFloor = 1;
    private int score = 0;
    private int enemySection = -1;

    private bool lost = false;

    public int startGold = 200;
    private int gold;
    public List<Resources> resources;

    public List<GameObject> TopList;

    private CamMoving cam;
    private Text maxFloorText;
    private GameObject scroller;
    private RectTransform enemyHeight;
    private Text scoreText;
    private GameObject goldField;
    private GameObject goldText;
    public GameObject tower;
    private Spawner spawner;
    private bool enemySectionChanged = false;
    private float enemySectionTarget;

    void Start () {
        gameObject.transform.position = new Vector3(0, FLOOR_SIZE, 0);
        cam = GameObject.FindWithTag("MainCamera").GetComponent<CamMoving>();
        maxFloorText = GameObject.FindWithTag("MaxFloor").GetComponent<Text>();
        scroller = GameObject.FindWithTag("Scroller");
        enemyHeight = GameObject.FindWithTag("EnemyHeight").GetComponent<RectTransform>();
        SetEnemySection(enemySection);
        scoreText = GameObject.FindWithTag("Score").GetComponent<Text>();
        goldField = GameObject.FindWithTag("GoldField");
        goldText = GameObject.FindWithTag("Gold");
        spawner = GameObject.FindWithTag("Spawner").GetComponent<Spawner>();
        TopList = new List<GameObject>();
        gold = startGold;
        ChangeGold(0);
        ChangeScore(0);
    }

    void Update() {
        if (TopList.Count > 0 && enemySection != maxFloor * 3) SetEnemySection(maxFloor * 3);
        if (enemySectionChanged) {
            if (enemyHeight.sizeDelta.y != enemySectionTarget) {
                if (Mathf.Abs(enemyHeight.sizeDelta.y - enemySectionTarget) < 1f) {
                    enemyHeight.sizeDelta = new Vector2(enemyHeight.sizeDelta.x, enemySectionTarget);
                    enemySectionChanged = false;
                }
                else enemyHeight.sizeDelta = new Vector2(enemyHeight.sizeDelta.x, Mathf.Lerp(enemyHeight.sizeDelta.y, enemySectionTarget, 0.1f));
            }
        }
    }

    public void UpMaxFloor() {
        Instantiate(tower, new Vector3(0, maxFloor * FLOOR_SIZE, 0), tower.transform.rotation);
        TopList.Clear();
        maxFloor++;
        gameObject.transform.position = new Vector3(0, maxFloor * FLOOR_SIZE, 0);
        SetMaxFloorText(maxFloor);
        SetEnemySection(enemySection);
    }

    public int GetMaxFloor() {
        return maxFloor;
    }

    public void SetMaxFloorText(int val) {
        maxFloorText.text = val.ToString();
    }

    public void ChangeGold(int amount) {
        gold += amount;
        if (gold < 0) gold = 0;
        int num = numbers(gold);
        goldText.GetComponent<RectTransform>().sizeDelta = new Vector2((goldText.GetComponent<Text>().fontSize/2+1)*num, goldText.GetComponent<RectTransform>().sizeDelta.y);
        goldText.GetComponent<Text>().text = gold.ToString();
        goldField.GetComponent<RectTransform>().sizeDelta= new Vector2((int)goldField.GetComponent<RectTransform>().sizeDelta.y + 6 + (goldText.GetComponent<Text>().fontSize/2+1)*num, goldField.GetComponent<RectTransform>().sizeDelta.y);
    }
    public int GetGold() {
        return gold;
    }
    private int numbers(int number) {
        if (number == 0) return 1;
        int n = number;
        int result = 0;
        while (n != 0) { n /= 10; result++; }
        return result;
    }
    public void ChangeScore(int amount) {
        score += amount;
        int num = 7 - numbers(score);
        if (num > 0) { 
            string nul = "";
            for (int i = 0; i < num; ++i) nul = nul + "0";
            scoreText.text = nul+score.ToString();
        }
        else scoreText.text = score.ToString();
    }
    public void decreaseEnemyAmount() {
        spawner.decreaseEnemyAmount();
    }
    public void SetEnemySection(int section) {
        enemySection = section;
        if (enemySection == -1) enemySectionTarget = 0;
        else enemySectionTarget = ((float)enemySection / (maxFloor * 3)) * scroller.GetComponent<RectTransform>().sizeDelta.y;
        enemySectionChanged = true;
    }
    public int GetEnemySection() {
        return enemySection;
    }
    public void Lost() {
        lost = true;
        Debug.Log("You lost");
        Time.timeScale = 0;
        cam.Lost();
    }
    public bool GetLost() {
        return lost;
    }
    public void UpdateResourceList () {
        for (int i = 0; i < resources.Count; ++i) resources[i].resourceTextField.text = resources[i].amount.ToString();
    }
}