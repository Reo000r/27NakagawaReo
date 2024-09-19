using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Threading.Tasks;  // async

public class GameSceneSoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _shotSESource;
    [SerializeField] private AudioSource _targetSummonSESource;
    [SerializeField] private AudioSource _targetRotaEndSESource;
    [SerializeField] private AudioSource _targetHitSESource;
    [SerializeField] private AudioSource _bonusTargetSESource;
    [SerializeField] private AudioSource _demeritTargetSESource;

    [SerializeField] private AudioSource _gameStartSE1Source;
    [SerializeField] private AudioSource _gameStartSE2Source;
    [SerializeField] private AudioSource _gamePatternUpdateSESource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("click");
            PlayShotSE();
        }
    }


    public void PlayShotSE()
    {
        _shotSESource.volume = 0.3f;
        _shotSESource.time = 0.15f;
        _shotSESource.pitch = 1.0f;
        _shotSESource.Play();
    }

    public void PlayTargetSummonSE()
    {
        _targetSummonSESource.volume = 0.15f;
        _targetSummonSESource.time = 0.0f;
        _targetSummonSESource.pitch = 1.3f;
        _targetSummonSESource.PlayOneShot(_targetSummonSESource.clip);
    }

    public void PlayTargetHitSE()
    {
        _targetHitSESource.volume = 0.4f;
        _targetHitSESource.time = 0.0f;
        _targetHitSESource.pitch = 1.1f;
        _targetHitSESource.PlayOneShot(_targetHitSESource.clip);
    }

    public void PlayBonusTargetHitSE()
    {
        _bonusTargetSESource.volume = 0.6f;
        _bonusTargetSESource.time = 0.0f;
        _bonusTargetSESource.pitch = 1.0f;
        _bonusTargetSESource.PlayOneShot(_bonusTargetSESource.clip);
    }

    public void PlayDemeritTargetHitSE()
    {
        _demeritTargetSESource.volume = 1.0f;
        _demeritTargetSESource.time = 0.0f;
        _demeritTargetSESource.pitch = 1.0f;
        _demeritTargetSESource.PlayOneShot(_demeritTargetSESource.clip);
    }



    public async void PlayGameStartSE1()
    {
        await Task.Delay(500);
        _gameStartSE1Source.volume = 0.6f;
        _gameStartSE1Source.time = 0.0f;
        _gameStartSE1Source.pitch = 1.0f;
        _gameStartSE1Source.PlayOneShot(_gameStartSE1Source.clip);
    }

    public async void PlayGameStartSE2()
    {
        await Task.Delay(1500);
        _gameStartSE2Source.volume = 0.7f;
        _gameStartSE2Source.time = 0.0f;
        _gameStartSE2Source.pitch = 1.4f;
        _gameStartSE2Source.PlayOneShot(_gameStartSE2Source.clip);
    }

    public async void PlayGamePatternUpdateSE()
    {
        await Task.Delay(2500);
        _gamePatternUpdateSESource.volume = 1.0f;
        _gamePatternUpdateSESource.time = 0.0f;
        _gamePatternUpdateSESource.pitch = 1.0f;
        _gamePatternUpdateSESource.PlayOneShot(_gamePatternUpdateSESource.clip);
    }
}
