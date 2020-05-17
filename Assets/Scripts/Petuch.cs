using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Petuch : MonoBehaviour
{
    public GlobalSceneState state;
    public int maxAxisSpeed = 5;

    private Vector3 tableSpeed;

    public Vector3 TableSpeed
    {
        set
        {
            tableSpeed = value;
            UpdateFinalSpeed();
        }
        private get => tableSpeed;
    }

    private Vector3 ownSpeed;
    private Vector3 finalSpeed;

    private float elapsedSecondsAfterLastSpeedUpdate;

    public float minPeriodForUpdateSpeed = 0.5f;
    public float maxPeriodForUpdateSpeed = 3f;

    // Start is called before the first frame update
    private void Start()
    {
        TableSpeed = Vector3.zero;
        state.OnSceneStateUpdated += OnSceneStateUpdated;
        UpdateOwnSpeed();
    }

    private void Update()
    {
        if (state.CurrentState != SceneState.Active)
        {
            return;
        }

        elapsedSecondsAfterLastSpeedUpdate += Time.deltaTime;
        if (elapsedSecondsAfterLastSpeedUpdate > minPeriodForUpdateSpeed)
        {
            if (elapsedSecondsAfterLastSpeedUpdate > Random.Range(minPeriodForUpdateSpeed, maxPeriodForUpdateSpeed))
            {
                UpdateOwnSpeed();
            }
        }

        transform.position += GetTranslate();
    }

    private void OnSceneStateUpdated(SceneState newState)
    {
        if (newState == SceneState.Beginning)
        {
            transform.position = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("gameOver"))
        {
            state.CurrentState = SceneState.GameOver;
            Debug.unityLogger.Log("Game Over");
        }
    }

    private Vector3 GetTranslate() => new Vector3(
        finalSpeed.x * Time.deltaTime,
        finalSpeed.y,
        finalSpeed.z * Time.deltaTime);

    private void UpdateOwnSpeed()
    {
        ownSpeed.x = Random.Range(-maxAxisSpeed, (float)maxAxisSpeed);
        ownSpeed.z = (float) Math.Sqrt(maxAxisSpeed * maxAxisSpeed - ownSpeed.x * ownSpeed.x)
                     * (Random.value > 0.5 ? 1 : -1);
        
        transform.rotation = Quaternion.Euler(
            0,
            Vector3.Angle(Vector3.forward, ownSpeed) * (ownSpeed.x > 0 ? 1 : -1),
            0);
        
        UpdateFinalSpeed();
        elapsedSecondsAfterLastSpeedUpdate = 0f;
    }

    private void UpdateFinalSpeed()
    {
        finalSpeed = ownSpeed + TableSpeed;
    }
}