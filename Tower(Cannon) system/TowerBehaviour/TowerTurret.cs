using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
    public Tower tower;
    private GameObject target;

    private Quaternion defaultEuler;
    private float freezer = 3f;
    private float range=0.0004f;
    private bool set = false;
    void Update () {
        if (!set) {
            defaultEuler = Quaternion.Euler(new Vector3(Random.Range(-30f, 30f), Random.Range(90f, 270f), Random.Range(-30f, 30f)));
            set = true;
        }
        if (tower.GetTarget() != target) {
            target = tower.GetTarget();
            range = 0.0004f;
            freezer = 3f;
            set = false;

        }
        else if (target) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), 0.25f);
        else {
            if (freezer > 0)
            {
                freezer -= Time.deltaTime;
            }
            else
            {
                transform.localRotation = Quaternion.Slerp(transform.localRotation, defaultEuler, range);
                if (range < 1) range += 0.0001f;
            }
        }
    }
}
