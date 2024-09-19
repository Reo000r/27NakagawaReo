using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class OptionBackButtonController : MonoBehaviour
{
    [SerializeField] private TitleSceneManager _titleSceneManager;
    [SerializeField] private TitlePerformanceManager _titlePerformanceManager;
    [SerializeField] private TitleSoundManager _titleSoundManager;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private TextMeshProUGUI _buttonText;

    [SerializeField] private Transform _panelTransform;

    [SerializeField] private Vector3 posOffset;

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
    private float scaleX = kScaleX;
    private float scaleY = kScaleY;

    private bool isStartAnim = false;

    private bool canClick = false;
    private bool onMouse = false;
    private bool isClick = false;

    private void Start()
    {
        Vector3 startPos = _panelTransform.position + posOffset;
        this.transform.position = startPos;

        //_buttonText.color = new Color(0, 0, 0, 0);
        this.transform.localScale = new Vector3(1, 1, 1);
    }

    void Update()
    {
        Vector3 v3 = _panelTransform.position + posOffset;
        this.transform.position = v3;

        //if (isStartAnim)
        //{
        //    time += Time.deltaTime;
        //    StartAnim();
        //}
        //else 
        if (isClick)
        {
            time += Time.deltaTime;
            _titlePerformanceManager.OptionOutAnim();
            isClick = false;
            //ClickAnim();
        }
        else if (canClick && !isClick)
        {
            time += Time.deltaTime;
            OnMouseAnim();
        }
        else if (!canClick && scaleY >= kScaleY)
        {
            scaleX -= (kMaxScaleX - kScaleX) / 10;
            scaleY -= (kMaxScaleY - kScaleY) / 10;
            this.transform.localScale = new Vector3(scaleX, scaleY, 1);
        }
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

    public void EndIn()
    {
        canClick = true;
        isClick = false;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && canClick && !isClick)
        {
            // ƒNƒŠƒbƒN‚³‚ê‚½‚ç
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
