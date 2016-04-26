using UnityEngine;
using System.Collections;

public class BomberAI : MonoBehaviour {
    public GameObject target;

    BehaviourController controller;

    void Start()
    {
        controller = gameObject.GetComponent<BehaviourController>();
        controller.target = target;
    }

    void Update()
    {
        controller.setBehaviour<Path>();
    }
}
