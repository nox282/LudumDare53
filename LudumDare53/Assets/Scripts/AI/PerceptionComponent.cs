using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerceptionComponent : MonoBehaviour
{
    [SerializeField] private LayerMask detectionLayerMask;
    [SerializeField] private LayerMask alertLayerMask;
    private GameObject player;
    [SerializeField] private float viewRadius;
    [SerializeField][Range(0, 360)] public float viewAngle;
    [SerializeField] private float closeDetectionRadius;
    public float lostSightBufferTime;

    private GameObject owner;
    public Transform lastKnownPosition;
    public float lastSeen;
    private bool hasPlayerInView = false;

    public Action detectedPlayer;
    public Action lostPlayer;

    [SerializeField] private bool drawDebug = true;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    private void Update()
    {
        bool savedHasPlayerInView = hasPlayerInView;
        float currentTime = Time.realtimeSinceStartup;

        Transform playerTransform = player.transform;
        Transform ownerTransform = transform;
        Vector3 dirToPlayer = (playerTransform.position - ownerTransform.position).normalized;
        if (IsInView(ownerTransform, playerTransform))
        {
            RaycastHit hit;
            LayerMask layerMask = AlertManager.Get.isAlerted ? alertLayerMask : detectionLayerMask;
            bool hasHit = Physics.Raycast(ownerTransform.position, dirToPlayer, out hit, viewRadius, layerMask);
            if (hasHit && hit.transform.gameObject == player)
            {
                hasPlayerInView = true;
                lastSeen = Time.realtimeSinceStartup;
                lastKnownPosition = playerTransform;
            }
        }

        if (savedHasPlayerInView)
        {
            if (currentTime - lastSeen > lostSightBufferTime)
            {
                hasPlayerInView = false;

                Debug.Log($"{gameObject.name} has lost player");
                lostPlayer?.Invoke();
            }
        }
        else if (hasPlayerInView)
        {
            Debug.Log($"{gameObject.name} has detected player");
            detectedPlayer?.Invoke();
        }
    }

    private bool IsInView(Transform ownerTransform, Transform playerTransform)
    {
        bool result = false;
        float distance = Vector2.Distance(playerTransform.position, ownerTransform.position);
        if (distance < closeDetectionRadius)
        {
            result = true;
        }
        else if (distance < viewRadius)
        {
            Vector3 dirToPlayer = (playerTransform.position - ownerTransform.position).normalized;
            float dotResult = Vector3.Dot(playerTransform.forward, dirToPlayer);
            float radAngle = Mathf.Deg2Rad * (viewAngle / 2);
            float cos = Mathf.Cos(radAngle);
            if (dotResult > cos)
            {
                result = true;
            }
        }
        return result;
    }

    private void OnDrawGizmos()
    {
        if (drawDebug)
        {
            Vector3 ownerPos = transform.position;

            Gizmos.color = Color.gray;
            Gizmos.DrawWireSphere(ownerPos, closeDetectionRadius);
            Gizmos.color = hasPlayerInView ? Color.red : Color.green;
            Gizmos.DrawWireSphere(ownerPos, viewRadius);
            float halfAngle = viewAngle / 2;
            Vector3 rightVector = Quaternion.AngleAxis(halfAngle, Vector3.up) * transform.forward;
            Vector3 leftVector = Quaternion.AngleAxis(-halfAngle, Vector3.up) * transform.forward;
            Gizmos.DrawLine(ownerPos, ownerPos + (rightVector * viewRadius));
            Gizmos.DrawLine(ownerPos, ownerPos + (leftVector * viewRadius));
        }
    }
}
