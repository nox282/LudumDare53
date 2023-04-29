using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour
{
    public static MinimapController Instance;

    [SerializeField] private GameObject followPrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private GameObject minimapParent;

    private (MinimapObject, GameObject) objectToFollow;
    private Dictionary<MinimapObject, GameObject> objects = new Dictionary<MinimapObject, GameObject>();
    private Coroutine coroutine;

    public void Awake()
    {
        Instance = this;
        coroutine = StartCoroutine(UpdateMinimap());
    }

    public void Add(MinimapObject obj)
    {
        GameObject go = null;
        switch (obj.objectType)
        {
            case MinimapObjectType.PLAYER:
                go = GameObject.Instantiate(followPrefab, minimapParent.transform);
                break;
            case MinimapObjectType.ENEMY:
                go = GameObject.Instantiate(enemyPrefab, minimapParent.transform);
                break;
            case MinimapObjectType.CUBE:
                go = GameObject.Instantiate(cubePrefab, minimapParent.transform);
                break;
        }

        if (obj.toFollow)
        {
            objectToFollow = (obj, go);
        }
        else
        {
            objects[obj] = go;
        }
    }

    public void Remove(MinimapObject obj)
    {
        if (obj.toFollow)
        {
            objectToFollow.Item1 = null;
            objectToFollow.Item2 = null;
        }
        else
        {
            objects.Remove(obj);
        }
    }

    private void OnDestroy()
    {
        StopCoroutine(coroutine);

        Instance = null;
    }

    private IEnumerator UpdateMinimap()
    {
        Vector3 followPos = Vector3.zero;
        const float multiplier = 5f;

        while (true)
        {
            if (objectToFollow.Item1 != null)
            {
                followPos = objectToFollow.Item1.gameObject.transform.position;
                objectToFollow.Item2.transform.localPosition = Vector3.zero;

                Vector3 followRotation = objectToFollow.Item1.gameObject.transform.localEulerAngles;
                Vector3 angle = Vector3.zero; 
                angle.z = 90 - followRotation.y;
                objectToFollow.Item2.transform.localEulerAngles = angle;
            }

            foreach (var o in objects)
            {
                MinimapObject minimapObject = o.Key;
                Transform minimapPoint = o.Value.transform;

                var tempPos = minimapPoint.localPosition;
                tempPos.x = (o.Key.transform.position.x - followPos.x) * multiplier;
                tempPos.y = (o.Key.transform.position.z - followPos.z) * multiplier;
                minimapPoint.localPosition = tempPos;

                if (minimapObject.needsRotationUpdate)
                {
                    Vector3 followRotation = minimapObject.transform.localEulerAngles;
                    Vector3 angle = Vector3.zero;
                    angle.z = 90 - followRotation.y;
                    minimapPoint.transform.localEulerAngles = angle;
                }
            }

            yield return null;
        }
    }
}

public enum MinimapObjectType
{
    PLAYER,
    ENEMY,
    CUBE,
}