using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintState : State
{
    float gravityValue;
    Vector3 currentVelocity;

    
    bool sprint;
    float playerSpeed;
    bool grounded;

    Vector3 cVelocity;
    public SprintState(Character _character, StateMachine stateMachine) : base(_character, stateMachine)
    {
        character = _character;
        machine = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        sprint = false;
     
        input = Vector2.zero;
        velocity = Vector3.zero;
        currentVelocity = Vector3.zero;
        gravityVelocity.y = 0;

        playerSpeed = character.sprintSpeed;
        gravityValue = character.gravityValue;
        grounded = character.controller.isGrounded;
    }

    public override void HandleInput()
    {
        base.Enter();

        input = moveAction.ReadValue<Vector2>();
        velocity = new Vector3(input.x, 0, input.y);

        velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
        velocity.y = 0f;
        if (sprintAction.triggered || input.sqrMagnitude == 0f)
        {
            sprint = false;
        }
        else
        {
            sprint = true;
        }
    }


    public override void LogicUpdate()
    {
        if (sprint)
        {
            character.animator.SetFloat("speed", input.magnitude + 0.5f, character.speedDampTime, Time.deltaTime);
        }
        else
        {
            machine.ChangeState(character.standing);
        }

    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        gravityVelocity.y += gravityValue * Time.deltaTime;
        grounded = character.controller.isGrounded;
        if (grounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = 0f;
        }

        currentVelocity = velocity;

        character.controller.Move(currentVelocity * Time.deltaTime * playerSpeed  +gravityVelocity * Time.deltaTime);


        if (velocity.sqrMagnitude > 0)
        {
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(velocity), character.rotationDampTime);
        }
    }
}
