using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable] public struct fiend {
    public GameObject enemy;
    public int value;
    public int maxSpawnAmount;
    public int spawnCooldown;
    public int waveForNext;
}

public class Spawner : MonoBehaviour {
    public const int START_POINTS = 6;
    public List<fiend> fiends;

    private Text waveText;
    private int wave = 0;
    private int spawnPoints;
    private int prevPoints;
    private int enemyAmount;

    private int enemyIndex;
    private int curIndex;
    private int maxIndex;
    private int queue;

    private float timer = 15;
    private bool waveCompleted = true;

    private void Start () {
        waveText = GameObject.FindWithTag("Wave").GetComponent<Text>();
        waveText.color = new Color(1, 1, 1, 0);
        spawnPoints = START_POINTS;
        prevPoints = spawnPoints;
        enemyAmount = 0;

        enemyIndex = 0;
        maxIndex = 0;
        curIndex = maxIndex;
    }

    void Update() {
        if (waveCompleted) {
            if (timer > 0) { timer -= Time.deltaTime;
                waveText.text = Mathf.Ceil(timer).ToString();
                if (waveText.color.a < 1) waveText.color= waveText.color + new Color(0, 0, 0, 0.002f);
            }
            else
            {
                wave++;
                spawnPoints = prevPoints + (wave + 1) * 2 + (wave + 1) / 5 * 2 + wave / 5 * 2;
                prevPoints = spawnPoints;
                if (wave == fiends[maxIndex].waveForNext) maxIndex++;
                curIndex = maxIndex;
                waveText.text = "Wave " + wave;
                waveCompleted = false;
            }
        }
        else
        {
            if (spawnPoints > 0)
            {
                if (waveText.color.a > 0) waveText.color = waveText.color - new Color(0,0,0,0.01f);
                if (timer <= 0)
                {
                    if (queue > 0)
                    {
                        Instantiate(fiends[enemyIndex].enemy, transform.position, transform.rotation);
                        spawnPoints -= fiends[enemyIndex].value;
                        timer = fiends[enemyIndex].spawnCooldown;
                        enemyAmount++;
                        queue--;
                    }
                    else {
                        if (spawnPoints < fiends[curIndex].value) curIndex--;
                        else {
                            enemyIndex = Random.Range(0, curIndex + 1);
                            queue = Random.Range(1, spawnPoints / fiends[enemyIndex].value + 1);
                            if (queue > fiends[enemyIndex].maxSpawnAmount) queue = fiends[enemyIndex].maxSpawnAmount;
                        }
                    }
                }
                else timer -= Time.deltaTime;
            }
            else if (enemyAmount<=0)
            {
                waveCompleted = true;
                enemyAmount = 0;
                timer = 15;
            }
        }
    }
    public void decreaseEnemyAmount() {
        enemyAmount--;
    }
}
