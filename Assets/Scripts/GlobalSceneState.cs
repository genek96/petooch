using System;
using UnityEngine;

public class GlobalSceneState : MonoBehaviour
{
    private SceneState currentState = SceneState.Beginning;
    public float startDelay = 3f;

    public SceneState CurrentState
    {
        get => currentState;
        set
        {
            currentState = value;
            OnSceneStateUpdated(value);
        }
    }

    public event Action<SceneState> OnSceneStateUpdated;
}