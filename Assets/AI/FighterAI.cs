using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FighterAI : MonoBehaviour {
    public GameObject target = null;
    public string targetTag = "";
    private int devotion = 10;

    BehaviourController controller;
    bool fleeing = false;
    
    void Start () {
        controller = gameObject.GetComponent<BehaviourController>();
        if (target != null)
            controller.target = target;
    }

    void Update() {
        if (devotion > 100)
            devotion -= 1;
        if (!GetComponent<Fly>().operable)
            controller.target = null;
        else
        if (Random.Range(0, devotion) == 0 || controller.target == null || !controller.target.GetComponent<Fly>().operable)
        {
            devotion = 700;
            List<GameObject> alive = new List<GameObject>();
            if (targetTag != "")
            {
                foreach (GameObject obj in GameObject.FindGameObjectsWithTag(targetTag))
                {
                    if (obj.GetComponent<Fly>().operable)
                    {
                        alive.Add(obj);
                    }
                }
                if (alive.Count > 0)
                {
                    controller.target = alive[Random.Range(0, alive.Count)];
                }
                else
                    controller.target = null;
            }
        }

        if (controller.target != null)
        {
            Vector3 aimOffset = controller.target.transform.position - transform.position;
            gameObject.GetComponent<Fire>().aimOffset = transform.InverseTransformDirection(aimOffset);

            if (fleeing)
            {
                if (Random.Range(0f, 1f) > 0.5f)
                    fleeing = false;
            }
            else
            {
                if (Random.Range(0f, 1f) > 0.9f)
                    fleeing = true;
            }

            if (fleeing)
                controller.setBehaviour<Flee>();
            else
                controller.setBehaviour<Flyby>();
        }
    }
}
