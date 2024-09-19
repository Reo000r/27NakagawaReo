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

    // �p�x�A���x
    private Vector2 direction;  // ���K�����ꂽ������set����
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

        // �󂵂��I�ɉ����Ďg�������ڂ�ς���
        // �����Ŏ擾����̂ł͂Ȃ�SetState�Ŏ擾����
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

        // �����ڕύX
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
            // ���Ԍo�߂ō폜
            Destroy(this.gameObject);
        }


        // �󂵂��I�ɉ����ē�����ς���
        switch (spriteType)
        {
            case 0:

                break;

            case 1:

                break;
        }

        Vector2 pos = _rectTransform.position;
        // 1-0�ɂȂ�悤��
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
