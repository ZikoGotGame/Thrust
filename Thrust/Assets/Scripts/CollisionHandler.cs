using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float delay = 2f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isTransitioning;
    bool collisionDisabled;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isTransitioning = false;
        collisionDisabled = false;
    }

    void Update()
    {
        SkipLevel();
        DisableCollisions();
    }

    void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning || collisionDisabled) return;

        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly object!");
                break;
            case "Finish":
                NextLevelSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        crashParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        GetComponent<Movement>().enabled = false; // End player movement.
        Invoke("ReloadLevel", delay);
    }

    void NextLevelSequence()
    {
        isTransitioning = true;
        successParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        GetComponent<Movement>().enabled = false; // End player movement.
        Invoke("LoadNextLevel", delay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex); // reloads the current scene.
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex + 1 == SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0); // Loop back to first level.
        }
        else
        {
            SceneManager.LoadScene(currentSceneIndex + 1); // Load next level.
        }
    }

    void SkipLevel()
    {
        if(Input.GetKey(KeyCode.L))
        {
            LoadNextLevel();
        }
    }

    void DisableCollisions()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }
}
