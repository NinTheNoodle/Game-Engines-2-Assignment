using UnityEngine;
using System.Collections;

public class OffsetPersue : MonoBehaviour {

    public Vector3 offset = new Vector3(0, 0, -10);
	
	void Update () {
        Fly fly = gameObject.GetComponent<Fly>();
        BehaviourController controller = gameObject.GetComponent<BehaviourController>();
        Vector3 targetPos;

        targetPos = controller.target.transform.position;
        targetPos += controller.target.transform.rotation * offset;

        fly.targetVelocity = targetPos - transform.position;
    }
}
