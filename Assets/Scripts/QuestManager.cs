using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    private int _allPopcorns = 0;
    private bool _isOpenedDoor = false;

    private GameObject[] _popcorns;
    [SerializeField]
    private Sprite _doorClosedSprite, _doorOpenedSprite;

    public static QuestManager Instance;

    void Awake()
    {
        _popcorns = GameObject.FindGameObjectsWithTag("Popcorn");
    }

    public bool CheckOpen()
    {
        if (_allPopcorns == _popcorns.Length)
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
        return _allPopcorns++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && _isOpenedDoor)
        {
            Debug.Log("Você passou de fase!");
        }
       
    }

}
