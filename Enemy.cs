using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private Game game;

    private float rot = -360;
    public float speed = 1; //amount of floors per minute
    public int health = 5;
    public int score = 1;
    public int gold = 1;
    public int type = 1;
    public GameObject TargetObject;
    private int floorSection;//for towers
    private Floor floorClass;
    private int sector; //for moving
    private bool reachedMax = false;

    void Start () {
        game = GameObject.FindWithTag("Player").GetComponent<Game>();
        floorSection = -3;
        floorClass = null;
    }
	
	void Update () {
        rot += Time.deltaTime*6*speed;
        gameObject.transform.rotation = Quaternion.Euler(0, -rot, 0);
        sector = Mathf.RoundToInt(Mathf.Floor((rot + 30f) / 60f));
        if (sector % 2 == 1) gameObject.transform.position = new Vector3(0f, (sector / 2) * Game.FLOOR_SIZE / 3f + ((rot + 30) - 60 * sector) * Game.FLOOR_SIZE / 180f, 0f);
        else if (sector%2==-1) gameObject.transform.position = new Vector3(0f, (sector / 2 - 1) * Game.FLOOR_SIZE / 3f + ((rot + 30) - 60 * sector) * Game.FLOOR_SIZE / 180f, 0f);
        else gameObject.transform.position = new Vector3(0f, (sector / 2) * Game.FLOOR_SIZE / 3f, 0f);


        if (rot >= game.GetMaxFloor() * 360 + 24) //lose condition
        {
            game.Lost();
            Destroy(this);
        }
        else if (game.GetLost()) Destroy(this);
        else if (health <= 0)
        {
            game.ChangeGold(gold);
            game.ChangeScore(score);
            game.decreaseEnemyAmount();
            if (floorClass) floorClass.RemoveFromList(floorSection % 3, gameObject);
            Destroy(gameObject);
        }
        else
        {
            if (floorSection >= 0 && !floorClass && !reachedMax)
            {
                floorClass = GameObject.Find("Floor " + (floorSection / 3 + 1)).GetComponent<Floor>();
                floorClass.AddToList(floorSection % 3, gameObject);
            }
            if (rot >= -60 && floorSection != (int)Mathf.Floor((rot + 60) / 120))
            {
                if (floorClass) floorClass.RemoveFromList(floorSection % 3, gameObject);
                if (floorSection / 3 != ((int)Mathf.Floor((rot + 60) / 120)) / 3)
                {
                    floorClass = null;
                    if (floorSection == -3) floorSection = -1;//start condition
                    floorSection++;
                    if (floorSection / 3 != game.GetMaxFloor())
                    {
                        floorClass = GameObject.Find("Floor " + (floorSection / 3 + 1)).GetComponent<Floor>();
                        floorClass.AddToList(floorSection % 3, gameObject);
                    }
                    else
                    {
                        reachedMax = true;
                        game.TopList.Add(gameObject);
                    }
                }
                else
                {
                    floorSection++;
                    floorClass.AddToList(floorSection % 3, gameObject);
                }
            }
            if (floorSection >= 0 && !floorClass && floorSection / 3 != game.GetMaxFloor()) reachedMax = false;
        }//end else

    }

    public float GetRot() {
        return rot;
    }
}