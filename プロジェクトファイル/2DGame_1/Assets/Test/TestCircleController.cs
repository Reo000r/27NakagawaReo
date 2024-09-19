using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UIElements;

public class TestCircleController : MonoBehaviour
{
    // �I��MaxScale��ς��Ă��ς��Ȃ������̂Ńe�X�g

    private const float kMaxScale = 3.0f;
    private const float kMinScale = 0.0f;
    private const float kSpeed = 1.0f;
    private float timer = 0.0f;

    private const float kChangeSpeed = 0.3f;

    private State state = State.Up;

    enum State
    {
        Up,
        Down,
        Stay,

        Stop
    }

    // Start is called before the first frame update
    void Start()
    {
        this.transform.localScale = new Vector3(kMinScale, kMinScale, 1);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime / 2;
        float t;
        float scale;
        float scaleX;
        float scaleY;

        switch (state)
        {
            case State.Up:
                //Debug.Log($"{kChangeSpeed}");
                //float f = 1.0f / kChangeSpeed;
                //// Mathf.Clamp01 ���g��float�̒l��0~1�Ɋۂ߂Ă���
                //// Time.deltaTime / kAnimTime ��deltaTime�������scale�̌���������߂Ă���
                //t = Mathf.Clamp01(timer / kChangeSpeed);
                //scaleY = t * kMaxScale;
                //// 2 * Mathf.PI �ň�����钷�������߂Ď��Ԃ��|����ƈ����₷���Ȃ�
                //float sin = Mathf.Sin(2 * Mathf.PI * f * timer);
                //// �T�C�Y����
                //scaleX = sin * t * kMaxScale;
                ////Debug.Log($"sin/size : {sin}/{scale}({modeTime})");
                ////Debug.Log($"sin = Mathf.Sin(2 * Mathf.PI * f * modeTime) : {sin} = {Mathf.Sin(2 * Mathf.PI * f * modeTime)}(2 * 3.14 * {f} * {modeTime})");
                //this.transform.localScale = new Vector3(scaleX, scaleY, 1);


                t = Mathf.Clamp01(timer / kChangeSpeed);
                scaleY = t * kMaxScale;

                Debug.Log($"{t}");
                float f = 1.0f / (kChangeSpeed);
                
                float sin = Mathf.Sin(2 * Mathf.PI * f * timer);
                // �T�C�Y����
                scaleX = sin * t * kMaxScale;
                
                this.transform.localScale = new Vector3(scaleX, scaleY, 1);


                // �i�s�x(0~1)
                t = timer / kSpeed;
                // 
                scale = t * kMaxScale;
                //this.transform.localScale = new Vector3(scale, scale, 1);
                break;

            case State.Stay:

                break;

            case State.Down:
                // �i�s�x(0~1)
                t = timer / kSpeed;
                t = 1 - t;
                // 
                scale = t * kMaxScale;
                this.transform.localScale = new Vector3(scale, scale, 1);
                break;
        }

        if(timer > kSpeed)
        {
            StateUpdate(State.Stop);
            //StateUpdate();
            //timer -= kSpeed;
        }
            
    }

    private void StateUpdate()
    {
        if(state == State.Up)
        {
            state = State.Stay;
            Debug.Log($"ChangeMode:{state}");
        }
        else if(state == State.Stay)
        {
            state = State.Down;
            Debug.Log($"ChangeMode:{state}");
        }
        else if(state == State.Down)
        {
            state = State.Up;
            Debug.Log($"ChangeMode:{state}");
        }
    }

    private void StateUpdate(State s)
    {
        state = s;
    }
}
