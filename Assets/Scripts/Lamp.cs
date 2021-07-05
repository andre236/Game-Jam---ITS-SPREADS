using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    private Animator _lampAnim;
    private QuestManager _questManagerDoor;
    public bool IsExploded { get; private set; } = false;

    private void Awake()
    {
        _lampAnim = GetComponent<Animator>();
        _questManagerDoor = GameObject.FindGameObjectWithTag("Door").GetComponent<QuestManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !IsExploded)
        {
            _lampAnim.SetBool("IsGettingFire", true);
            StartCoroutine("CooldownToExplode");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !IsExploded)
        {
            _lampAnim.SetBool("IsGettingFire", false);
            StopCoroutine("CooldownToExplode");
        }
    }

    IEnumerator CooldownToExplode()
    {
        yield return new WaitForSeconds(3);
        if (!IsExploded)
        {
            IsExploded = true;
            _lampAnim.SetBool("OnFire", true);
            _questManagerDoor.AddExploded();
            _questManagerDoor.CheckOpen();
        }

    }
}
