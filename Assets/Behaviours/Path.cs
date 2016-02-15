using UnityEngine;
using System.Collections;

public class Path : MonoBehaviour {

    private float goodEnoughTimer = 0;

	// Use this for initialization
	void Start () {
        enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        Fly fly = gameObject.GetComponent<Fly>();
        BehaviourController controller = gameObject.GetComponent<BehaviourController>();
        Vector3 offset = controller.target.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, offset);

        PathNode targetComponent = controller.target.GetComponent<PathNode>();
        
        fly.targetVelocity = offset;
        

        if (offset.magnitude <= targetComponent.radius)
        {
            controller.target = targetComponent.nextTarget;
            goodEnoughTimer = 0;
        }

        if (offset.magnitude <= targetComponent.goodEnoughRadius)
        {
            goodEnoughTimer += Time.deltaTime;
            if (goodEnoughTimer > targetComponent.goodEnoughTime)
            {
                controller.target = targetComponent.nextTarget;
                goodEnoughTimer = 0;
            }
        }
        else
            goodEnoughTimer = 0;
    }
}
