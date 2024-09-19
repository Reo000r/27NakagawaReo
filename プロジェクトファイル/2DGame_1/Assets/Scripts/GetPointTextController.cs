using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetPointTextController : MonoBehaviour
{
    private TextMeshProUGUI _textMeshProUGUI;
    private RectTransform _rectTransform;

    private const string kPlus = "+";

    private const float kPointDecelerationRate = 0.975f;
    private const float kPointAnimTime = 0.5f;
    private const float kPointFirstSpeed = 1.3f;
    private const float kPointChangeAlphaTime = kPointAnimTime * 0.8f;

    private const float kBonusDecelerationRate = 0.975f;
    private const float kBonusAnimTime = 1.0f;
    private const float kBonusFirstSpeed = 1.3f;
    private const float kBonusChangeAlphaTime = kBonusAnimTime * 0.5f;
    private const float kBonusStartBlinkTime = kBonusAnimTime * 0.5f;
    private const float kBonusBlinkInterval = 0.05f;

    private const float kDemeritDecelerationRate = 0.975f;
    private const float kDemeritAnimTime = 0.7f;
    private const float kDemeritFirstSpeed = 1.3f;
    private const float kDemeritChangeAlphaTime = kDemeritAnimTime * 0.5f;
    private const float kDemeritEndShakeTime = kDemeritAnimTime * 0.33f;
    private const float kDemeritShakeWidthX = 20.0f;
    private const int kDemeritShakeNum = 4;

    private int point = 0;
    private float time = 0.0f;
    private float speed = 0.0f;

    private float bonusBlinkTime = kBonusStartBlinkTime;

    private bool isPointAnim = false;
    private bool isBonusAnim = false;
    private bool isDemeritAnim = false;

    private bool isBonusBlink = false;

    private Vector3 offset = new Vector3(0, 60, 0);
    private Vector3 firstPos = Vector3.zero;

    private Color pointColor = new Color(68 / 255f, 81 / 255f, 236 / 255f);
    private Color bonusColor = new Color(246 / 255f, 186 / 255f, 66 / 255f);
    private Color demeritColor = new Color(175 / 255f, 60 / 255f, 229 / 255f);

    // Start is called before the first frame update
    void Start()
    {
        _textMeshProUGUI = this.gameObject.GetComponent<TextMeshProUGUI>();
        _rectTransform = this.gameObject.GetComponent<RectTransform>();

        if (point < 0)
        {
            _textMeshProUGUI.text = point.ToString();
        }
        else
        {
            _textMeshProUGUI.text = kPlus + point.ToString();
        }
        
        _rectTransform.position += offset;
        firstPos = _rectTransform.position;

        Debug.Log(_textMeshProUGUI.color);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPointAnim)
        {
            time += Time.deltaTime;
            PointAnim();
        }

        if (isBonusAnim)
        {
            time += Time.deltaTime;
            BonusAnim();
        }

        if (isDemeritAnim)
        {
            time += Time.deltaTime;
            DemeritAnim();
        }
    }

    public void SetState(int p)
    {
        //Debug.Log("Set");
        point = p;
        
        time = 0.0f;

        if (point < 0)
        {
            // -1~
            isDemeritAnim = true;
            speed = kDemeritFirstSpeed;
        }
        else if (point >= 3)
        {
            // 3~
            isBonusAnim = true;
            speed = kBonusFirstSpeed;
        }
        else
        {
            // 1,2
            isPointAnim = true;
            speed = kPointFirstSpeed;
            
        }
    }

    private void PointAnim()
    {
        //Debug.Log("Anim");
        if (time > kPointAnimTime)
        {
            isPointAnim = false;
            Destroy(this.gameObject);
            return;
        }
        float progress = time / kPointAnimTime;

        // 減速しながら上がっていき一定速度になったら透明度変更
        float decelerationRate = kPointDecelerationRate;  // 減速率
        speed *= decelerationRate;

        float posX = _rectTransform.position.x;
        float posY = _rectTransform.position.y + speed;
        float posZ = _rectTransform.position.z;
        _rectTransform.position = new Vector3(posX, posY, posZ);

        if (time > kPointChangeAlphaTime)
        {
            // 0~1 -> 1~0
            float alphaProgress = (time - kPointChangeAlphaTime) / (kPointAnimTime - (kPointChangeAlphaTime));
            //Debug.Log($"{alphaProgress}");
            alphaProgress = 1.0f - alphaProgress;
            float alpha = alphaProgress;
            _textMeshProUGUI.color = new Color(pointColor.r, pointColor.g, pointColor.b, alpha);
        }
        else { _textMeshProUGUI.color = pointColor; }
    }

    private void BonusAnim()
    {
        if (time > kBonusAnimTime)
        {
            isPointAnim = false;
            Destroy(this.gameObject);
            return;
        }
        float progress = time / kBonusAnimTime;

        // 減速しながら上がっていき一定速度になったら透明度変更
        float decelerationRate = kBonusDecelerationRate;  // 減速率
        speed *= decelerationRate;

        float posX = _rectTransform.position.x;
        float posY = _rectTransform.position.y + speed;
        float posZ = _rectTransform.position.z;
        _rectTransform.position = new Vector3(posX, posY, posZ);

        if (time > bonusBlinkTime)
        {
            bonusBlinkTime += kBonusBlinkInterval;
            if (isBonusBlink) isBonusBlink = false;
            else              isBonusBlink = true;
        }

        if (time > kBonusChangeAlphaTime)
        {
            // 0~1 -> 1~0
            float alphaProgress = (time - kBonusChangeAlphaTime) / (kBonusAnimTime - (kBonusChangeAlphaTime));
            //Debug.Log($"{alphaProgress}");
            alphaProgress = 1.0f - alphaProgress;
            float alpha = alphaProgress;
            if (isBonusBlink) alpha = 0;
            _textMeshProUGUI.color = new Color(bonusColor.r, bonusColor.g, bonusColor.b, alpha);
        }
        else { _textMeshProUGUI.color = bonusColor; }
    }

    private void DemeritAnim()
    {
        if (time > kDemeritAnimTime)
        {
            isPointAnim = false;
            Destroy(this.gameObject);
            return;
        }
        float progress = time / kDemeritAnimTime;

        // 減速しながら上がっていき一定速度になったら透明度変更
        float decelerationRate = kDemeritDecelerationRate;  // 減速率
        speed *= decelerationRate;

        float addX = 0.0f;
        if (time < kDemeritEndShakeTime)
        {
            float shakeProgress = time / kDemeritEndShakeTime;
            shakeProgress *= kDemeritShakeNum; // 左右移動回数
            float sin = Mathf.Sin(2 * Mathf.PI * shakeProgress);
            addX = sin * kDemeritShakeWidthX;
        }

        float posX = firstPos.x + addX;
        float posY = _rectTransform.position.y + speed;
        float posZ = _rectTransform.position.z;
        _rectTransform.position = new Vector3(posX, posY, posZ);

        if (time > kDemeritChangeAlphaTime)
        {
            // 0~1 -> 1~0
            float alphaProgress = (time - kDemeritChangeAlphaTime) / (kDemeritAnimTime - (kDemeritChangeAlphaTime));
            //Debug.Log($"{alphaProgress}");
            alphaProgress = 1.0f - alphaProgress;
            float alpha = alphaProgress;
            _textMeshProUGUI.color = new Color(demeritColor.r, demeritColor.g, demeritColor.b, alpha);
        }
        else { _textMeshProUGUI.color = demeritColor; }
    }
}
