using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ResultTargetController : MonoBehaviour
{
    [SerializeField] private ScoreTextController _scoreTextController;
    [SerializeField] private ResultSoundManager _resultSoundManager;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _sprite;

    private const float kMaxScale = 1.0f;
    private const float kSpeed = 0.3f;
    private const float kClickedSpeed = 1.0f;
    private const float kClickedTime = 1.0f;

    private float time = 0.0f;
    private float scaleX = 0.0f;
    private float scaleY = 0.0f;

    //private bool canClick = false;
    //private bool isClick = false;
    //private bool isClickAnimEnd = false;

    private bool isStartAnim = false;
    private bool isChangeAnim = false;
    private bool isScoreUpdate = false;

    // Start is called before the first frame update
    void Start()
    {
       //this.gameObject.SetActive(false);
        this.transform.localScale = Vector3.zero;
        time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStartAnim)
        {
            time += Time.deltaTime;
            StartAnim();
        }

        if (isChangeAnim)
        {
            time += Time.deltaTime;
            ChangeAnim();
        }

        //time += Time.deltaTime;

        //if (!canClick && !isClick)
        //{
        //    //StartAnim();
        //}
        //else if (time >= kClickedTime && canClick)
        //{
        //    // クリックされた時の処理のまま
        //    isClick = true;
        //    canClick = false;
        //    time = 0.0f;
        //}
        //else if (!canClick && isClick && !isClickAnimEnd)
        //{
        //    //ClickAnim();
        //}
    }

    public void ChangeSprite()
    {
        isScoreUpdate = true;
        _spriteRenderer.sprite = _sprite;
    }

    public void StartAnim()
    {
        float endRotaTime = kSpeed;

        if (!isStartAnim)
        {
            _resultSoundManager.PlayTargetSummonSE();
            this.transform.localScale = Vector3.zero;
            time = 0.0f;
            isStartAnim = true;
        }

        if (endRotaTime < time)
        {
            isStartAnim = false;
            return;
        }

        float f = 1.25f;  // 回転数
        float t = time / kSpeed;
        scaleY = t * kMaxScale;
        float sin = Mathf.Sin(2 * Mathf.PI * f * t);
        scaleX = sin * t * kMaxScale;  // サイズ調整
        this.transform.localScale = new Vector3(scaleX, scaleY, 1);
    }

    public void ChangeAnim()
    {
        float endRotaTime = kClickedSpeed;

        if (!isChangeAnim)
        {
            if (isScoreUpdate) _resultSoundManager.PlayBonusTargetSE();
            else               _resultSoundManager.PlayTargetHitSE();

            time = 0.0f;
            isChangeAnim = true;
        }

        if (endRotaTime < time)
        {
            isChangeAnim = false;
            _scoreTextController.NumberAnim(kClickedSpeed);
            this.gameObject.SetActive(false);
            return;
        }

        float f = 1.75f;  // 回転数
        float t = time / kClickedSpeed;
        t = 1 - t;
        scaleY = kMaxScale;
        float sin = Mathf.Sin(2 * Mathf.PI * f * t);
        scaleX = sin * kMaxScale;  // サイズ調整
        this.transform.localScale = new Vector3(scaleX, scaleY, 1);
    }

    //private void OnMouseOver()
    //{
    //    if (Input.GetMouseButtonDown(0) && canClick && !isClick)
    //    {
    //        //// クリックされたら
    //        //isClick = true;
    //        //canClick = false;
    //        //time = 0.0f;
    //    }
    //}
}
