using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable] public struct resources {
    public int id;
    public int amount;
}

public class TowerBuy : MonoBehaviour {
    public int gold;
    public Text goldText;
    public resources[] res;
    public GameObject towerPrefab;
    private Game game;
    private CamMoving cam;
    private bool canBuild;

    void Start() {
        game = GameObject.FindWithTag("Player").GetComponent<Game>();
        cam = GameObject.FindWithTag("MainCamera").GetComponent<CamMoving>();
        goldText.text = gold.ToString();
    }

    void Update() {
        checkBalance();
        if (canBuild) gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
        else gameObject.GetComponent<Image>().color = new Color(0.5f, 0, 0, 0.5f);
    }

    void checkBalance() {
        bool enoughRes = true; 
        for (int i = 0; i < res.Length; ++i) {
            if (game.resources[res[i].id].amount - res[i].amount < 0) enoughRes = false ;
        }
        if (game.GetGold() - gold >= 0 && enoughRes) canBuild = true;
        else canBuild = false;
    }

    public void SetTower() {
        if (canBuild)
        {
            int section = cam.GetCurrentSection();
            if (section / 3 < game.GetMaxFloor()) {
                GameObject floor = GameObject.Find("Floor " + (section / 3 + 1));
                for (int i = section % 3 * 3; i < section % 3 * 3 + 3; ++i) {
                    if (!floor.GetComponent<Floor>().tower[i])
                    {
                        floor.GetComponent<Floor>().tower[i] = Instantiate(towerPrefab, new Vector3(0, section * Game.FLOOR_SIZE / 3, 0), Quaternion.Euler(new Vector3(0, -(120 * (section % 3) + 20 * (i % 3) - 20), 0)));
                        floor.GetComponent<Floor>().tower[i].GetComponent<Tower>().AddGoldForUpgrade((int)(gold * floor.GetComponent<Floor>().tower[i].GetComponent<Tower>().upgradeCostModifier));
                        floor.GetComponent<Floor>().tower[i].GetComponent<Tower>().AddGoldAfterSale(gold / 2);
                        game.ChangeGold(-gold);
                        for (int j = 0; j < res.Length; ++j)
                        {
                            game.resources[res[j].id] =new Resources(game.resources[res[j].id].amount - res[j].amount, game.resources[res[j].id].resourceTextField);
                            game.resources[res[j].id].resourceTextField.text = game.resources[res[j].id].amount.ToString();
                        }
                        break;
                    }
                }
            }
        }
    }
}
