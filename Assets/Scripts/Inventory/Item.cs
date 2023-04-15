using RPG.Combat;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Inventory
{
    [CreateAssetMenu(fileName = "Item", menuName = "Items/Make New Item", order = 0)]
    public class Item : ScriptableObject
    {

        enum type { consumable, weapon, keyitem }

        [Header("-- Essentials --")]
        [SerializeField] public GameObject itemPrefab;
        [SerializeField] public Image itemImage;
        [SerializeField] type itemType;
        [Header("-- Item Parameters --")]
        [SerializeField] float healingPower = 0;
        [Header("--Weapon Parameters --")]
        [SerializeField] Weapon weapon;


        public void Use()
        {
            if(itemType == type.consumable)
            {
                Health player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
                player.Heal(healingPower);
            }

            if(itemType == type.weapon)
            {
                Fighter player = GameObject.FindGameObjectWithTag("Player").GetComponent<Fighter>();
                player.EquipWeapon(weapon);

            }

            if (itemType == type.keyitem)
            {

            }
        }
    }
}