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
    [SerializeField] private Color _zoneColor;
    [SerializeField] private Color _alertedColor;

    private (MinimapObject, MinimapItem) objectToFollow;
    private Dictionary<MinimapObject, MinimapItem> objects = new Dictionary<MinimapObject, MinimapItem>();
    private Dictionary<MinimapObject, PerceptionComponent> perceptionComponents = new Dictionary<MinimapObject, PerceptionComponent>();
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

        MinimapItem minimapItem = go.GetComponent<MinimapItem>();
        if (obj.toFollow)
        {
            objectToFollow = (obj, minimapItem);
        }
        else
        {
            objects[obj] = minimapItem;
            perceptionComponents[obj] = obj.GetComponent<PerceptionComponent>();
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
            perceptionComponents.Remove(obj);
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

            AlertManager alertManager = AlertManager.Get;
            foreach (var o in objects)
            {
                MinimapObject minimapObject = o.Key;
                Transform minimapPoint = o.Value.transform;
                Material material = o.Value.coneImage?.material;

                var tempPos = minimapPoint.localPosition;
                tempPos.x = (o.Key.transform.position.x - followPos.x) * multiplier;
                tempPos.y = (o.Key.transform.position.z - followPos.z) * multiplier;
                minimapPoint.localPosition = tempPos;

                perceptionComponents.TryGetValue(o.Key, out PerceptionComponent perception);
                float perceptionAngle = 0;
                if (perception != null)
                {
                    perceptionAngle = alertManager.isAlerted ? perception.viewAngleInAlertMode : perception.viewAngle;
                    material.SetFloat("_Angle", perceptionAngle);
                    material.SetColor("_Color", alertManager.isAlerted ? _alertedColor : _zoneColor);

                    Vector3 scale = Vector3.one;
                    if (alertManager.isAlerted)
                    {
                        // lol
                        scale.x = 1.42f;
                        scale.y = 1.42f;
                    }
                    minimapPoint.transform.localScale = scale;
                }

                if (minimapObject.needsRotationUpdate)
                {
                    Vector3 followRotation = minimapObject.transform.localEulerAngles;
                    Vector3 angle = Vector3.zero;
                    angle.z = 90 - followRotation.y - (perceptionAngle / 2f);
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