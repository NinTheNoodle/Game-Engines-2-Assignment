using UnityEngine;
using System.Collections;

public class FlareBurst : MonoBehaviour {
    public GameObject flare;
    public float force;
    public int count;
    void Start() {
        for (int i = 0; i < count; i++)
        { 
            GameObject newFlare = (GameObject)Instantiate(flare, transform.position + Vector3.up + Random.insideUnitSphere, transform.rotation);
            newFlare.GetComponent<Flare>().velocity = Random.insideUnitSphere * force * Random.Range(0.1f, 1f) + Vector3.up * force / 2;
        }
    }
}
