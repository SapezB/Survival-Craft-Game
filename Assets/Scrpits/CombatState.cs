using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatState : State
{
    float gravityValue;
    Vector3 currentVelocity;
    bool grounded;
    bool leaveCombat;
    float playerSpeed;
    bool attack;

    public CombatState(Character Character, StateMachine Statemachine) : base(Character, Statemachine)
    {
        character = Character;
        machine = Statemachine;
    }


    public override void Enter()
    {
        base.Enter();

        leaveCombat = false;
        input = Vector2.zero;
        currentVelocity = Vector3.zero;
        gravityVelocity.y = 0;
        attack = false;

        velocity = character.playerVelocity;
        playerSpeed = character.playerSpeed;
        grounded = character.controller.isGrounded;
        gravityValue = character.gravityValue;
    }


    public override void HandleInput()
    {
        base.HandleInput();

        if (enterCombat.triggered)
        {
            leaveCombat = true;
        }

        if (attackAction.triggered)
        {
            attack = true;
        }

        input = moveAction.ReadValue<Vector2>();
        velocity = new Vector3(input.x, 0, input.y);

        velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
        velocity.y = 0f;

    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        character.animator.SetFloat("speed", input.magnitude, character.speedDampTime, Time.deltaTime);

        if (leaveCombat)
        {
            character.animator.SetTrigger("leaveCombat");
            machine.ChangeState(character.standing);
        }

        if (attack)
        {
            character.animator.SetTrigger("attack");
            machine.ChangeState(character.attacking);
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


