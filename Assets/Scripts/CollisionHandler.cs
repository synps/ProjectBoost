using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip successFx, crashFx;
    [SerializeField] ParticleSystem successParticles, crashParticles;

    AudioSource audioSource;
    bool isTranstioning = false,
    isCollisionDisabled = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKey(KeyCode.L))
            NextLevel();
        else if (Input.GetKey(KeyCode.C))
            isCollisionDisabled = !isCollisionDisabled;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isTranstioning || isCollisionDisabled)
            return;
        switch (other.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Fuel":
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }
    void StartCrashSequence()
    {
        isTranstioning = true;
        audioSource.volume = 1f;
        audioSource.clip = crashFx;
        audioSource.loop = false;
        audioSource.Stop();
        audioSource.Play();
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }
    void StartSuccessSequence()
    {
        isTranstioning = true;
        audioSource.volume = 1f;
        audioSource.clip = successFx;
        audioSource.loop = false;
        audioSource.Stop();
        audioSource.Play();
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", levelLoadDelay);

    }


    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        isTranstioning = false;
    }
    void NextLevel()
    {
        var currentLevel = SceneManager.GetActiveScene().buildIndex;
        var nextLevel = currentLevel + 1;
        if (nextLevel == SceneManager.sceneCountInBuildSettings)
        {
            nextLevel = 0;
        }
        SceneManager.LoadScene(nextLevel);
        isTranstioning = false;
    }
}
