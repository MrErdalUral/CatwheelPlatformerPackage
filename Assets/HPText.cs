using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class HPText : MonoBehaviour
{
    private TextMeshProUGUI _text;
    public HealthObject Health;
    // Start is called before the first frame update
    void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Health == null) return;
        _text.text = "HP: " + Health.Health;
    }
}
