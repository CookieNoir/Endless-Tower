using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {
    public List<GameObject> FloorSection1;
    public List<GameObject> FloorSection2;
    public List<GameObject> FloorSection3;

    public GameObject[] tower;
    private Game game;
    private int floorNumber;

	void Start () {
        game = GameObject.FindWithTag("Player").GetComponent<Game>();
        FloorSection1 = new List<GameObject>();
        FloorSection2 = new List<GameObject>();
        FloorSection3 = new List<GameObject>();
        tower = new GameObject[9];
        floorNumber = (int)(gameObject.transform.position.y / Game.FLOOR_SIZE + 1);
        gameObject.name = "Floor " + floorNumber;
	}

    void Update () {
        if (FloorSection1.Count > 0 && game.GetEnemySection() < (floorNumber - 1) * 3) game.SetEnemySection((floorNumber - 1) * 3);
        else if (FloorSection2.Count > 0 && game.GetEnemySection() < (floorNumber - 1) * 3 + 1) game.SetEnemySection((floorNumber - 1) * 3 + 1);
        else if (FloorSection3.Count > 0 && game.GetEnemySection() < (floorNumber - 1) * 3 + 2) game.SetEnemySection((floorNumber - 1) * 3 + 2);
        else if (FloorSection1.Count == 0 && game.GetEnemySection() == (floorNumber - 1) * 3) game.SetEnemySection((floorNumber - 1) * 3 - 1);
        else if (FloorSection2.Count == 0 && game.GetEnemySection() == (floorNumber - 1) * 3 + 1) game.SetEnemySection((floorNumber - 1) * 3);
        else if (FloorSection3.Count == 0 && game.GetEnemySection() == (floorNumber - 1) * 3 + 2) game.SetEnemySection((floorNumber - 1) * 3 + 1);
    }

    public int GetFloorNumber() {
        return floorNumber;
    }
    public void AddToList(int section,GameObject target)
    {
        if (section == 0) FloorSection1.Add(target);
        else if (section==1) FloorSection2.Add(target);
        else FloorSection3.Add(target);
    }
    public void RemoveFromList(int section,GameObject target) {
        if (section == 0) FloorSection1.Remove(target);
        else if (section == 1) FloorSection2.Remove(target);
        else FloorSection3.Remove(target);
    }
    public int SelectFarEnemy(int section,int priority) {
        if (section == 0) {
            if (FloorSection1.Count > 0)
            {
                int result = -1;
                float rot = -60;
                for (int i = 0; i < FloorSection1.Count; ++i)
                {
                    if (((priority&FloorSection1[i].GetComponent<Enemy>().type)>0)&&FloorSection1[i].GetComponent<Enemy>().GetRot() > rot)
                    {
                        result = i;
                        rot = FloorSection1[i].GetComponent<Enemy>().GetRot();
                    }
                }
                return result;
            }
            else return -1;
        }//section 1
        else if (section == 1)
        {
            if (FloorSection2.Count > 0)
            {
                int result = -1;
                float rot = -60;
                for (int i = 0; i < FloorSection2.Count; ++i)
                {
                    if (((priority & FloorSection2[i].GetComponent<Enemy>().type) > 0) && FloorSection2[i].GetComponent<Enemy>().GetRot() > rot)
                    {
                        result = i;
                        rot = FloorSection2[i].GetComponent<Enemy>().GetRot();
                    }
                }
                return result;
            }
            else return -1;
        }//section 2
        else {
            if (FloorSection3.Count > 0)
            {
                int result = -1;
                float rot = -60;
                for (int i = 0; i < FloorSection3.Count; ++i)
                {
                    if (((priority & FloorSection3[i].GetComponent<Enemy>().type) > 0) && FloorSection3[i].GetComponent<Enemy>().GetRot() > rot)
                    {
                        result = i;
                        rot = FloorSection3[i].GetComponent<Enemy>().GetRot();
                    }
                }
                return result;
            }
            else return -1;
        }//section 3
    }

}
