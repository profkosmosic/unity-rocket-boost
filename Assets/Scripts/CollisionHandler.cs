using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField]float levelDelay = 1f;
    [SerializeField]AudioClip failSound;
    [SerializeField]AudioClip winSound;
    [SerializeField]ParticleSystem win;
    [SerializeField]ParticleSystem fail;
    private AudioSource sounds;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    private void Start() {
        sounds = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other) {
        if(isTransitioning == false && collisionDisabled == false) {
            switch(other.gameObject.tag) {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
            }
        }
    }
    void StartSuccessSequence() {
        isTransitioning = true;
        sounds.Stop();
        GetComponent<Movement>().enabled = false;
        sounds.PlayOneShot(winSound);
        win.Play();
        Invoke("NextLevel", levelDelay);
    }
    void StartCrashSequence() {
        isTransitioning = true;
        sounds.Stop();
        GetComponent<Movement>().enabled = false;
        sounds.PlayOneShot(failSound);
        fail.Play();
        Invoke("ReloadLevel", levelDelay);
    }
    void ReloadLevel() {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
    void NextLevel() {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if(nextScene == SceneManager.sceneCountInBuildSettings) {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }
    void Update() {
        if(Input.GetKeyDown(KeyCode.L)) {
            StartSuccessSequence();
        }
        else if(Input.GetKeyDown(KeyCode.C)) {
            collisionDisabled = !collisionDisabled;
        }
        else if(Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }
}
