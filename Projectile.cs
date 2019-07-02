using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject target;
    public float projectileSpeed = 10f;
    private int damage;
    void Start()
    {
        transform.LookAt(target.transform);
    }

    void Update()
    {
        if (target)
        {
            if ((transform.position - target.transform.position).sqrMagnitude > 0.25) transform.position = Vector3.MoveTowards(transform.position, target.transform.position, projectileSpeed*Time.deltaTime);
            else
            {
                target.GetComponent<ProjectileTarget>().HitEnemy(damage);
                Destroy(gameObject);
            }
        }
        else Destroy(gameObject);
    }

    public void SetTarget(GameObject t,int d) {
        target = t;
        damage = d;
    }
}