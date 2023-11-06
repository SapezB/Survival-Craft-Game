using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class State 
{
    public Character character;
    public StateMachine machine;

    protected Vector3 velocity;
    protected Vector2 input;
    protected Vector3 gravityVelocity;


    public InputAction moveAction;
    public InputAction lookAction;
    public InputAction sprintAction;
    public InputAction enterCombat;
    public InputAction attackAction;

    public State(Character Character, StateMachine Statemachine)
    {
        character = Character;
        machine = Statemachine;

        moveAction = character.playerInput.actions["Move"];
        lookAction = character.playerInput.actions["Look"];
        sprintAction = character.playerInput.actions["Sprint"];
        enterCombat = character.playerInput.actions["enterCombat"];
        attackAction = character.playerInput.actions["Attack"];
    }

    public virtual void Enter()
    {
        //Debug.Log("enter state: " + this.ToString());

    }

    public virtual void HandleInput()
    {

    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }
    public virtual void Exit()
    {

    }



}
