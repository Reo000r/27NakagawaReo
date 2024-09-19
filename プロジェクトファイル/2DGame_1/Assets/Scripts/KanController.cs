using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KanController : MonoBehaviour
{
    Rigidbody2D _rb2D;
    Transform _transform;
    SpriteRenderer _spriteRenderer;
    //bool isOnMouse = false;

    Vector3 direction;

    // ��]���� (direction Of Rotation) 1 or -1
    int dirOfRota = 1;

    int phyOfRota = 1;
    
    float rotaForce;

    const float kPhyForce = 13.0f;

    int clickCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        _rb2D = this.gameObject.GetComponent<Rigidbody2D>();
        _transform = this.gameObject.GetComponent<Transform>();
        _spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //�ŏI�I�ɂ͂������ɏ�������
        //KanBound();
        
        // ��ʊO�̍����ꏊ�ɂ��鎞�ɏd�͂�����������(�����߂��ė��Ăق���)
        if (_transform.position.y > 10)
        {
            _rb2D.gravityScale = 2.0f;
        }
        else
        {
            _rb2D.gravityScale = 0.4f;
        }

        // �[�ɗ������ɒe���Ԃ�
        if (_transform.position.x < -7)
        {
            _rb2D.MovePosition(new Vector3(-7, _transform.position.y, _transform.position.z));

            PhyRotaInversion();
        }
        if (_transform.position.x > 7)
        {
            _rb2D.MovePosition(new Vector3(7, _transform.position.y, _transform.position.z));

            PhyRotaInversion();
        }

        // �������ꍇ�폜
        if (_transform.position.y < -7)
        {
            Destroy(this.gameObject);
        }

        // �������x������������
        //if(Rb2D.velocity.magunitude < 10)
    }

    // x���W�̈ړ������Ɖ�]�����𔽓]������
    private void PhyRotaInversion()
    {
        // �ړ��������]
        phyOfRota *= -1;
        direction = new Vector3(direction.x * phyOfRota, _rb2D.velocity.y / kPhyForce, 0);
        _rb2D.velocity = direction * kPhyForce;

        // ��]���]
        dirOfRota *= -1;
        _rb2D.angularVelocity = rotaForce * Mathf.PI * dirOfRota;
    }

    private void KanClicked()
    {
        clickCount++;

        // ���X�ɐF�𔖂�����
        _spriteRenderer.color -= new Color(0, 0, 0, 0.2f);

        // �X�R�A���Z
        GameManagerSingleton.Instance.AddScore(clickCount);

        // ���񐔃N���b�N�ō폜
        if (clickCount >= 5)
        {
            Destroy(this.gameObject);
        }

        // �ł��グ
        phyOfRota *= -1; 
        direction = new Vector3(Random.Range(0.0f, 0.3f) * phyOfRota, 1, 0);
        //Rb2D.AddForce(direction * 1, ForceMode2D.Force);
        _rb2D.velocity = direction * kPhyForce;

        // ��]�ʌ���
        dirOfRota *= -1;
        rotaForce = Random.Range(80.0f, 200.0f);
        _rb2D.angularVelocity =  rotaForce * Mathf.PI * dirOfRota;

        Debug.Log("Bound");
    }

    // �}�E�X���������
    private void OnMouseEnter()
    {
        //Debug.Log("KanEnter");
    }

    // �}�E�X������Ă��鎞
    private void OnMouseOver()
    {
        //if (Input.GetMouseButtonDown(0))
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(1))
        {
            // �N���b�N���ꂽ��
            KanClicked();
        }
    }

    // �}�E�X�����ꂽ��
    private void OnMouseExit()
    {
        //Debug.Log("KanExit");
    }
}
