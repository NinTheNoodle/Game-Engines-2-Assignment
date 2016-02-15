using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

    public GameObject player;
    public float distance = 40;
    public float angle = 35;
    public float rotationReactivity = 2;
    public float movementReactivity = 1;
    public float verticalReactivity = 1;
    public Vector3 offset = new Vector3(0, 5, 0);

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 position = offset + player.transform.position;

        Quaternion rotation = Quaternion.LookRotation(position - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationReactivity);
        float dist = Vector3.Distance(transform.position, position);
        transform.position += transform.forward * ((dist - this.distance) * Time.deltaTime * movementReactivity);
        transform.position += transform.up * (Mathf.DeltaAngle(transform.eulerAngles.x, angle) * Time.deltaTime * verticalReactivity);
	}
}
