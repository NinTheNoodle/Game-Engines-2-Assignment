﻿using UnityEngine;
using System.Collections;

public class Seek : MonoBehaviour
{
	void Start () {
        enabled = false;
	}
	
	void Update () {
        Fly fly = gameObject.GetComponent<Fly>();
        BehaviourController controller = gameObject.GetComponent<BehaviourController>();

        fly.targetVelocity = controller.target.transform.position - transform.position;
    }
}
