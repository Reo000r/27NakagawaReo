using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class TitleTextController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _titleText;

    private const float kMaxScale = 4.0f;
    private const float kSpeed = 0.6f;
    private const float kClickedSpeed = 0.5f;
    private const float kClickedTime = 1.0f;

    private float time = 0.0f;
    private float scaleX = 0.0f;
    private float scaleY = 0.0f;

    private bool isStartAnim = false;
    private bool isChangeAnim = false;

    // Start is called before the first frame update
    void Start()
    {
        
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
            SceneChangeAnim();
        }
    }

    public void StartAnim()
    {
        float endRotaTime = kSpeed;

        if (!isStartAnim)
        {
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

        float alpha = t;
        int color = 0;
        _titleText.color = new Color(color, color, color, alpha);
    }

    public void SceneChangeAnim()
    {
        float endRotaTime = kSpeed;

        if (!isChangeAnim)
        {
            this.transform.localScale = Vector3.zero;
            time = 0.0f;
            isChangeAnim = true;
        }

        if (endRotaTime < time)
        {
            isChangeAnim = false;
            return;
        }

        float f = 1.25f;  // 回転数
        float t = time / kSpeed;
        t = 1.0f - t;
        scaleY = t * kMaxScale;
        float sin = Mathf.Sin(2 * Mathf.PI * f * t);
        scaleX = sin * t * kMaxScale;  // サイズ調整
        this.transform.localScale = new Vector3(scaleX, scaleY, 1);

        float alpha = t;
        int color = 0;
        _titleText.color = new Color(color, color, color, alpha);
    }
}
