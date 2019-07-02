using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    private Game game;
    public Floor floor;
    public GameObject projectile;
    public GameObject pointFrom;
    public Enemy enemy;
    public GameObject pointTo;

    public int damage = 1;
    public float attackSpeed = 1;
    public int priority = 15;

    public float upgradeDamageModifier = 2f;
    public float upgradeAttackSpeedModifier = 1.1f; // attackSpeed / upgradeAttackSpeedModifier (>1 makes it faster)
    public float upgradeCostModifier = 2f;

    public Sprite icon;
    private int goldForUpgrade = 0;
    private int goldAfterSale = 0;

    private float timer;
    private int section=-1;

	void Start () {
        game = GameObject.FindWithTag("Player").GetComponent<Game>();
        timer = attackSpeed;
	}

	void Update () {
        if (!floor) floor = GameObject.Find("Floor " + ((int)(transform.position.y / Game.FLOOR_SIZE) + 1)).GetComponent<Floor>();
        if (enemy) if (enemy.GetRot() > section * 120 + 60) { enemy = null; pointTo = null; }
        if (!pointTo) {
            if (timer <= 0) FindTarget();
            else timer -= Time.deltaTime;
        }
        else
        {
            if (timer <= 0)
            {
                GameObject proj = Instantiate(projectile, pointFrom.transform.position, projectile.transform.rotation);
                proj.GetComponent<Projectile>().SetTarget(pointTo, damage);
                timer = attackSpeed;
            }
            else timer -= Time.deltaTime;
        }
    }

    private void FindTarget() {
        if (section == -1) section = (int)(transform.position.y / (Game.FLOOR_SIZE / 3));
        int en = floor.SelectFarEnemy(section % 3, priority);
        if (en > -1)
        {
            if (section%3 == 0) enemy = floor.FloorSection1[en].GetComponent<Enemy>();
            else if (section%3 == 1) enemy = floor.FloorSection2[en].GetComponent<Enemy>();
            else if (section%3 == 2) enemy = floor.FloorSection3[en].GetComponent<Enemy>();
            pointTo = enemy.TargetObject;
        }
    }

    public GameObject GetTarget() {
        return pointTo;
    }
    public void Upgrade() {
        if (game.GetGold() >= goldForUpgrade)
        {
            damage = (int)(damage * upgradeDamageModifier);
            attackSpeed /= upgradeAttackSpeedModifier;
            AddGoldAfterSale(goldForUpgrade / 2);
            game.ChangeGold(-goldForUpgrade);
            goldForUpgrade = (int)(goldForUpgrade * upgradeCostModifier);
        }
    }
    public int GetUpgradeCost() {
        return goldForUpgrade;
    }
    public void AddGoldForUpgrade(int amount) {
        goldForUpgrade += amount;
    }
    public void AddGoldAfterSale(int amount) {
        goldAfterSale += amount;
    }
    public void SellTower() {
        game.ChangeGold(goldAfterSale);
        Destroy(gameObject);
    }
}