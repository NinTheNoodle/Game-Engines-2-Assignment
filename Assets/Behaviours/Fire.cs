using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {
    public GameObject bullet;
    public Vector3 offset;
    public Vector3 force = Vector3.forward * 5000;
    public float inaccuracy = 0.1f;
    public float rpm = 120;
    public float aimFidelity = 0.5f;
    public float maxAngle = 30;

    public Vector3 aimOffset = Vector3.forward;

    float cooldown = 0;

    void Start()
    {
        cooldown = Random.Range(0, 60 / rpm);
    }

    void Update () {
        if (cooldown <= 0 && gameObject.GetComponent<Fly>().operable)
        {
            aimOffset.Normalize();

            if (Vector3.Angle(aimOffset, Vector3.forward) < maxAngle)
            {
                Vector3 aim = Vector3.Slerp(transform.TransformDirection(aimOffset), Random.insideUnitSphere.normalized, inaccuracy);
                GameObject bulletObj = Instantiate(bullet);
                bulletObj.transform.forward = Vector3.SlerpUnclamped(aim, transform.forward, aimFidelity);
                bulletObj.transform.position = transform.position + transform.TransformVector(offset);
                Rigidbody rigidBody = bulletObj.GetComponent<Rigidbody>();
                rigidBody.AddRelativeForce(force, ForceMode.VelocityChange);
                rigidBody.AddForce(gameObject.GetComponent<Rigidbody>().velocity, ForceMode.VelocityChange);
                cooldown = 60 / rpm;
            }
        }
        cooldown -= Time.deltaTime;
	}
}
