using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using static UnityEngine.GraphicsBuffer;

public class TargetGeneratorController : MonoBehaviour
{
    [SerializeField] private GameObject _target0;
    [SerializeField] private GameObject _target1;
    [SerializeField] private GameObject _target2;

    //private TargetController _target0Controller;
    //private TargetController _target1Controller;

    Transform _transform;
    [SerializeField] private TargetJsonLoader _jLoader;
    [SerializeField] private GameSceneManagerController _gameSceneManagerController;
    [SerializeField] private GameSceneSoundManager _gameSceneSoundManager;

    // [SerializeField]��private�ł�inspecter
    [SerializeField] private const float kSummonTime = 2.5f;
    private const float kMaxX = 7.0f;
    private const float kMinX = -7.0f;
    private const float kMaxY = 3.0f;
    private const float kMinY = -3.0f;
    private const float kRangeX = kMaxX + (-kMinX);
    private const float kRangeY = kMaxY + (-kMinY);

    private const int xPattern = 6;
    private const int yPattern = 3;
    private const float kAddPX = kRangeX / (xPattern - 1);
    private const float kAddPY = kRangeY / (yPattern - 1);
    
    public float timer = 0.0f;

    private int patternNum = 0;
    private int targetNum = 0;
    private float posX = 0.0f;
    private float posY = 0.0f;
    private float timing = 0.0f;
    private float speed = 0.0f;
    private float staytime = 0.0f;
    private int type = 0;

    private int patternTotal;

    void Start()
    {
        //_target0Controller = _target0.GetComponent<TargetController>();
        //_target1Controller = _target1.GetComponent<TargetController>();
        _transform = this.gameObject.GetComponent<Transform>();
        //_jLoader = GetComponent<TargetJsonLoader>();

        this.transform.position = new Vector3(kMinX, kMinY, 0);

        TargetInit();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (_gameSceneManagerController.IsStateTargetStart)
        if (_gameSceneManagerController.IsStateTarget)
        {
            TargetGeneratorUpdate();
        }
    }

    private void TargetGeneratorUpdate()
    {
        timer += Time.deltaTime;

        if (timer > timing)
        {
            timer -= timing;

            //TargetGenerate();
            TargetGenerate();
        }

        
    }

    // private void TargetGenerateOld()
    
    private void TargetGenerate()
    {


        // pattern��target(����pattern)�̑��������
        patternTotal = _jLoader.GetPatternTotalNum(_jLoader.PatternDicData);
        int targetTotal = _jLoader.GetTargetTotalNum(_jLoader.PatternDicData[patternNum]);

        // �����dic�ɕۑ�����Ă���List�̗v�f������
        //for (int i = 0; i < patternNum; i++)
        //{
        //    // GetDic���v�f���������Ă���̂�-1
        //    for (int j = 0; j < _jLoader.GetTargetTotalNum(_jLoader.PatternDicData[i]); j++)
        //    {
        //        // i��Dic��patternNum�ԖځAj��List��TargetNum�Ԗ�
        //        //Debug.Log($"dic{i},{j}({_jLoader.GetTargetNum(_jLoader.PatternDicData[i])}) : " +
        //        //    $"{_jLoader.PatternDicData[i][_jLoader.KX + j]}," +
        //        //    $"{_jLoader.PatternDicData[i][_jLoader.KY + j]}," +
        //        //    $"{_jLoader.PatternDicData[i][_jLoader.KTiming + j]}," +
        //        //    $"{_jLoader.PatternDicData[i][_jLoader.KSpeed + j]}," +
        //        //    $"{_jLoader.PatternDicData[i][_jLoader.KStaytime + j]}," +
        //        //    $"{_jLoader.PatternDicData[i][_jLoader.KType + j]}");
        //    }
        //}

        //Debug.Log($"1 pattern num:{patternNum}, total{patternTotal} / " +
        //    $"target num:{targetNum}, total{targetTotal}");
        
        Vector3 v3 = new Vector3(posX, posY, 0);
        GameObject _target = null;
        TargetController _tClass = null;

        // ����
        // �I�̎�ޖ��ɏo��GameObject��ς��Ă���
        switch (type)
        {
            case 0:
                _target = Instantiate(_target0, v3, Quaternion.identity);
                _tClass = _target.GetComponent<TargetController>();
                //_tClass = _target0Controller;
                _tClass.SetState(speed, staytime, type);
                break;

            case 1:
                _target = Instantiate(_target1, v3, Quaternion.identity);
                _tClass = _target.GetComponent<TargetController>();
                //_tClass = _target1Controller;
                _tClass.SetState(speed, staytime, type);
                break;

            case 2:
                _target = Instantiate(_target2, v3, Quaternion.identity);
                _tClass = _target.GetComponent<TargetController>();
                //_tClass = _target1Controller;
                _tClass.SetState(speed, staytime, type);
                break;

        }

        

        // �I���X�V
        //TargetNumUpdate();


        if (patternNum < patternTotal &&
            targetNum < targetTotal-1)
        {
            // pattern��targett�������𒴂��Ă��Ȃ����
            // �ʏ폈��
            TargetNumUpdate();
        }
        else if(targetNum >= targetTotal-1)
        {
            //Debug.Log($"D");
            // target�̐��������𒴂��Ă����ꍇ��
            // pattern��i�߂�
            PatternNumUpdate();
        }

        //Debug.Log($"2 pattern num:{patternNum}, total{patternTotal} / " +
        //    $"target num:{targetNum}, total{targetTotal}");
    }
    

    public void TargetInit()
    {
        patternNum = 0;
        targetNum = 0;
        timing = _jLoader.PatternDicData[patternNum][_jLoader.KTiming + targetNum];
        TargetStateUpdate();
    }

    public void TargetNumUpdate()
    {
        //patternNum = patternNum;
        targetNum++;
        timing = _jLoader.PatternDicData[patternNum][_jLoader.KTiming + targetNum];
        TargetStateUpdate();
    }

    public void PatternNumUpdate()
    {
        patternNum++;
        targetNum = 0;

        if (patternNum >= patternTotal)
        {
            Debug.Log($"d");
            // pattern�̐��������𒴂��Ă����ꍇ��
            // GamemodeState��ς���
            _gameSceneManagerController.GamemodeStateUpdate();
            // ������ւ񂨂����ȋ���������(Update��������Ă΂�铙)
            return;
        }

        timing = _jLoader.PatternDicData[patternNum][_jLoader.KTiming + targetNum];

        _gameSceneSoundManager.PlayGamePatternUpdateSE();

        TargetStateUpdate();
    }

    private void TargetStateUpdate()
    {
        posX = _jLoader.PatternDicData[patternNum][_jLoader.KX + targetNum];
        posY = _jLoader.PatternDicData[patternNum][_jLoader.KY + targetNum];
        speed = _jLoader.PatternDicData[patternNum][_jLoader.KSpeed + targetNum];
        staytime = _jLoader.PatternDicData[patternNum][_jLoader.KStaytime + targetNum];
        type = (int)_jLoader.PatternDicData[patternNum][_jLoader.KType + targetNum];
    }
}
