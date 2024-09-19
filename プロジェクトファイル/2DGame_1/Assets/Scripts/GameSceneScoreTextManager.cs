using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameSceneScoreTextManager : MonoBehaviour
{
    [SerializeField] private GameSceneManagerController _gameSceneManagerController;

    [SerializeField] private TextMeshProUGUI _scoreText;

    private const float kActiveTime = 1.0f;
    private const float kInActiveTime = 1.0f;

    private int score = 0;
    private float time = 0.0f;

    private bool isActive = false;
    private bool isInActive = false;

    //public int Score { set{ score = value; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            time += Time.deltaTime;
            Active();
        }

        if (isInActive)
        {
            time += Time.deltaTime;
            InActive();
        }
    }

    public void TextUpdate()
    {
        score = _gameSceneManagerController.Score;
        _scoreText.text = $"{score} pts";
    }

    public void Active()
    {
        if (!isActive)
        {
            isActive = true;
            time = 0.0f;
        }

        if (time >= kActiveTime)
        {
            time = kActiveTime;
            isActive = false;
        }
        float alpha = time / kActiveTime;

        int c = 0;
        _scoreText.color = new Color(c, c, c, alpha);
    }

    public void InActive()
    {
        if (!isInActive)
        {
            isInActive = true;
            time = 0.0f;
        }

        if (time >= kInActiveTime)
        {
            time = kInActiveTime;
            isInActive = false;
        }
        float alpha = 1.0f - time / kInActiveTime;

        int c = 0;
        _scoreText.color = new Color(c, c, c, alpha);
    }
}
