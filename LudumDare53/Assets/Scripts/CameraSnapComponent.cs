using System;
using System.Collections;
using UnityEngine;

public class CameraSnapComponent : MonoBehaviour
{
    public BoxCollider BoxCollider;
    public float ProjectedOffset = 500;
    public float θ = 45f;
    public float TransitionDuration = .5f;

    private Camera mainCamera;

    public void Activate(Action callback)
    {
        float offset = Mathf.Abs(ProjectedOffset / θ);

        Quaternion rotation = Quaternion.AngleAxis(θ, Vector3.right);
        Vector3 rotatedVector = rotation * Vector3.back;

        Debug.DrawLine(transform.position, transform.position + rotatedVector * 500, Color.red, Time.deltaTime);

        StartCoroutine(TransitionRoutine(
            targetPosition: transform.position + rotatedVector * offset,
            targetOrthographicSize: BoxCollider.size.z * 0.5f,
            targetAspect: BoxCollider.size.x / BoxCollider.size.z,
            TransitionDuration,
            callback));
    }

    IEnumerator TransitionRoutine(Vector3 targetPosition, float targetOrthographicSize, float targetAspect, float duration, Action callback)
    {
        PlayerCharacter.Get.InputComponent.enabled = false;

        Vector3 startPosition = Camera.main.transform.position;
        float startOrthographicSize = Camera.main.orthographicSize;
        float startAspect = Camera.main.aspect;
        Vector3 distance = targetPosition - startPosition;
        float startTime = Time.time;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float progress = elapsedTime / duration;

            Camera.main.transform.position = startPosition + distance * progress;
            Camera.main.orthographicSize = Mathf.Lerp(startOrthographicSize, targetOrthographicSize, progress);
            Camera.main.aspect = Mathf.Lerp(startAspect, targetAspect, progress);

            elapsedTime = Time.time - startTime;
            yield return null;
        }

        Camera.main.transform.position = targetPosition;
        Camera.main.orthographicSize = targetOrthographicSize;
        Camera.main.aspect = targetAspect;

        PlayerCharacter.Get.InputComponent.enabled = true;

        callback?.Invoke();
    }
}
