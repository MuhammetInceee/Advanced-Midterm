using System;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    public event Action PlayerInput;


    private void Update()
    {
        PlayerInput?.Invoke();
    }
}
