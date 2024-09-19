using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KanGeneratorController : MonoBehaviour
{
    public GameObject _kan;

    Transform _transform;

    public const float kSummonTime = 5.0f;

    public float timer = 0.0f;

    void Start()
    {
        // publicにしたのでコメントアウト
        //_kan = GameObject.Find("Assets/Kan(PrefabAsset)");
        _transform = this.gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        
        if(timer > kSummonTime)
        {
            timer -= kSummonTime;
            Vector3 vector3 = _transform.position;
            Instantiate(_kan, vector3, Quaternion.identity);
        }
    }
}
