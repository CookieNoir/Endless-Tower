using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUpdater : MonoBehaviour {
    private Game game;
    private CamMoving cam;

    public int number;
    public Image targetImage;
    public Sprite defaultIcon;
    public Text upgradeCost;

    private int section = -1;
    private Tower tower;

    void OnEnable() {
        if (!cam) {
            cam = GameObject.FindWithTag("MainCamera").GetComponent<CamMoving>();
            game = GameObject.FindWithTag("Player").GetComponent<Game>();
        }
        else
        {
            if (section != cam.currentSection&&cam.currentSection!=game.GetMaxFloor()*3) section = cam.currentSection;
            SetTower();
        }
    }

    void Update () {
        if (section != cam.currentSection && cam.currentSection != game.GetMaxFloor() * 3) {
            section = cam.currentSection;
            SetTower();
        }
	}

    private void SetTower() {
        GameObject floorObj = GameObject.Find("Floor " + (section / 3 + 1));
        if (floorObj)
        {
            Floor floor = floorObj.GetComponent<Floor>();
            if (floor.tower[section % 3 * 3 + number])
            {
                tower = floor.tower[section % 3 * 3 + number].GetComponent<Tower>();
                targetImage.sprite = tower.icon;
                upgradeCost.text = tower.GetUpgradeCost().ToString();
            }
            else SetByDefault();
        }
        else SetByDefault();
    }
    public void Upgrade() {
        if (tower) {
            tower.Upgrade();
            upgradeCost.text = tower.GetUpgradeCost().ToString();
        }
    }
    public void SellTower() {
        if (tower) {
            tower.SellTower();
            SetByDefault();
        }
    }
    private void SetByDefault() {
        targetImage.sprite = defaultIcon;
        upgradeCost.text = "";
    }
}
