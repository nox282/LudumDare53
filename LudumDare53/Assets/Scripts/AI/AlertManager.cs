using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AlertManager : MonoBehaviour
{
    public static AlertManager Get { get; private set; }

    public Action OnAlertOn;
    public Action OnAlertOff;

    public AlertBehavior AlertBehavior;
    public GameObject CanvasPrefab;
    public AudioSource AudioSource;

    public float AlertFadeInSeconds = 3f;

    private List<GuardCharacter> guardCharacters = new List<GuardCharacter>();
    public bool isAlerted = false;
    private int guardThatDetectedThePlayer = 0;
    private GameObject canvasGO;
    private float AlertFadeElapsed = 0f;
    public int AlertLevel = 0;

    private void Awake()
    {
        Get = this;

        canvasGO = Instantiate(CanvasPrefab, transform);
        canvasGO.SetActive(false);

        AudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        guardCharacters = FindObjectsOfType<GuardCharacter>(includeInactive: true).ToList();

        foreach (var guardCharacter in guardCharacters)
        {
            if (guardCharacter != null)
            {
                guardCharacter.PerceptionComponent.detectedPlayer += OnPlayerDetected;
                guardCharacter.PerceptionComponent.lostPlayer += OnPlayerLost;
            }
        }
    }

    private void OnDestroy()
    {
        foreach (var guardCharacter in guardCharacters)
        {
            if (guardCharacter != null)
            {
                guardCharacter.PerceptionComponent.detectedPlayer -= OnPlayerDetected;
                guardCharacter.PerceptionComponent.lostPlayer -= OnPlayerLost;
            }
        }
    }

    private void Update()
    {
        if (!isAlerted)
        {
            return;
        }

        if (guardThatDetectedThePlayer > 0)
        {
            AlertFadeElapsed = 0;
        }
        else
        {
            ChangeAlertLevel(1);
        }

        AlertFadeElapsed += Time.deltaTime;

        TryStopAlert();
    }

    // Gilbert dans taxi.
    public void ALERTEGENERAAAAAAAAAAAAAAAAAALE()
    {
        ChangeAlertLevel(2);
        if (isAlerted)
        {
            return;
        }

        AlertFadeElapsed = 0f;
        isAlerted = true;
        canvasGO.SetActive(isAlerted);
        AudioSource.Play();

        foreach (var guardCharacter in guardCharacters)
        {
            guardCharacter.BehaviorComponent.SetBehavior(AlertBehavior);
        }

        OnAlertOn?.Invoke();
    }

    public void TryStopAlert(bool isRespawning = false)
    {
        if (!isAlerted)
        {
            return;
        }

        if (guardThatDetectedThePlayer > 0)
        {
            return;
        }

        if (!isRespawning && AlertFadeElapsed < AlertFadeInSeconds)
        {
            return;
        }

        guardThatDetectedThePlayer = 0;

        isAlerted = false;
        ChangeAlertLevel(0);
        canvasGO.SetActive(isAlerted);

        foreach (var guardCharacter in guardCharacters)
        {
            guardCharacter.BehaviorComponent.SetIdle();
        }

        OnAlertOff?.Invoke();
    }

    private void OnPlayerDetected()
    {
        guardThatDetectedThePlayer++;
        ALERTEGENERAAAAAAAAAAAAAAAAAALE();
    }

    private void OnPlayerLost(bool isRespawning = false)
    {
        guardThatDetectedThePlayer--;
        TryStopAlert(isRespawning);
    }

    private void ChangeAlertLevel(int level)
    {
        if (AlertLevel == level)
        {
            return;
        }
        AlertLevel = level;

        if (AlertLevel > 0 && canvasGO.transform.childCount > 0)
        {
            Color color = AlertLevel == 1 ? Color.yellow : Color.red;
            color.a = 0.5f;

            Image image = canvasGO.transform.GetChild(0).GetComponent<Image>();
            if (image != null)
            {
                image.color = color;
            }
        }
    }
}