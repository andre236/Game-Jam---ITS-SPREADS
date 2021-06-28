using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Controller2D _playerControl;

    public static GameManager Instance;
    private void Awake()
    {
        _playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller2D>();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}
