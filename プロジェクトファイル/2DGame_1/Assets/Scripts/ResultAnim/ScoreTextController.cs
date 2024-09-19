using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class ScoreTextController : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private ResultSceneManager _resultSceneManager;
    [SerializeField] private ResultSoundManager _resultSoundManager;

    private const float kMaxScale = 6.0f;
    private float kSceneChangeSpeed = 0.3f;

    [SerializeField] private int scoreTextNum;

    private bool isAnimStart = false;
    private bool isAnimEnd = false;
    private bool isChangeAnim = false;
    private string text;
    private int dispScore = -1;
    private float speed = 0.5f;
    private float time = 0.0f;
    private float scaleX = 0.0f;
    private float scaleY = kMaxScale;

    public int DispScore { set{ dispScore = value; } }

    private Vector3 offset = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isAnimStart && !isAnimEnd)
        {
            time += Time.deltaTime;
            NumberAnim(speed);
        }

        if (isChangeAnim)
        {
            time += Time.deltaTime;
            SceneChangeAnim();
        }
    }

    public void NumberAnim(float s)
    {
        if (!isAnimStart)
        {
            this.gameObject.SetActive(true);
            isAnimStart = true;
            time = 0.0f;
            speed = s / 3;

            if(dispScore == -1)
            {
                Debug.Log("DispScore Error");
                dispScore = 0;
            }
            text = dispScore.ToString();

            _scoreText.text = text;
            //_scoreText.transform.position = Camera.main.WorldToScreenPoint(_targetTransform.position + offset);
        }

        float endRotaTime = speed;
        if (endRotaTime < time && !isAnimEnd)
        {
            isAnimEnd = true;
            _resultSoundManager.PlayAnimStopSE();
            _resultSceneManager.ScoreTextAnimComplete();
            return;
        }

        float f = 0.25f;  // 回転数
        // Mathf.Clamp01 を使いfloatの値を0~1に丸めている
        // Time.deltaTime / kAnimTime でdeltaTimeあたりのscaleの減り方を求めている
        float t = time / speed; // 0-1に変化
        scaleY = kMaxScale;
        // 2 * Mathf.PI で一周する長さを求めて時間を掛けると扱いやすくなる
        float sin = Mathf.Sin(2 * Mathf.PI * f * t);
        // サイズ調整
        scaleX = sin * kMaxScale;
        //Debug.Log($"sin/size : {sin}/{scale}({modeTime})");
        //Debug.Log($"sin = Mathf.Sin(2 * Mathf.PI * f * modeTime) : {sin} = {Mathf.Sin(2 * Mathf.PI * f * modeTime)}(2 * 3.14 * {f} * {modeTime})");
        this.transform.localScale = new Vector3(scaleX, scaleY, 1);
    }

    public void SceneChangeAnim()
    {
        float endRotaTime = kSceneChangeSpeed;

        if (!isChangeAnim)
        {
            isChangeAnim = true;
            time = 0;
        }

        if (endRotaTime < time)
        {
            this.gameObject.SetActive(false);
            return;
        }

        float f = 0.75f;  // 回転数
        float t = time / kSceneChangeSpeed;
        t = 1 - t;
        scaleY = t * kMaxScale;
        float sin = Mathf.Sin(2 * Mathf.PI * f * t);
        scaleX = sin * t * kMaxScale;  // サイズ調整
        this.transform.localScale = new Vector3(scaleX, scaleY, 1);
    }
}
