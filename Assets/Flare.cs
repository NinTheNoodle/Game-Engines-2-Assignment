using UnityEngine;
using System.Collections;

public class Flare : MonoBehaviour {
    public Vector3 velocity = Vector3.zero;
    public float timer = 5;
    bool dead = false;

    void Start ()
    {
        timer = Random.Range(timer / 2, timer);
    }
	
	void Update () {
        transform.rotation = Quaternion.identity;
        timer -= Time.deltaTime;
        if (timer <= 0)
            Destroy(gameObject);

        if (!dead)
        {
            velocity += Vector3.down * 9.81f * Time.deltaTime;
            if (Physics.Raycast(transform.position, velocity, velocity.magnitude, ~LayerMask.NameToLayer("Terrain")))
            {
                RaycastHit hit;
                Physics.Raycast(transform.position, Vector3.down, out hit, float.PositiveInfinity, ~LayerMask.NameToLayer("Terrain"));

                transform.position += Vector3.down * hit.distance + Vector3.up;

                velocity = Vector3.zero;
                dead = true;
            }
            transform.position += velocity;
        }
        else
        {
            if (!Physics.Raycast(transform.position, Vector3.down, 5, ~LayerMask.NameToLayer("Terrain")))
            {
                dead = false;
            }
        }
        
    }
}
