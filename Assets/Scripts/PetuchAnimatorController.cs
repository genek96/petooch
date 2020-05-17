using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetuchAnimatorController : MonoBehaviour
{
    public GlobalSceneState globalSceneState;
    private Animator petuchAnimator;
    private static readonly int State = Animator.StringToHash("state");

    public void Start()
    {
        petuchAnimator = GetComponent<Animator>();
        globalSceneState.OnSceneStateUpdated += OnStateUpdated;
    }

    public void OnStateUpdated(SceneState newState)
    {
        Debug.unityLogger.Log($"New state is {newState}");
        petuchAnimator.SetInteger(State, (int)newState);
    }
}
