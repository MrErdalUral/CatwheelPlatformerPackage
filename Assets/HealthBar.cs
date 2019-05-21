using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HealthBar : MonoBehaviour
{
    private Image _healthBarImage;
    public HealthObject Health;
    // Start is called before the first frame update
    void Start()
    {
        _healthBarImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_healthBarImage == null) return;
        _healthBarImage.fillAmount = Health.Health / Health.MaxHealth;
    }
}
