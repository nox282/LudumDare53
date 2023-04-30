using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScreenComponent : MonoBehaviour
{
    public BoxCollider Box;
    public CameraSnapComponent CameraSnapComponent;
    public ScreenComponent NextSceneComponent;
    public List<GuardCharacter> GuardCharacters;
    public Vector2 aspectRatio = new Vector2(16, 10);

    public bool IsFirstScreen = false;
    public Transform FirstStartTransform;

    public UnityEvent OnGoal;

    private List<Vector3> originalPositions = new List<Vector3>();

	private Vector3 _respawnPosition;

    private void Start()
    {
        FindGuardCharacters();

        originalPositions.Clear();
        foreach (var guardCharacter in GuardCharacters)
        {
            originalPositions.Add(guardCharacter.transform.position);
            guardCharacter.BehaviorComponent.enabled = false;
        }

        if (IsFirstScreen)
        {
            Activate(FirstStartTransform.position);
        }
    }

    public void Respawn()
    {
        PlayerCharacter.Get.OnRespawn();
        foreach (var guardCharacter in GuardCharacters)
        {
            guardCharacter.PerceptionComponent.OnRespawn();
        }
        Activate(_respawnPosition);
    }

    public void Activate(Vector3 spawnPoint)
    {
		_respawnPosition = spawnPoint;

		PlayerCharacter.Get.transform.position = spawnPoint;

        for (int i = 0; i < Math.Min(GuardCharacters.Count, originalPositions.Count); i++)
        {
            var guardCharacter = GuardCharacters[i];
            var originalPosition = originalPositions[i];
            guardCharacter.transform.position = originalPosition;
        }

        CameraSnapComponent.Activate(() =>
        {
            foreach (var guardCharacter in GuardCharacters)
            {
                guardCharacter.BehaviorComponent.enabled = true;
            }
        });
    }

    public void GoalReached(ScreenStart nextScreen)
    {
        if (AlertManager.Get.isAlerted)
        {
            return;
        }

        foreach (var guardCharacter in GuardCharacters)
        {
            guardCharacter.BehaviorComponent.enabled = false;
        }

        if (nextScreen.Screen != null)
        {
			nextScreen.Screen.Activate(nextScreen.transform.position);
        }
        else if (!PlayerCharacter.Get.isStamped)
        {
            return;
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

    private void OnValidate()
    {
        var aspect = Box.size.x / Box.size.z;
        float targetAspect = aspectRatio.x / aspectRatio.y;
        if (Mathf.Abs(aspect - targetAspect) > float.Epsilon)
        {
            Vector3 size = Box.size;
            size.z = Box.size.x / targetAspect;
            Box.size = size;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        if (Box != null)
        {
            Vector3 center = Box.center + transform.position;
            Vector3 size = Box.size;

            // Draw a wireframe box using Gizmos
            Gizmos.DrawWireCube(center, size);
        }
    }
}
