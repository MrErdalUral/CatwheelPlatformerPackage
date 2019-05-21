using System.Collections;
using System.Collections.Generic;
using Assets;
using Assets.Input_Handlers;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField]
    private WeaponData _weapon;

    public SpriteSetter SpriteSetter;

    private bool _isInContactWithThePlayer;
    private bool _isSwapButtonDown;

    public bool IsInContactWithThePlayer
    {
        get => _isInContactWithThePlayer;
        set => _isInContactWithThePlayer = value;
    }

    public bool IsSwapButtonDown
    {
        get => _isSwapButtonDown;
        set => _isSwapButtonDown = value;
    }

    public WeaponData Weapon
    {
        get => _weapon;
        set
        {
            _weapon = value;
            SetSprite();
            if (value.WeaponType == WeaponType.Hand)
                Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetSprite();
    }

    private void SetSprite()
    {
        if (_weapon == null) return;
        if (SpriteSetter != null)
            SpriteSetter.SetSprite(Weapon.WeaponSprite);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsInContactWithThePlayer && IsSwapButtonDown)
        {
            var wController = FindObjectOfType<WeaponController>();
            if (wController == null) return;
            
            Weapon = wController.EquipWeapon(Weapon);
        }
    }
}
