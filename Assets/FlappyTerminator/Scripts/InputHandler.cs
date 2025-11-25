using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Game _game;

    public const KeyCode FlyButton = KeyCode.Space;
    public const KeyCode FireButton = KeyCode.Return;
    public const KeyCode FireButton2 = KeyCode.KeypadEnter;
    public const KeyCode ResetButton = KeyCode.R;

    public const int MouseFlyButton = 0;
    public const int MouseFireButton = 1;

    public event Action Fly;
    public event Action Fire;
    public event Action Reset;

    private void Update()
    {
        if (_game.IsPaused == false)
        {
            if (Input.GetKeyDown(FlyButton) || Input.GetMouseButtonDown(MouseFlyButton))
            {
                Fly?.Invoke();
            }

            if (Input.GetKeyDown(FireButton) || Input.GetKeyDown(FireButton2) || Input.GetMouseButtonDown(MouseFireButton))
            {
                Fire?.Invoke();
            }
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
