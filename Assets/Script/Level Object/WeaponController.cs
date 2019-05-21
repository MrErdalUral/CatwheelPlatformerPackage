using Assets.Input_Handlers;
using UnityEngine;

namespace Assets
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField]
        private EquipmentData _equipmentData;

        public EquipmentDataUnityEvent OnUseWeapon;

        public SpriteSetter FrontHandRenderer;
        public SpriteSetter BackHandRenderer;
        public AnimatorParameterController AnimatorParameterController;
        //public Spawner ProjSpawner;

        public WeaponData DefaultHandWeaponData;
        public WeaponPickup WeaponPickupPrefab;

        public WeaponData FrontHandWeaponData
        {
            get => _equipmentData.FrontHand;
            set => SetFrontWeapon(value);
        }
        public WeaponData BackHandWeaponData
        {
            get => _equipmentData.BackHand;
            set => SetBackWeapon(value);
        }

        private void SetFrontWeapon(WeaponData value)
        {
            if (AnimatorParameterController)
                AnimatorParameterController.AttackType = GetAttackType();
            if (FrontHandRenderer)
                FrontHandRenderer.SetSprite(value.WeaponSprite != null ? value.WeaponSprite : DefaultHandWeaponData.WeaponSprite);
            _equipmentData.FrontHand = value;
        }

        private int GetAttackType()
        {
            var frontHand = _equipmentData.FrontHand.WeaponType;
            var backHand = _equipmentData.BackHand.WeaponType;

            if (frontHand == WeaponType.OneHanded || frontHand == WeaponType.Dagger)
            {
                if (backHand == WeaponType.Hand)
                    return 0;
                else if (backHand == WeaponType.Dagger)
                    return 1;
                else if (backHand == WeaponType.Shield)
                    return 2;
            }

            return 0;
        }

        private void SetBackWeapon(WeaponData value)
        {
            if (AnimatorParameterController)
                AnimatorParameterController.AttackType = GetAttackType();
            if (BackHandRenderer)
                BackHandRenderer.SetSprite(value.WeaponSprite != null ? value.WeaponSprite : DefaultHandWeaponData.WeaponSprite);
            _equipmentData.BackHand = value;
        }

        public void UseWeapons()
        {
            OnUseWeapon.Invoke(_equipmentData);
        }
        void Start()
        {
            if (_equipmentData != null)
            {
                SetFrontWeapon(DefaultHandWeaponData);
                SetBackWeapon(DefaultHandWeaponData);
            }
        }

        public WeaponData EquipWeapon(WeaponData weapon)
        {
            if (!weapon.CanBeDualWielded)
            {
                return EquipFrontHandWeapon(weapon);
            }
            else
            {
                if (FrontHandWeaponData.WeaponType == WeaponType.Hand || BackHandWeaponData == weapon)
                    return EquipFrontHandWeapon(weapon);
                else
                    return EquipBackHandWeapon(weapon);
            }
        }

        private WeaponData EquipBackHandWeapon(WeaponData weapon)
        {
            var temp = BackHandWeaponData;
            BackHandWeaponData = weapon;
            return temp;
        }

        private WeaponData EquipFrontHandWeapon(WeaponData weapon)
        {
            var temp = FrontHandWeaponData;
            FrontHandWeaponData = weapon;
            return temp;
        }

        private void DropWeapon()
        {
            if (BackHandWeaponData.WeaponType != WeaponType.Hand)
            {
                var wPickup = Instantiate(WeaponPickupPrefab, transform.position, Quaternion.identity);
                wPickup.Weapon = BackHandWeaponData;
                BackHandWeaponData = DefaultHandWeaponData;
                return;
            }

            if (FrontHandWeaponData.WeaponType != WeaponType.Hand)
            {
                var wPickup = Instantiate(WeaponPickupPrefab, transform.position, Quaternion.identity);
                wPickup.Weapon = FrontHandWeaponData;
                FrontHandWeaponData = DefaultHandWeaponData;
                return;
            }
        }
    }
}
