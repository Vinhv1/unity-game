using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(CharacterController))]
public class ThirdPersonMovementScript : MonoBehaviour
{
    public float gravity = 9.8f;
    public float speed = 6f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Rigidbody rb;
    public Transform cam;
    CharacterController cc;
    Vector3 currentMovement;

    // Use this for initialization
    void Start()

    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            cc.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        if (!cc.isGrounded)
        {
            Vector3 gravity_move = new Vector3(0, -gravity, 0);
            cc.Move(gravity_move.normalized * speed * Time.deltaTime);
        }
        if (rb.position.y < 0.3) {
            Vector3 correction_vector = new Vector3(0, 1, 0);
            cc.Move(correction_vector.normalized * speed * Time.deltaTime);
        }
        if (rb.position.y > 2)
        {
            Vector3 correction_vector = new Vector3(0, -1, 0);
            cc.Move(correction_vector.normalized * speed * Time.deltaTime);
        }
        Debug.Log(rb.position.y);
    }
}