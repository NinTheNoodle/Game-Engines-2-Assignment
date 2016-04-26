using UnityEngine;
using System.Collections;

public class BulletDecay : MonoBehaviour {
    public float life;
	
	void Update () {
        life -= Time.deltaTime;
        if (life <= 0)
            Destroy(gameObject);
	}
}
