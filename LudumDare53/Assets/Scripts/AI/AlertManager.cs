using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AlertManager : MonoBehaviour
{
    public static AlertManager Get { get; private set; }

    public Action OnAlertOn;
    public Action OnAlertOff;

    public AlertBehavior AlertBehavior;
    public GameObject CanvasPrefab;
    public AudioSource AudioSource;

    private List<GuardCharacter> guardCharacters = new List<GuardCharacter>();
    public bool isAlerted = false;
    private int guardThatDetectedThePlayer = 0;
    private GameObject canvasGO;

    private void Awake()
    {
        Get = this;

        canvasGO = Instantiate(CanvasPrefab, transform);
        canvasGO.SetActive(false);

        AudioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        guardCharacters = FindObjectsOfType<GuardCharacter>().ToList();

        foreach (var guardCharacter in guardCharacters)
        {
            if (guardCharacter != null)
            {
                guardCharacter.PerceptionComponent.detectedPlayer += OnPlayerDetected;
                guardCharacter.PerceptionComponent.lostPlayer += OnPlayerLost;
            }
        }
    }

    private void OnDisable()
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

    // Gilbert dans taxi.
    public void ALERTEGENERAAAAAAAAAAAAAAAAAALE()
    {
        if (isAlerted)
        {
            return;
        }

        isAlerted = true;
        canvasGO.SetActive(isAlerted);
        AudioSource.Play();

        foreach (var guardCharacter in guardCharacters)
        {
            guardCharacter.BehaviorComponent.SetBehavior(AlertBehavior);
        }

        OnAlertOn?.Invoke();
    }

    public void TryStopAlert()
    {
        if (!isAlerted)
        {
            return;
        }

        if (guardThatDetectedThePlayer > 0)
        {
            return;
        }

        isAlerted = false;
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

    private void OnPlayerLost()
    {
        guardThatDetectedThePlayer--;
        TryStopAlert();
    }
}