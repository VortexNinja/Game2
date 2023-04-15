using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] AnimatorOverrideController weaponOverride = null;
        [SerializeField] float weaponDamage = 10;
        [SerializeField] float weaponRange = 1;
        [SerializeField] bool leftHanded = false;

        public float GetWeaponDamage() { return weaponDamage; }
        public float GetWeaponRange() { return weaponRange; }


        public GameObject Spawn(Transform rightHandTransform, Transform leftHandTransform, Animator animator)
        {
            GameObject weaponInHand;
            if (!leftHanded)
                weaponInHand = Instantiate(weaponPrefab, rightHandTransform);
            else
                weaponInHand = Instantiate(weaponPrefab, leftHandTransform);
            animator.runtimeAnimatorController = weaponOverride;
            return weaponInHand;
        }
    }
}