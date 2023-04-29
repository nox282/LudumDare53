using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScreenComponent : MonoBehaviour
{
    public TriggerComponent Goal;
    public BoxCollider Box;
    public CameraSnapComponent CameraSnapComponent;
    public ScreenComponent NextSceneComponent;
    public List<GuardCharacter> GuardCharacters;
    public Transform StartTransform;

    public bool IsFirstScreen = false;

    public UnityEvent OnGoal;

    private List<Vector3> originalPositions = new List<Vector3>();

    private void Start()
    {
        FindGuardCharacters();

        originalPositions.Clear();
        foreach (var guardCharacter in GuardCharacters)
        {
            originalPositions.Add(guardCharacter.transform.position);
        }

        if (IsFirstScreen)
        {
            Activate();
        }
    }

    private void OnEnable()
    {
        Goal.OnTriggerStayEvent += OnGoalStay;
    }

    private void OnDisable()
    {
        Goal.OnTriggerStayEvent -= OnGoalStay;
    }

    public void Respawn()
    {
        PlayerCharacter.Get.OnRespawn();
        foreach (var guardCharacter in GuardCharacters)
        {
            guardCharacter.PerceptionComponent.OnRespawn();
        }
        Activate();
    }

    public void Activate()
    {
        for (int i = 0; i < Math.Min(GuardCharacters.Count, originalPositions.Count); i++)
        {
            var guardCharacter = GuardCharacters[i];
            var originalPosition = originalPositions[i];
            guardCharacter.transform.position = originalPosition;

            guardCharacter.BehaviorComponent.isPaused = false;
        }

        CameraSnapComponent.Activate();

        PlayerCharacter.Get.transform.position = StartTransform.position;
    }

    private void OnGoalStay(Collider other)
    {
        if (AlertManager.Get.isAlerted)
        {
            return;
        }

        foreach (var guardCharacter in GuardCharacters)
        {
            guardCharacter.BehaviorComponent.isPaused = true;
        }

        if (NextSceneComponent != null)
        {
            NextSceneComponent.Activate();
        }

        OnGoal.Invoke();
    }

    private void FindGuardCharacters()
    {
        Collider[] colliders = Physics.OverlapBox(Box.center, Box.size);

        foreach (Collider collider in colliders)
        {
            GameObject gameObject = collider.gameObject;
            GuardCharacter guardCharacter = gameObject.GetComponent<GuardCharacter>();

            if (guardCharacter != null)
            {
                GuardCharacters.Add(guardCharacter);
            }
        }
    }
}
