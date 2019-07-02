using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildMenuBehaviour : MonoBehaviour {
    public GameObject buildTowerButton;
    public GameObject buildStructureButton;
    public GameObject shop;
    public GameObject towerPanel;
    public ScrollRect shopContent;
    public GameObject shopTower;
    public GameObject shopStructure;

    private bool shopOpened = true;
    private bool shopTowerSelected = true;
    private bool changed = true;

	void Update () {
        if (changed) {
            if (shopOpened) {
                towerPanel.SetActive(false);
                shop.SetActive(true);
                if (shopTowerSelected) {
                    shopStructure.SetActive(false);
                    shopTower.SetActive(true);
                    shopContent.content = shopTower.GetComponent<RectTransform>();
                }
                else {
                    shopTower.SetActive(false);
                    shopStructure.SetActive(true);
                    shopContent.content = shopStructure.GetComponent<RectTransform>();
                }
            }
            else {
                shop.SetActive(false);
                towerPanel.SetActive(true);
            }
            changed = false;
        }
	}
    public void BuildTowerButton() {
        if (shopOpened)
        {
            if (!shopTowerSelected) shopTowerSelected = true;
            else shopOpened = false;
        }
        else {
            shopOpened = true;
            shopTowerSelected = true;
        }
        changed = true;

    }
    public void BuildStructureButton() {
        if (shopOpened)
        {
            if (shopTowerSelected) shopTowerSelected = false;
            else shopOpened = false;
        }
        else
        {
            shopOpened = true;
            shopTowerSelected = false;
        }
        changed = true;
    }
}
