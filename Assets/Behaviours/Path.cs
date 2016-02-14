using UnityEngine;
using System.Collections;

public class Path : MonoBehaviour {

	// Use this for initialization
	void Start () {
        enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        Fly fly = gameObject.GetComponent<Fly>();
        BehaviourController controller = gameObject.GetComponent<BehaviourController>();
        fly.targetVelocity = controller.target.transform.position - transform.position;
        fly.targetVelocity.Normalize();

        PathNode targetComponent = controller.target.GetComponent<PathNode>();

        if (Vector3.Distance(transform.position, controller.target.transform.position) <= targetComponent.radius)
        {
            controller.target = targetComponent.nextTarget;
        }
    }
}
