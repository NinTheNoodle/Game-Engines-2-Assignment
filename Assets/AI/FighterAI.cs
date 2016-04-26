using UnityEngine;
using System.Collections;

public class FighterAI : MonoBehaviour {

    public GameObject target;

    BehaviourController controller;
    bool fleeing = false;
    
    void Start () {
        controller = gameObject.GetComponent<BehaviourController>();
        controller.target = target;
    }

    void Update() {
        Vector3 aimOffset = controller.target.transform.position - transform.position;
        gameObject.GetComponent<Fire>().aimOffset = transform.InverseTransformDirection(aimOffset);

        if (fleeing)
        {
            if (Random.Range(0f, 1f) > 0.5f)
                fleeing = false;
        }
        else {
            if (Random.Range(0f, 1f) > 0.9f)
                fleeing = true;
        }

        if (fleeing)
            controller.setBehaviour<Flee>();
        else
            controller.setBehaviour<Flyby>();
    }
}
