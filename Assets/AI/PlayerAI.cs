using UnityEngine;
using System.Collections;

public class PlayerAI : MonoBehaviour {
    BehaviourController controller;
    Fire fire;

    void Start()
    {
        controller = gameObject.GetComponent<BehaviourController>();
        controller.setBehaviour<Control>();
        controller.target = null;
        fire = gameObject.GetComponent<Fire>();
        fire.aimOffset = Vector3.forward;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            Instantiate(gameObject.GetComponent<Fly>().explosion, transform.position + transform.forward + Vector3.up, transform.rotation);

        if (Input.GetKey(KeyCode.Space))
            fire.aimOffset = Vector3.forward;
        else
            fire.aimOffset = Vector3.back;
    }
}
