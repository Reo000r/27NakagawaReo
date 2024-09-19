using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorEffectController : MonoBehaviour
{
    private Image _spriteImage;

    private float time = 0.0f;
    private const float kLifeTime = 0.12f;
    private const float kMaxAlpha = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        _spriteImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (time < kLifeTime)
        {
            time += Time.deltaTime;
            EffectUpdate();
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    private void EffectUpdate()
    {
        int color = 255;
        float t = (time / kLifeTime);
        t = 1.0f - t;
        float alpha = t * kMaxAlpha;
        _spriteImage.color = new Color(color, color, color, alpha);
    }
}
