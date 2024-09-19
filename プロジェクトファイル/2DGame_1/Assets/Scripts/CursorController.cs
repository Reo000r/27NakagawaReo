using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;  // async
using static UnityEngine.GraphicsBuffer;
using System.Drawing;

public class CursorController : MonoBehaviour
{
    [SerializeField] private CursorOffest _cursorOffset;
    [SerializeField] private GameObject _cursorEffect;
    [SerializeField] private GameObject _canvas;

    [SerializeField] private Image _cursorImage;

    //private Image _image;

    Vector3 pos;

    private const float kEffectTime = 0.0f;
    private const float kActiveTime = 1.0f;
    private const float kInActiveTime = 1.0f;
    private const int kWaitTime = 100;

    private float time = 0.0f;
    private float fadeTime = 0.0f;
    private float alpha = 0;

    private bool isActive = false;
    private bool isInActive = false;

    // Start is called before the first frame update
    void Start()
    {
        //_image = this.gameObject.GetComponent<Image>();
        pos = this.transform.position;
        //_image.color = new UnityEngine.Color(255, 255, 255, 0);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        pos = Input.mousePosition;

        if (time > kEffectTime && alpha == 1)
        {
            time -= kEffectTime;
            EffectGenerate();
        }

        if (isActive)
        {
            fadeTime += Time.deltaTime;
            Active();
        }

        if (isInActive)
        {
            fadeTime += Time.deltaTime;
            InActive();
        }

        this.transform.position = pos;
    }

    private void EffectGenerate()
    {
        Vector3 v3 = new Vector3(0, 0, 0);
        v3 = pos;
        //Vector2 offset = _cursorOffset.GetScreenOffset();
        //v3 *= offset;

        GameObject _effect = Instantiate(_cursorEffect, v3, Quaternion.identity);
        _effect.transform.SetParent(_canvas.transform, false);
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
        alpha = time / kActiveTime;

        int c = 255;
        _cursorImage.color = new UnityEngine.Color(c, c, c, alpha);
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
        alpha = 1.0f - time / kInActiveTime;

        int c = 255;
        _cursorImage.color = new UnityEngine.Color(c, c, c, alpha);
    }
}
