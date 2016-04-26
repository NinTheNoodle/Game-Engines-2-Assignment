using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour {
    float turnSpeed = 0;

    void Update () {
        float mouseX = Input.mousePosition.x / Screen.width - 0.5f;
        float mouseY = Input.mousePosition.y / Screen.height - 0.5f;

        Fly fly = gameObject.GetComponent<Fly>();
        BehaviourController controller = gameObject.GetComponent<BehaviourController>();

        Vector3 vel = transform.forward;
        vel.y = 0;

        if (Input.GetKey(KeyCode.D))
            vel += transform.right;

        if (Input.GetKey(KeyCode.A))
            vel += -transform.right;

        if (transform.forward.y < 0 && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
            vel += Vector3.up;

        if (Input.GetKey(KeyCode.S) || (transform.forward.y < 0 && !Input.GetKey(KeyCode.W)))
            vel += Vector3.up;

        if (Input.GetKey(KeyCode.W))
            vel += Vector3.down;

        fly.targetVelocity = vel;
    }
}
