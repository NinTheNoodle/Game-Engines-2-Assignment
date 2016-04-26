using UnityEngine;
using System.Collections;

public class Fly : MonoBehaviour {
    public float yCorrectionSpeed = 100;
    public float rotationSpeed = 1;
    public float bankFactor = 1;
    public float maxBank = 25;
    public float thrust = 100;
    public float maxLift = 40;
    public float maxLiftSpeed = 20;
    public float stallingFactor = 2.2f;

    [Range(0, 1)]
    public float stallPoint = 0.1f;

    [Range(0, 1)]
    public float unstallPoint = 0.85f;

    [Range(0, 1)]
    public float stallAversion = 0.1f;

    [Range(0, 1)]
    public float minumumTurnSpeed = 0.5f;

    [Range(0, 1)]
    public float fullTurnSpeed = 0.7f;

    public float drag = 0.5f;
    public float angularDrag = 0.5f;
    public bool safety = true;

    public Vector3 startForce = Vector3.zero;
    public GameObject explosion = null;
    public float explosionSpeed = 10;

    [HideInInspector]
    public Vector3 targetVelocity;

    [HideInInspector]
    public float paralizedTimer = 0;

    [HideInInspector]
    public bool dead = false;

    [HideInInspector]
    public bool operable = true;

    [HideInInspector]
    public bool stalled = false;
    private float fire_timer = 0;
    private float fireStartTimer = 1;
    private float flySpeed = 0;
    private float deadTimer = 0;


    void Start()
    {
        Rigidbody body = gameObject.GetComponent<Rigidbody>();
        body.AddForce(startForce, ForceMode.VelocityChange);
    }


    void Update()
    {
        float dt = 30 * Time.deltaTime;
        Rigidbody body = gameObject.GetComponent<Rigidbody>();
        float currentSpeed = Vector3.Project(body.velocity, transform.forward).magnitude;

        if (currentSpeed > flySpeed)
        {
            flySpeed = currentSpeed;
        }
        else
        {
            flySpeed += (currentSpeed - flySpeed) * (dt / 4);
        }
        

        targetVelocity.Normalize();

        if (safety)
        {
            RaycastHit heightHit, forwardHit;

            Ray heightRay = new Ray(transform.position, Vector3.down);
            Ray forwardRay = new Ray(transform.position, transform.forward);
            bool heightColliding = Physics.Raycast(heightRay, out heightHit, 300, ~LayerMask.NameToLayer("Terrain"));
            bool forwardColliding = Physics.Raycast(forwardRay, out forwardHit, 500, ~LayerMask.NameToLayer("Terrain"));
            if (heightColliding)
                targetVelocity.y = Mathf.Max(0, targetVelocity.y);
            if (heightColliding && heightHit.distance < 200)
                targetVelocity.y = Mathf.Max(0.1f, targetVelocity.y);
            if (forwardColliding && forwardHit.distance < currentSpeed * 2)
                targetVelocity.y = Mathf.Max(0.5f, targetVelocity.y);
            targetVelocity.y = Mathf.Clamp(targetVelocity.y, -0.5f, 0.5f);
            targetVelocity.Normalize();
        }

        float liftSpeed = Mathf.Min(1, currentSpeed / maxLiftSpeed);
        float liftPitch = Mathf.Pow(Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.x), stallingFactor);

        if (!stalled && paralizedTimer <= 0 && !dead)
        {
            operable = true;
            Vector2 yawTransform = new Vector2(transform.forward.x, transform.forward.z);
            Vector2 yawTarget = new Vector2(targetVelocity.x, targetVelocity.z);

            float requiredYaw = Vector2.Angle(yawTransform, yawTarget) * Mathf.Sign(Vector3.Cross(yawTarget, yawTransform).z);
            float lift = liftPitch * liftSpeed * maxLift;
            float turnFactor = Mathf.Clamp((liftSpeed - minumumTurnSpeed) / (fullTurnSpeed - minumumTurnSpeed), 0, 1);

            if (targetVelocity.y < 0)
                lift *= Mathf.Pow(1 + targetVelocity.y, 3);

            fire_timer -= Time.deltaTime;

            requiredYaw *= turnFactor;

            body.velocity += transform.forward * (flySpeed - currentSpeed);
            body.AddForce((transform.forward * thrust * liftPitch + transform.up * lift) * dt);

            float y_offset = Mathf.Clamp(targetVelocity.y - transform.forward.y, stallAversion - stallPoint, stallPoint - stallAversion);
            
            Vector3 torque = new Vector3(
                -y_offset * yCorrectionSpeed * liftPitch * Mathf.Max(liftSpeed, 0.5f),
                -Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.z) * rotationSpeed * liftPitch,
                Mathf.DeltaAngle(transform.rotation.eulerAngles.z, Mathf.Clamp(-requiredYaw * liftPitch * bankFactor, -maxBank, maxBank))
                );

            torque *= dt;
            body.AddRelativeTorque(torque);

            body.angularDrag = angularDrag * liftPitch * liftSpeed;
            body.drag = drag * liftPitch;
            fireStartTimer = 1;

            if (liftPitch < stallPoint)
                stalled = true;
        }
        else
        {
            operable = false;
            body.angularDrag = 0.1f;
            body.drag = 0.05f;
            if (fireStartTimer <= 0)
                fire_timer = 2;
            else
                fireStartTimer -= Time.deltaTime;

            paralizedTimer -= Time.deltaTime;
            if (body.velocity.magnitude < 1)
                deadTimer += Time.deltaTime;
            else
                deadTimer = 0;
            stalled = true;

            if (deadTimer > 2 && !dead)
            {
                Instantiate(explosion, transform.position + transform.forward + Vector3.up, transform.rotation);
                dead = true;
            }

            if (liftSpeed > unstallPoint && liftPitch > stallPoint)
                stalled = false;
        }

        transform.FindChild("Burning").gameObject.SetActive(fire_timer > 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        if ((collision.impulse.magnitude >= explosionSpeed && operable) || collision.impulse.magnitude >= explosionSpeed * 2)
        {
            fireStartTimer = 0;
            paralizedTimer = 2;
            if (explosion != null)
                Instantiate(explosion, transform.position + transform.forward, transform.rotation);
        }
    }
}
