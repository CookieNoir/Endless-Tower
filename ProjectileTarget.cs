using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTarget : MonoBehaviour {
    public Enemy enemy;
    public void HitEnemy(int damage) {
        enemy.health -= damage;
    }
}
