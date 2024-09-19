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

    // 回転方向 (direction Of Rotation) 1 or -1
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
        //最終的にはこっちに書きたい
        //KanBound();
        
        // 画面外の高い場所にいる時に重力を強くしたい(早く戻って来てほしい)
        if (_transform.position.y > 10)
        {
            _rb2D.gravityScale = 2.0f;
        }
        else
        {
            _rb2D.gravityScale = 0.4f;
        }

        // 端に来た時に弾き返す
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

        // 落ちた場合削除
        if (_transform.position.y < -7)
        {
            Destroy(this.gameObject);
        }

        // 落下速度制限がしたい
        //if(Rb2D.velocity.magunitude < 10)
    }

    // x座標の移動方向と回転方向を反転させる
    private void PhyRotaInversion()
    {
        // 移動方向反転
        phyOfRota *= -1;
        direction = new Vector3(direction.x * phyOfRota, _rb2D.velocity.y / kPhyForce, 0);
        _rb2D.velocity = direction * kPhyForce;

        // 回転反転
        dirOfRota *= -1;
        _rb2D.angularVelocity = rotaForce * Mathf.PI * dirOfRota;
    }

    private void KanClicked()
    {
        clickCount++;

        // 徐々に色を薄くする
        _spriteRenderer.color -= new Color(0, 0, 0, 0.2f);

        // スコア加算
        GameManagerSingleton.Instance.AddScore(clickCount);

        // 一定回数クリックで削除
        if (clickCount >= 5)
        {
            Destroy(this.gameObject);
        }

        // 打ち上げ
        phyOfRota *= -1; 
        direction = new Vector3(Random.Range(0.0f, 0.3f) * phyOfRota, 1, 0);
        //Rb2D.AddForce(direction * 1, ForceMode2D.Force);
        _rb2D.velocity = direction * kPhyForce;

        // 回転量決定
        dirOfRota *= -1;
        rotaForce = Random.Range(80.0f, 200.0f);
        _rb2D.angularVelocity =  rotaForce * Mathf.PI * dirOfRota;

        Debug.Log("Bound");
    }

    // マウスが乗った時
    private void OnMouseEnter()
    {
        //Debug.Log("KanEnter");
    }

    // マウスが乗っている時
    private void OnMouseOver()
    {
        //if (Input.GetMouseButtonDown(0))
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(1))
        {
            // クリックされたら
            KanClicked();
        }
    }

    // マウスが離れた時
    private void OnMouseExit()
    {
        //Debug.Log("KanExit");
    }
}
