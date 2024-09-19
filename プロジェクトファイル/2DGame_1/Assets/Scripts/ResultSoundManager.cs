using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultSoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _resultBGMSource;
    [SerializeField] private AudioSource _buttonSESource;
    [SerializeField] private AudioSource _sceneChangeSESource;
    [SerializeField] private AudioSource _reloadSESource;
    [SerializeField] private AudioSource _targetHitSESource;
    [SerializeField] private AudioSource _animStopSESource;
    [SerializeField] private AudioSource _scoreUpdateSESource;
    [SerializeField] private AudioSource _targetSummonSESource;
    [SerializeField] private AudioSource _bonusTargetSESource;

    private const float kMaxBGMVol = 0.3f;
    private const float kFadeOutTime = 3.0f;

    private bool isResultBGMFadeOut = false;
    private float fadeTime = 0.0f;

    public bool IsResultBGMFadeOut { set { isResultBGMFadeOut = value; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isResultBGMFadeOut)
        {
            fadeTime += Time.deltaTime;
            if (fadeTime >= kFadeOutTime)
            {
                fadeTime = kFadeOutTime;
                isResultBGMFadeOut = false;
            }
            float vol = (float)(1.0 - fadeTime / kFadeOutTime);
            _resultBGMSource.volume = vol * kMaxBGMVol;
        }
    }

    public void PlayResultBGM()
    {
        _resultBGMSource.volume = kMaxBGMVol;
        _resultBGMSource.time = 0.0f;
        _resultBGMSource.pitch = 1.0f;
        _resultBGMSource.Play();
    }

    public void PlayButtonSE()
    {
        _buttonSESource.volume = 1.0f;
        _buttonSESource.time = 0.0f;
        _buttonSESource.pitch = 1.0f;
        _buttonSESource.PlayOneShot(_buttonSESource.clip);
    }

    public void PlaySceneChangeSE()
    {
        _sceneChangeSESource.volume = 1.0f;
        _sceneChangeSESource.time = 0.0f;
        _sceneChangeSESource.pitch = 1.0f;
        _sceneChangeSESource.PlayOneShot(_sceneChangeSESource.clip);
    }

    public void PlayReloadSE()
    {
        _reloadSESource.volume = 1.0f;
        _reloadSESource.time = 0.0f;
        _reloadSESource.pitch = 1.0f;
        _reloadSESource.PlayOneShot(_reloadSESource.clip);
    }

    public void PlayTargetHitSE()
    {
        _targetHitSESource.volume = 0.6f;
        _targetHitSESource.time = 0.0f;
        _targetHitSESource.pitch = 1.0f;
        _targetHitSESource.PlayOneShot(_targetHitSESource.clip);
    }

    public void PlayAnimStopSE()
    {
        _animStopSESource.volume = 1.0f;
        _animStopSESource.time = 0.0f;
        _animStopSESource.pitch = 1.0f;
        _animStopSESource.PlayOneShot(_animStopSESource.clip);
    }

    public void PlayScoreUpdateSE()
    {
        _scoreUpdateSESource.volume = 1.0f;
        _scoreUpdateSESource.time = 0.0f;
        _scoreUpdateSESource.pitch = 1.0f;
        _scoreUpdateSESource.PlayOneShot(_scoreUpdateSESource.clip);
    }

    public void PlayTargetSummonSE()
    {
        _targetSummonSESource.volume = 1.0f;
        _targetSummonSESource.time = 0.0f;
        _targetSummonSESource.pitch = 1.0f;
        _targetSummonSESource.PlayOneShot(_targetSummonSESource.clip);
    }

    public void PlayBonusTargetSE()
    {
        _bonusTargetSESource.volume = 0.7f;
        _bonusTargetSESource.time = 0.0f;
        _bonusTargetSESource.pitch = 1.0f;
        _bonusTargetSESource.PlayOneShot(_bonusTargetSESource.clip);
    }
}
