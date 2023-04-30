using UnityEngine;

public class GameLoopMusicController : MonoBehaviour
{
    public AudioSource IdleMusic;
    public AudioSource AlertMusic;
    public AudioSource AlarmSound;

    private void Start()
    {
        AlertManager.Get.OnAlertOff += OnAlertOff;
        AlertManager.Get.OnAlertOn += OnAlertOn;
    }

    private void OnDestroy()
    {
        AlertManager.Get.OnAlertOff -= OnAlertOff;
        AlertManager.Get.OnAlertOn -= OnAlertOn;
    }

    private void OnEnable()
    {
        PlayIdle();
    }

    private void OnDisable()
    {
        StopMusic();
    }

    private void OnAlertOn()
    {
        PlayAlert();
    }

    private void OnAlertOff()
    {
        PlayIdle();
    }

    private void StopMusic()
    {
        IdleMusic.Stop();
        AlertMusic.Stop();
    }

    private void PlayAlert()
    {
        IdleMusic.Stop();
        AlertMusic.Play();
        AlarmSound.Play();
    }

    private void PlayIdle()
    {
        IdleMusic.Play();
        AlertMusic.Stop();
        AlarmSound.Stop();
    }
}
