using UnityEngine;
using System.Collections;

public class Flee : MonoBehaviour {

    void Start()
    {
        enabled = false;
    }

    void Update()
    {
        Fly fly = gameObject.GetComponent<Fly>();
        BehaviourController controller = gameObject.GetComponent<BehaviourController>();

        fly.targetVelocity = transform.position - controller.target.transform.position;
    }
}
