using UnityEngine;

public class QuestManager : MonoBehaviour
{

    private int _allLamps = 0;
    private bool _isOpenedDoor = false;

    private GameObject[] _lamps;
    [SerializeField]
    private Sprite _doorClosedSprite, _doorOpenedSprite;

    public static QuestManager Instance;

    void Awake()
    {
        _lamps = GameObject.FindGameObjectsWithTag("Lamp");
    }

    public bool CheckOpen()
    {
        if (_allLamps == _lamps.Length)
        {
            GetComponent<SpriteRenderer>().sprite = _doorOpenedSprite;
            return _isOpenedDoor = true;
        }
        else
        {
            return _isOpenedDoor = false;
        }
    }

    public int AddExploded()
    {
        return _allLamps++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && _isOpenedDoor)
        {
            GameManager.Instance.LoadNextLevelScene();
        }
       
    }

}
