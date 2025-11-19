using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public const KeyCode FlyButton = KeyCode.Space;
    public const KeyCode FireButton = KeyCode.Return;
    public const KeyCode FireButton2 = KeyCode.KeypadEnter;
    public const KeyCode ResetButton = KeyCode.R;

    public event Action Fly;
    public event Action Fire;
    public event Action Reset;

    private void Update()
    {
        if (Input.GetKeyDown(FlyButton))
        {
            Fly?.Invoke();
        }

        if (Input.GetKeyDown(FireButton) || Input.GetKeyDown(FireButton2))
        {
            Fire?.Invoke();
        }

        if (Input.GetKeyDown(ResetButton))
        {
            InvokeResetAction();
        }
    }

    public void InvokeResetAction()
    {
        Reset?.Invoke();
    }
}
