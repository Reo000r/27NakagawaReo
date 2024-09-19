using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionButtonController : MonoBehaviour
{
    [SerializeField] private TitleSceneManager _titleSceneManager;
    [SerializeField] private TitlePerformanceManager _titlePerformanceManager;
    [SerializeField] private TitleSoundManager _titleSoundManager;
    [SerializeField] private CursorOffest _cursorOffset;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private TextMeshProUGUI _buttonText;

    //[SerializeField] private string _sceneName;

    private const float kSpeed = 0.5f;
    private const float kSceneChangeSpeed = 0.5f;
    private const float kAddScale = 0.3f;
    private const float kScaleX = 1.0f;
    private const float kScaleY = 1.0f;
    private const float kMaxScaleX = 1.1f;
    private const float kMaxScaleY = 1.1f;
    private const float kClickAnim = 0.5f;

    private float time = 0.0f;
    private float scaleX = 0.0f;
    private float scaleY = 0.0f;

    private bool isStartAnim = false;

    private bool canClick = false;
    private bool onMouse = false;
    private bool isSceneChange = false;
    private bool isClick = false;
    private bool isClickAnimEnd = false;
    private bool isChangeAnim = false;

    void Start()
    {
        _buttonText.color = new Color(0, 0, 0, 0);
        this.transform.localScale = new Vector3(0, 0, 1);
        //this.gameObject.SetActive(false);

        Vector2 screenSize = _cursorOffset.GetDefaultScreenSize();
        _cursorOffset.SetScreenSize(screenSize);
    }

    // Update is called once per frame
    void Update()
    {
        if (isStartAnim)
        {
            time += Time.deltaTime;
            StartAnim();
        }
        else if (!canClick && isClick && !isClickAnimEnd)
        {
            time += Time.deltaTime;
            if (!isSceneChange)
            {
                _titlePerformanceManager.OptionInAnim();
                isSceneChange = true;
            }

            //ClickAnim();
        }
        else if (canClick && !isClick && !isClickAnimEnd)
        {
            time += Time.deltaTime;
            OnMouseAnim();
        }


        if (isChangeAnim)
        {
            time += Time.deltaTime;
            SceneChangeAnim();
        }
    }

    public void StartAnim()
    {
        _buttonText.color = new Color(0, 0, 0, 255);
        //this.gameObject.SetActive(true);
        float endRotaTime = kSpeed;

        if (!isStartAnim)
        {
            canClick = false;
            onMouse = false;
            isSceneChange = false;
            isClick = false;
            isClickAnimEnd = false;
            isChangeAnim = false;
            this.gameObject.SetActive(true);
            _buttonText.color = new Color(0, 0, 0, 0);
            this.transform.localScale = new Vector3(0, 0, 1);
            time = 0;
            isStartAnim = true;
        }

        if (endRotaTime < time)
        {
            //Debug.Log("animEnd");
            isStartAnim = false;
            canClick = true;
            return;
        }

        float f = 1.25f;  // 回転数
        float t = time / kSpeed;
        scaleY = t * kScaleY;
        float sin = Mathf.Sin(2 * Mathf.PI * f * t);
        scaleX = sin * t * kScaleX;  // サイズ調整
        this.transform.localScale = new Vector3(scaleX, scaleY, 1);
    }

    private void OnMouseAnim()
    {
        //Debug.Log("anim");
        if (onMouse && scaleY <= kMaxScaleY)
        {
            scaleX += (kMaxScaleX - kScaleX) / 10;
            scaleY += (kMaxScaleY - kScaleY) / 10;
            this.transform.localScale = new Vector3(scaleX, scaleY, 1);
        }
        else if (!onMouse && scaleY >= kScaleY)
        {
            scaleX -= (kMaxScaleX - kScaleX) / 10;
            scaleY -= (kMaxScaleY - kScaleY) / 10;
            this.transform.localScale = new Vector3(scaleX, scaleY, 1);
        }
    }

    //private void ClickAnim()
    //{
    //    //if (_sceneName == "Ranking")
    //    //{

    //    //    return;
    //    //}

    //    float endAnimTime = kClickAnim;

    //    if (endAnimTime < time && !isClickAnimEnd)
    //    {
    //        isClickAnimEnd = true;
    //        this.gameObject.SetActive(false);
    //        return;
    //    }

    //    scaleX += kAddScale * Time.deltaTime;
    //    scaleY += kAddScale * Time.deltaTime;

    //    float c = time / kClickAnim;
    //    c = 1.0f - c;
    //    _spriteRenderer.color = new Color(255, 255, 255, c);
    //    _buttonText.color = new Color(0, 0, 0, c);
    //    this.transform.localScale = new Vector3(scaleX, scaleY, 1);
    //}

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
            isChangeAnim = false;
            this.gameObject.SetActive(false);
            return;
        }

        float f = 0.75f;  // 回転数
        float t = time / kSceneChangeSpeed;
        t = 1 - t;
        scaleY = t * kScaleY;
        float sin = Mathf.Sin(2 * Mathf.PI * f * t);
        scaleX = sin * t * kScaleX;  // サイズ調整
        this.transform.localScale = new Vector3(scaleX, scaleY, 1);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && canClick && !isClick)
        {
            //if (_sceneName == "Ranking")
            //{
            //    _titleSoundManager.PlayErrorSE();
            //    _titleSceneManager.DataDelete();
            //    return;
            //}

            // クリックされたら
            isClick = true;
            canClick = false;
            time = 0.0f;
        }
    }

    private void OnMouseEnter()
    {
        onMouse = true;
        if (canClick && !isClick)
        {
            _titleSoundManager.PlayButtonSE();
        }
    }

    private void OnMouseExit()
    {
        onMouse = false;
    }
}

