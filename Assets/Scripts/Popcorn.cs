using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popcorn : MonoBehaviour
{
    [SerializeField]
    private Sprite _defaultSprite, _explodedSprite;

    private QuestManager _questManagerDoor;
    public bool IsExploded { get; private set; } = false;

    private void Awake()
    {
        _questManagerDoor = GameObject.FindGameObjectWithTag("Door").GetComponent<QuestManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !IsExploded)
        {
            StartCoroutine("CooldownToExplode");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !IsExploded)
        {
            StopCoroutine("CooldownToExplode");
        }
    }

    IEnumerator CooldownToExplode()
    {
        yield return new WaitForSeconds(3);
        if (!IsExploded)
        {
            IsExploded = true;
            GetComponent<SpriteRenderer>().sprite = _explodedSprite;
            _questManagerDoor.AddExploded();
            _questManagerDoor.CheckOpen();
        }
    
    }
}
