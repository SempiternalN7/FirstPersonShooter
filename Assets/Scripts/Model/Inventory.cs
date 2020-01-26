using UnityEngine;

namespace Geekbrains
{
	public sealed class Inventory : IInitialization
	{
		private Weapon[] _weapons = new Weapon[5];

        private static int _weaponNumber = 0;

        public Weapon[] Weapons => _weapons;

		public FlashLightModel FlashLight { get; private set; }

		public void Initialization()
		{
			_weapons = ServiceLocatorMonoBehaviour.GetService<CharacterController>().
				GetComponentsInChildren<Weapon>();

			foreach (var weapon in Weapons)
			{
				weapon.IsVisible = false;
			}

			FlashLight = Object.FindObjectOfType<FlashLightModel>();
			FlashLight.Switch(FlashLightActiveType.Off);
		}

        /// <summary>
        /// Выбор оружия по номеру
        /// </summary>
        /// <param name="i">Номер оружия</param>
        public static void SelectWeapon(int i)
        {
            if (i < 0) i = 0;
            if (i > 2) i = 2;
            ServiceLocator.Resolve<WeaponController>().Off();
            var tempWeapon = ServiceLocator.Resolve<Inventory>().Weapons[i];
            if (tempWeapon != null)
            {
                ServiceLocator.Resolve<WeaponController>().On(tempWeapon);
                _weaponNumber = i;
            }
        }

        public static void SelectNextWeapon()
        {
            _weaponNumber++;
            SelectWeapon(_weaponNumber);
        }

        public static void SelectPreviousWeapon()
        {
            _weaponNumber--;
            SelectWeapon(_weaponNumber);
        }

        public static void RemoveWeapon()
        {
            ServiceLocator.Resolve<WeaponController>().Off();
            _weaponNumber = 0;
        }
	}
}