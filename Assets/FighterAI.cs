using UnityEngine;
using System.Collections;

public class FighterAI : MonoBehaviour {

    public GameObject target;

    BehaviourController controller;

    // Use this for initialization
    void Start () {
        controller = gameObject.GetComponent<BehaviourController>();
        controller.target = target;
    }
	
	// Update is called once per frame
	void Update () {
        controller.behaviour = gameObject.GetComponent<Path>();
    }
}
