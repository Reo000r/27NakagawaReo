using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TargetController : MonoBehaviour
{
    // mode�ؑ֗p
    enum Mode
    {
        Spawn,
        Stay,
        Return,
        ModeNum
    }

    Mode mode = Mode.Spawn;

    // Prefab�ɃA�^�b�`���Ă邩��Editor�������Ȃ��H
    [SerializeField] private EffectGeneratorController _effectGeneratorController;
    [SerializeField] private GameSceneManagerController _gameSceneManagerController;
    [SerializeField] private GameSceneSoundManager _gameSceneSoundManager;
    [SerializeField] private GetPointTextGenerator _getPointTextGenerator;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    //private Vector3 startV3 = new Vector3(0, 100, 0);
    //private Vector3 summonPosV3 = new Vector3(-100, -100, 0);

    private const float kStayTime = 3.0f;
    private const float kAnimTime = 0.3f;
    private const float kRotaSpeed = 0.4f;
    private const float kMaxScale = 0.8f;
    private const float kMinScale = 0.0f;
    private const float kClickAnimMaxScaleMagnification = 1.4f;  // �g��{��
    private const float kClickAnimTime = 0.2f;

    private float speed = 0.0f;
    private float staytime = 0.0f;
    private int type = 0;  // �X�R�A�����Ƃ��Ŏg������

    // �A�j���[�V�������ԊǗ�
    // startRotaTime����Ȃ�����
    private float startModeTime = 0;
    private float modeTime = 0;
    private float scaleX = 0.0f;
    private float scaleY = 0.0f;

    private float time = 0.0f;
    private Vector3 animStartScale = Vector3.zero;

    private bool isClickAnim = false;
    private bool isClick = false;

    // Start is called before the first frame update
    void Start()
    {
        _effectGeneratorController = GameObject.Find("EffectGenerator").GetComponent<EffectGeneratorController>();
        _gameSceneManagerController = GameObject.Find("GameSceneManager").GetComponent<GameSceneManagerController>();
        _gameSceneSoundManager = GameObject.Find("SoundManager").GetComponent<GameSceneSoundManager>();
        _getPointTextGenerator = GameObject.Find("GetPointTextGenerator").GetComponent<GetPointTextGenerator>();
        _spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();


        _gameSceneSoundManager.PlayTargetSummonSE();
        //this.transform.position = startV3;
    }

    // Update is called once per frame
    void Update()
    {
        //if (_gameSceneManagerController.IsStateTarget)
        if (isClickAnim)
        {
            time += Time.deltaTime;
            TargetClickAnim();
        }
        else
        {
            RotaAnim();
        }
    }

    private void RotaAnim()
    {
        //if (this.transform.position == startV3) this.transform.position = summonPosV3;

        // ���Ԃ�i�߂�
        modeTime += Time.deltaTime;


        // mode�ɉ����ăA�j���[�V��������
        switch (mode)
        {
            case Mode.Spawn:
                SpawnModeUpdate();
                break;

            case Mode.Stay:
                StayModeUpdate();
                break;

            case Mode.Return:
                ReturnModeUpdate();
                break;

            //Default:
            //    break;
        }
    }

    // Change��Add/Forward/Advance���ɕς���������������
    private void ChangeMode()
    {
        // mode��1�i�߂�Mode�̗v�f����葽����΍ŏ���Mode�ɂ���
        mode++;

        switch (mode)
        {
            case Mode.Spawn:
                //scale = kMinScale;
                break;

            case Mode.Stay:
                //scale = kMaxScale;
                break;

            case Mode.Return:
                //scale = kMaxScale;
                break;

            case Mode.ModeNum:
                mode = Mode.Spawn;
                break;
        }

        //Debug.Log($"mode : {mode}");

        startModeTime = Time.time;
        modeTime = 0;
    }

    // �A�j���[�V�����܂Ƃ߂���
    // RotaAnim����switch�ŏ������Ă���������
    private void SpawnModeUpdate()
    {
        float endRotaTime = speed + startModeTime;

        if (endRotaTime < startModeTime + modeTime)
        {
            // kAnimTime�o������
            //Debug.Log("SpawnModeUpdate End");
            float scale = this.transform.localScale.y;
            this.transform.localScale = new Vector3(scale, scale, this.transform.localScale.z);
            ChangeMode();
            return;
        }

        // ������
        //float f = 1.0f / kRotaSpeed;
        //// Mathf.Clamp01 ���g��float�̒l��0~1�Ɋۂ߂Ă���
        //// Time.deltaTime / kAnimTime ��deltaTime�������scale�̌���������߂Ă���
        //scale += Mathf.Clamp01(Time.deltaTime / kAnimTime);
        //scale *= kMaxScale;
        //// 2 * Mathf.PI �ň�����钷�������߂Ď��Ԃ��|����ƈ����₷���Ȃ�
        //float sin = Mathf.Sin(2 * Mathf.PI * f * modeTime);
        //// �T�C�Y����
        //sin *= scale;
        ////Debug.Log($"sin/size : {sin}/{scale}({modeTime})");
        ////Debug.Log($"sin = Mathf.Sin(2 * Mathf.PI * f * modeTime) : {sin} = {Mathf.Sin(2 * Mathf.PI * f * modeTime)}(2 * 3.14 * {f} * {modeTime})");
        //this.transform.localScale = new Vector3(sin, scale, 1);


        float f = 1.25f;  // ��]��
        // Mathf.Clamp01 ���g��float�̒l��0~1�Ɋۂ߂Ă���
        // Time.deltaTime / kAnimTime ��deltaTime�������scale�̌���������߂Ă���
        float t = modeTime / speed; // 0-1�ɕω�
        scaleY = t * kMaxScale;
        // 2 * Mathf.PI �ň�����钷�������߂Ď��Ԃ��|����ƈ����₷���Ȃ�
        float sin = Mathf.Sin(2 * Mathf.PI * f * t);
        // �T�C�Y����
        scaleX = sin * t * kMaxScale;
        //Debug.Log($"sin/size : {sin}/{scale}({modeTime})");
        //Debug.Log($"sin = Mathf.Sin(2 * Mathf.PI * f * modeTime) : {sin} = {Mathf.Sin(2 * Mathf.PI * f * modeTime)}(2 * 3.14 * {f} * {modeTime})");
        this.transform.localScale = new Vector3(scaleX, scaleY, 1);

        //Debug.Log($"{modeTime / speed} , {sin}");
    }

    private void StayModeUpdate()
    {
        float endUpdateTime = staytime + startModeTime;

        if (endUpdateTime < startModeTime + modeTime)
        {
            // kAnimTime�o������
            //Debug.Log("StayModeUpdate End");
            ChangeMode();
            return;
        }
    }

    private void ReturnModeUpdate()
    {
        float endRotaTime = speed + startModeTime;

        if (endRotaTime < startModeTime + modeTime)
        {
            // kAnimTime�o������
            //Debug.Log("ReturnModeUpdate End");
            Destroy(this.gameObject);

            ChangeMode();
            return;
        }

        // ������
        //float f = 1.0f / kRotaSpeed;
        //// Mathf.Clamp01 ���g��float�̒l��0~1�Ɋۂ߂Ă���
        //// Time.deltaTime / kAnimTime ��deltaTime�������scale�̌���������߂Ă���
        //scale -= Mathf.Clamp01(Time.deltaTime / kAnimTime);
        //// 2 * Mathf.PI �ň�����钷�������߂Ď��Ԃ��|����ƈ����₷���Ȃ�
        //float sin = Mathf.Sin(2 * Mathf.PI * f * modeTime);
        //// �T�C�Y����
        //sin *= scale;
        ////Debug.Log($"sin/size : {sin}/{scale}({modeTime})");
        //this.transform.localScale = new Vector3(sin, scale, 1);

        float f = 1.25f;  // ��]��
        // Mathf.Clamp01 ���g��float�̒l��0~1�Ɋۂ߂Ă���
        // Time.deltaTime / kAnimTime ��deltaTime�������scale�̌���������߂Ă���
        float t = modeTime / speed; // 0-1�ɕω�
        t = 1 - t;
        scaleY = t * kMaxScale;
        // 2 * Mathf.PI �ň�����钷�������߂Ď��Ԃ��|����ƈ����₷���Ȃ�
        float sin = Mathf.Sin(2 * Mathf.PI * f * t);
        // �T�C�Y����
        scaleX = sin * t * kMaxScale;
        //Debug.Log($"sin/size : {sin}/{scale}({modeTime})");
        //Debug.Log($"sin = Mathf.Sin(2 * Mathf.PI * f * modeTime) : {sin} = {Mathf.Sin(2 * Mathf.PI * f * modeTime)}(2 * 3.14 * {f} * {modeTime})");
        this.transform.localScale = new Vector3(scaleX, scaleY, 1);

        //Debug.Log($"{modeTime / speed} , {sin}");
    }

    private void TargetClicked()
    {
        if (isClick) return;

        isClick = true;

        int addScore = 0;
        Vector3 pos = this.transform.position;
        switch (type)
        {
            case 0:
                // �X�R�A���Z
                addScore = 1;
                _gameSceneManagerController.AddScore(addScore);
                _getPointTextGenerator.GenerateText(pos, addScore);
                TargetClickAnim();
                _gameSceneSoundManager.PlayTargetHitSE();
                break;

            case 1:
                // �X�R�A���Z
                addScore = 5;
                _gameSceneManagerController.AddScore(addScore);
                _getPointTextGenerator.GenerateText(pos, addScore);
                TargetClickAnim();
                _gameSceneSoundManager.PlayBonusTargetHitSE();
                break;

            case 2:
                // ���_
                addScore = -3;
                _gameSceneManagerController.AddScore(addScore);
                _getPointTextGenerator.GenerateText(pos, addScore);
                TargetClickAnim();
                _gameSceneSoundManager.PlayDemeritTargetHitSE();
                break;
        }
        
        // (�j��G�t�F�N�g���o��)
        _effectGeneratorController.EffectGenerate(type);

        // �폜
        //Destroy(this.gameObject);
    }


    private void TargetClickAnim()
    {
        // scale�ς���
        // �����x�ς���
        //kClickAnimMaxScaleMagnification

        if (!isClickAnim)
        {
            animStartScale = this.transform.localScale;
            time = 0;
            isClickAnim = true;
        }

        if (kClickAnimTime < time)
        {
            isClickAnim = false;
            // �폜
            Destroy(this.gameObject);
            return;
        }

        float t = time / kClickAnimTime;
        Vector3 animAddScale = animStartScale * (kClickAnimMaxScaleMagnification - 1.0f);
        Vector3 scale = animStartScale + animAddScale * t;
        this.transform.localScale = scale;

        float alpha = 1.0f - t;
        int c = 255;
        _spriteRenderer.color = new Color(c, c, c, alpha);
    }


    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // �N���b�N���ꂽ��
            TargetClicked();
        }
    }

    public void SetState(/*Vector3 iV3, */float iSpeed, float iStaytime, int iType)
    {
        /*
        Debug.LogWarning($"TargetController.SetState: changed! " +
            //$"{summonPosV3} -> {iV3}," +
            $"{speed} -> {iSpeed}," +
            $"{staytime} -> {iStaytime}," +
            $"{type} -> {iType}," +
            $"");
        */
        //summonPosV3 = iV3;
        speed = iSpeed;
        staytime = iStaytime;
        type = iType;
    }
}
