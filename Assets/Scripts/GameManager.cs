using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _audioManager;

    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            AudioManager.Instance.PlayBGM(0);
        }
    }

    public void RestartLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentScene);
    }

    public void LoadNextLevelScene()
    {
        AudioManager.Instance.StopCurrentBGM();
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        
        SceneManager.LoadScene(nextScene);
    }

    public void LoadOneScene(int indexScene)
    {

        AudioManager.Instance.StopCurrentBGM();
        AudioManager.Instance.PlaySFX(0);
        SceneManager.LoadScene(indexScene);
    }

}
