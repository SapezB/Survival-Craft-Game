using System.Collections;

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class StandingState : State

{
    float gravityValue;
    Vector3 currentVelocity;
    bool sprint;
    bool grounded;
    float playerSpeed;
    bool inCombat;
    

    Vector3 cVelocity;


    public StandingState(Character Character, StateMachine Statemachine) : base(Character, Statemachine)
    {
        character = Character;
        machine = Statemachine;
    }


    public override void Enter()
    {
        base.Enter();

        inCombat = false;
        sprint = false;
        input = Vector2.zero;
        
        currentVelocity = Vector3.zero;
        gravityVelocity.y = 0;

        velocity = character.playerVelocity;
        playerSpeed = character.playerSpeed;
        gravityValue = character.gravityValue;
        grounded = character.controller.isGrounded;


    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (sprintAction.triggered)
        {
            sprint = true;
        }
        if (base.enterCombat.triggered)
        {
            inCombat = true;
        }

        input = moveAction.ReadValue<Vector2>();
        velocity = new Vector3(input.x, 0, input.y);

        velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
        velocity.y = 0f;
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        character.animator.SetFloat("speed", input.magnitude, 0, Time.deltaTime);

        if (sprint)
        {
            machine.ChangeState(character.sprinting);
        }
        if (inCombat)
        {
            machine.ChangeState(character.combat);
            character.animator.SetTrigger("enterCombat");
        }

    }



    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        gravityVelocity.y += gravityValue * Time.deltaTime;
        grounded = character.controller.isGrounded;

        if  ( grounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = 0f;
        }



        currentVelocity = velocity;
        character.controller.Move(currentVelocity * Time.deltaTime * playerSpeed + gravityVelocity * Time.deltaTime);



        if (velocity.sqrMagnitude > 0)
        {
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(velocity), character.rotationDampTime);
        }
    }
    public override void Exit()
    {
        base.Exit();
        gravityVelocity.y = 0f;

        character.playerVelocity = new Vector3(input.x, 0, input.y);

        if (velocity.sqrMagnitude > 0)
        {
            character.transform.rotation = Quaternion.LookRotation(velocity);
        }
    }
}
