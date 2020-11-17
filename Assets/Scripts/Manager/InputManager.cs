using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Joystick joystick = null;

    private void Update()
    {
        JoystickInput();
        KeyboardInput();
    }

    private void JoystickInput()
    {
        Vector3 movement = Vector3.zero;

        movement.x = joystick.Horizontal;
        movement.z = joystick.Vertical;

        if (movement.x != 0 || movement.y != 0)
        {
            Messenger<Vector3>.Broadcast(GameEvent.MOVE, movement);
        }
    }

    private void KeyboardInput()
    {
        Vector3 movement = Vector3.zero;

        float horInput = Input.GetAxis("Horizontal");
        float verInput = Input.GetAxis("Vertical");

        if (horInput != 0 || verInput != 0)
        {
            movement.x = horInput;
            movement.z = verInput;
            movement.Normalize();
            Messenger<Vector3>.Broadcast(GameEvent.MOVE, movement);
        }
    }
}
