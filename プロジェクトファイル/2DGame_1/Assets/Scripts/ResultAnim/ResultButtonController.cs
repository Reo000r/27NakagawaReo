using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class ResultButtonController : MonoBehaviour
{
    [SerializeField] private ResultSceneManager _resultSceneManager;
    [SerializeField] private ResultSoundManager _resultSoundManager;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private TextMeshProUGUI _buttonText;

    [SerializeField] private string _sceneName;

    private const float kSpeed = 0.5f;
    private const float kSceneChangeSpeed = 0.5f;
    private const float kAddScale = 0.3f;
    private const float kScale = 1.0f;
    private const float kMaxScale = 1.1f;
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
                isSceneChange = true;
                _resultSoundManager.PlaySceneChangeSE();
                _resultSoundManager.IsResultBGMFadeOut = true;
                _resultSceneManager.SceneChange(_sceneName);
            }
            
            ClickAnim();
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
        this.gameObject.SetActive(true);
        float endRotaTime = kSpeed;

        if (!isStartAnim)
        {
            _buttonText.color = new Color(0, 0, 0, 0);
            this.transform.localScale = new Vector3(0, 0, 1);
            time = 0;
            isStartAnim = true;
        }

        if (endRotaTime < time)
        {
            isStartAnim = false;
            canClick = true;
            return;
        }

        float f = 1.25f;  // 回転数
        float t = time / kSpeed;
        scaleY = t * kScale;
        float sin = Mathf.Sin(2 * Mathf.PI * f * t);
        scaleX = sin * t * kScale;  // サイズ調整
        this.transform.localScale = new Vector3(scaleX, scaleY, 1);
    }

    private void OnMouseAnim()
    {
        if (onMouse && scaleX <= kMaxScale)
        {
            scaleX += (kMaxScale - kScale) / 10;
            scaleY += (kMaxScale - kScale) / 10;
            this.transform.localScale = new Vector3(scaleX, scaleY, 1);
        }
        else if (!onMouse && scaleX >= kScale)
        {
            scaleX -= (kMaxScale - kScale) / 10;
            scaleY -= (kMaxScale - kScale) / 10;
            this.transform.localScale = new Vector3(scaleX, scaleY, 1);
        }
    }

    private void ClickAnim()
    {
        float endAnimTime = kClickAnim;

        if (endAnimTime < time && !isClickAnimEnd)
        {
            isClickAnimEnd = true;
            this.gameObject.SetActive(false);
            return;
        }

        scaleX += kAddScale * Time.deltaTime;
        scaleY += kAddScale * Time.deltaTime;

        float c = time / kClickAnim;
        c = 1.0f - c;
        _spriteRenderer.color = new Color(255, 255, 255, c);
        _buttonText.color = new Color(0, 0, 0, c);
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

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && canClick && !isClick)
        {
            // クリックされたら
            isClick = true;
            canClick = false;
            time = 0.0f;
        }
    }

    private void OnMouseEnter()
    {
        onMouse = true;
        if (canClick && !isClick) _resultSoundManager.PlayButtonSE();
    }

    private void OnMouseExit()
    {
        onMouse = false;
    }
}
