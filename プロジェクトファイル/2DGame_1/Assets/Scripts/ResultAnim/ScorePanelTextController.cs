using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScorePanelTextController : MonoBehaviour
{
    [SerializeField] private Transform _panelTransform;
    [SerializeField] private TextMeshProUGUI _panelText;

    private const string kText = "Score:";
    private Vector3 offset = new Vector3(-1.8f, 1.3f, 0);

    void Start()
    {
        _panelText.text = kText;
    }

    // Update is called once per frame
    void Update()
    {
        _panelText.transform.position = Camera.main.WorldToScreenPoint(_panelTransform.position + offset);
    }
}
