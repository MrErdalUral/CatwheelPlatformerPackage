using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UIHealthText : MonoBehaviour
{
    [SerializeField]
    private HealthObject _Health;

    private Text _Text;
    // Start is called before the first frame update
    void Awake()
    {
        _Text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_Health)
            _Text.text = $"Health: {_Health.Health}";
    }
}
    