using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TargetController : MonoBehaviour
{
    // mode切替用
    enum Mode
    {
        Spawn,
        Stay,
        Return,
        ModeNum
    }

    Mode mode = Mode.Spawn;

    // PrefabにアタッチしてるからEditorから入らない？
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
    private const float kClickAnimMaxScaleMagnification = 1.4f;  // 拡大倍率
    private const float kClickAnimTime = 0.2f;

    private float speed = 0.0f;
    private float staytime = 0.0f;
    private int type = 0;  // スコア処理とかで使うかも

    // アニメーション時間管理
    // startRotaTimeいらないかも
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

        // 時間を進める
        modeTime += Time.deltaTime;


        // modeに応じてアニメーション分岐
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

    // ChangeをAdd/Forward/Advance等に変えた方がいいかも
    private void ChangeMode()
    {
        // modeを1進めてModeの要素数より多ければ最初のModeにする
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

    // アニメーションまとめたい
    // RotaAnim内のswitchで処理してもいいかも
    private void SpawnModeUpdate()
    {
        float endRotaTime = speed + startModeTime;

        if (endRotaTime < startModeTime + modeTime)
        {
            // kAnimTime経ったら
            //Debug.Log("SpawnModeUpdate End");
            float scale = this.transform.localScale.y;
            this.transform.localScale = new Vector3(scale, scale, this.transform.localScale.z);
            ChangeMode();
            return;
        }

        // 旧処理
        //float f = 1.0f / kRotaSpeed;
        //// Mathf.Clamp01 を使いfloatの値を0~1に丸めている
        //// Time.deltaTime / kAnimTime でdeltaTimeあたりのscaleの減り方を求めている
        //scale += Mathf.Clamp01(Time.deltaTime / kAnimTime);
        //scale *= kMaxScale;
        //// 2 * Mathf.PI で一周する長さを求めて時間を掛けると扱いやすくなる
        //float sin = Mathf.Sin(2 * Mathf.PI * f * modeTime);
        //// サイズ調整
        //sin *= scale;
        ////Debug.Log($"sin/size : {sin}/{scale}({modeTime})");
        ////Debug.Log($"sin = Mathf.Sin(2 * Mathf.PI * f * modeTime) : {sin} = {Mathf.Sin(2 * Mathf.PI * f * modeTime)}(2 * 3.14 * {f} * {modeTime})");
        //this.transform.localScale = new Vector3(sin, scale, 1);


        float f = 1.25f;  // 回転数
        // Mathf.Clamp01 を使いfloatの値を0~1に丸めている
        // Time.deltaTime / kAnimTime でdeltaTimeあたりのscaleの減り方を求めている
        float t = modeTime / speed; // 0-1に変化
        scaleY = t * kMaxScale;
        // 2 * Mathf.PI で一周する長さを求めて時間を掛けると扱いやすくなる
        float sin = Mathf.Sin(2 * Mathf.PI * f * t);
        // サイズ調整
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
            // kAnimTime経ったら
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
            // kAnimTime経ったら
            //Debug.Log("ReturnModeUpdate End");
            Destroy(this.gameObject);

            ChangeMode();
            return;
        }

        // 旧処理
        //float f = 1.0f / kRotaSpeed;
        //// Mathf.Clamp01 を使いfloatの値を0~1に丸めている
        //// Time.deltaTime / kAnimTime でdeltaTimeあたりのscaleの減り方を求めている
        //scale -= Mathf.Clamp01(Time.deltaTime / kAnimTime);
        //// 2 * Mathf.PI で一周する長さを求めて時間を掛けると扱いやすくなる
        //float sin = Mathf.Sin(2 * Mathf.PI * f * modeTime);
        //// サイズ調整
        //sin *= scale;
        ////Debug.Log($"sin/size : {sin}/{scale}({modeTime})");
        //this.transform.localScale = new Vector3(sin, scale, 1);

        float f = 1.25f;  // 回転数
        // Mathf.Clamp01 を使いfloatの値を0~1に丸めている
        // Time.deltaTime / kAnimTime でdeltaTimeあたりのscaleの減り方を求めている
        float t = modeTime / speed; // 0-1に変化
        t = 1 - t;
        scaleY = t * kMaxScale;
        // 2 * Mathf.PI で一周する長さを求めて時間を掛けると扱いやすくなる
        float sin = Mathf.Sin(2 * Mathf.PI * f * t);
        // サイズ調整
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
                // スコア加算
                addScore = 1;
                _gameSceneManagerController.AddScore(addScore);
                _getPointTextGenerator.GenerateText(pos, addScore);
                TargetClickAnim();
                _gameSceneSoundManager.PlayTargetHitSE();
                break;

            case 1:
                // スコア加算
                addScore = 5;
                _gameSceneManagerController.AddScore(addScore);
                _getPointTextGenerator.GenerateText(pos, addScore);
                TargetClickAnim();
                _gameSceneSoundManager.PlayBonusTargetHitSE();
                break;

            case 2:
                // 減点
                addScore = -3;
                _gameSceneManagerController.AddScore(addScore);
                _getPointTextGenerator.GenerateText(pos, addScore);
                TargetClickAnim();
                _gameSceneSoundManager.PlayDemeritTargetHitSE();
                break;
        }
        
        // (破壊エフェクトを出す)
        _effectGeneratorController.EffectGenerate(type);

        // 削除
        //Destroy(this.gameObject);
    }


    private void TargetClickAnim()
    {
        // scale変える
        // 透明度変える
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
            // 削除
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
            // クリックされたら
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
