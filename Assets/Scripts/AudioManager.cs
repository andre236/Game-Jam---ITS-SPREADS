using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private bool _canPlayAgain = true;

    private GameObject[] _bgms;
    private GameObject[] _sfxs;

    public static AudioManager Instance;

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

        _bgms = GameObject.FindGameObjectsWithTag("BGM");
        _sfxs = GameObject.FindGameObjectsWithTag("SFX");
    }


    public void PlayBGM(int index)
    {
        AudioSource bgmSelected = _bgms[index].GetComponent<AudioSource>();
        bgmSelected.Play();
    }

    public void StopCurrentBGM()
    {
        for (int i = 0; i < _bgms.Length; i++)
        {
            _bgms[i].GetComponent<AudioSource>().Stop();
        }
    }

    public void PlaySFX(int index)
    {
        AudioSource sfxSelected = _sfxs[index].GetComponent<AudioSource>();
        if (_canPlayAgain)
        {
            _canPlayAgain = false;
            sfxSelected.Play();
            StartCoroutine(CooldownToPlayAgain());
        }

    }

    public void MuteAll()
    {
        for (int i = 0; i < _bgms.Length; i++)
        {
            if (_bgms[i].GetComponent<AudioSource>().mute == false)
            {
                _bgms[i].GetComponent<AudioSource>().mute = true;
            }
            else
            {
                _bgms[i].GetComponent<AudioSource>().mute = false;
            }
        }

        for (int i = 0; i < _sfxs.Length; i++)
        {
            if (_sfxs[i].GetComponent<AudioSource>().mute == false)
            {
                _sfxs[i].GetComponent<AudioSource>().mute = true;
            }
            else
            {
                _sfxs[i].GetComponent<AudioSource>().mute = false;
            }
        }
    }

    IEnumerator CooldownToPlayAgain()
    {
        yield return new WaitForSeconds(0.1f);
        _canPlayAgain = true;

    }
}
