using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _titleBGMSource;
    [SerializeField] private AudioSource _buttonSESource;
    [SerializeField] private AudioSource _buttonClickSE1Source;
    [SerializeField] private AudioSource _errorSESource;
    [SerializeField] private AudioSource _sceneChangeSESource;

    private const float kMaxBGMVol = 0.3f;
    private const float kFadeOutTime = 3.0f;

    private bool isTitleBGMFadeOut = false;
    private float fadeTime = 0.0f;

    public bool IsTitleBGMFadeOut { set { isTitleBGMFadeOut = value; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTitleBGMFadeOut)
        {
            fadeTime += Time.deltaTime;
            if (fadeTime >= kFadeOutTime)
            {
                fadeTime = kFadeOutTime;
                isTitleBGMFadeOut = false;
            }
            float vol = (float)(1.0 - fadeTime / kFadeOutTime);
            _titleBGMSource.volume = vol * kMaxBGMVol;
        }
    }

    public void PlayTitleBGM()
    {
        _titleBGMSource.volume = kMaxBGMVol;
        _titleBGMSource.time = 0.0f;
        _titleBGMSource.pitch = 1.0f;
        _titleBGMSource.Play();
    }

    public void PlayButtonSE()
    {
        _buttonSESource.volume = 1.0f;
        _buttonSESource.time = 0.0f;
        _buttonSESource.pitch = 1.0f;
        _buttonSESource.PlayOneShot(_buttonSESource.clip);
    }

    public void PlayButtonClickSE1()
    {
        _buttonClickSE1Source.volume = 1.0f;
        _buttonClickSE1Source.time = 10.0f;
        _buttonClickSE1Source.pitch = 1.0f;
        _buttonClickSE1Source.PlayOneShot(_buttonClickSE1Source.clip);
    }

    public void PlayErrorSE()
    {
        _errorSESource.volume = 1.0f;
        _errorSESource.time = 10.0f;
        _errorSESource.pitch = 1.0f;
        _errorSESource.PlayOneShot(_errorSESource.clip);
    }

    public void PlaySceneChangeSE()
    {
        _sceneChangeSESource.volume = 1.0f;
        _sceneChangeSESource.time = 0.0f;
        _sceneChangeSESource.pitch = 1.0f;
        _sceneChangeSESource.PlayOneShot(_sceneChangeSESource.clip);
    }
}
