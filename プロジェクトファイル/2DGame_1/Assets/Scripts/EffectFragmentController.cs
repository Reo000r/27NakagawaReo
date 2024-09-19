using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class EffectFragmentController : MonoBehaviour
{
    [SerializeField] private Sprite _effectSprite0;
    [SerializeField] private Sprite _effectSprite1;

    RectTransform _rectTransform;

    private Sprite _useEffectSprite;
    private Image _spriteImage;

    private EffectGeneratorController _effectGenCont;

    //private Sprite _sprite;

    // 角度、速度
    private Vector2 direction;  // 正規化された方向をsetする
    private float speed;
    private float lifeTime;

    private float time;
    private int spriteType;


    // Start is called before the first frame update
    void Start()
    {
        _effectGenCont = GameObject.Find("EffectGenerator").GetComponent<EffectGeneratorController>();
        _spriteImage = gameObject.GetComponent<Image>();
        _rectTransform = this.gameObject.GetComponent<RectTransform>();

        speed = _effectGenCont.KEffectSpeed;
        lifeTime = _effectGenCont.KEffectLifeTime;

        // 壊した的に応じて使う見た目を変える
        // ここで取得するのではなくSetStateで取得した
        //spriteType = _effectGenCont.GenerateSpriteType;
        switch (spriteType)
        {
            case 0:
                _useEffectSprite = _effectSprite0;
                break;

            case 1:
                _useEffectSprite = _effectSprite1;
                break;
        }

        // 見た目変更
        _spriteImage.sprite = _useEffectSprite;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        EffectUpdate();
    }

    private void EffectUpdate()
    {
        time += Time.deltaTime;

        if (time > lifeTime)
        {
            // 時間経過で削除
            Destroy(this.gameObject);
        }


        // 壊した的に応じて動きを変える
        switch (spriteType)
        {
            case 0:

                break;

            case 1:

                break;
        }

        Vector2 pos = _rectTransform.position;
        // 1-0になるように
        float t = time / lifeTime;
        t = 1.0f - t;
        pos += direction * speed * t;
        _rectTransform.position = pos;
    }

    public void SetState(Vector2 iDir, int iSpriteType)
    {
        /*
        Debug.LogWarning($"EffectFragmentController.SetState: changed! " +
            $"{direction} -> {iDir}," +
            $"{spriteType} -> {iSpriteType}," +
            $"");
        */
        direction = iDir;
        spriteType = iSpriteType;
    }
}
