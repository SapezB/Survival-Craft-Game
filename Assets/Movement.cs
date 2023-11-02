using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScrpit : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Animator animator;

    private KeyCode keyPressed;

    public float speed = 6f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (Input.anyKeyDown)
        {
            foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(key))
                {
                    keyPressed = key;
                    Debug.Log("Key Pressed: " + keyPressed.ToString());


                    HandleMovementInput(keyPressed);
                }
            }
        }

        if (direction.magnitude >= 0.01f)
        {
            animator.SetBool("IsMoving", true);
            float targetAngle = Mathf.Atan2(direction.x, vertical) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirc = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirc.normalized * speed * Time.deltaTime);
        }

        else
        {
            animator.SetBool("IsMoving", false);
        }

    }

    void HandleMovementInput(KeyCode key)
    {
        Sprinting(key);

        if (key == KeyCode.W)
        {
            animator.SetBool("Forward", true);
            animator.SetBool("Backwards", false);
        }
        else if (key == KeyCode.S)
        {
            animator.SetBool("Backwards", true);
            animator.SetBool("Forward", false);
        }
        else if (key == KeyCode.A)
        {
            animator.SetBool("Left", true);

        }
        else if (key == KeyCode.D)
        {
            animator.SetBool("Right", true);

        }


    }

    void Sprinting(KeyCode key)
    {
        if (key == KeyCode.LeftShift)
        {
            animator.SetBool("Sprinting", true);
            animator.SetBool("test", true);
            Debug.Log("speed changed");
            speed = 10f;


        }
        else if (key == KeyCode.LeftControl)
        {
            animator.SetBool("Sprinting", false);
            animator.SetBool("test", false);
            speed = 6f;

        }
    }


}
