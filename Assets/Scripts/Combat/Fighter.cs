using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] Weapon defaultWeapon;
        [SerializeField] Weapon currentWeapon;
        [SerializeField] Transform RighthandTransform = null;
        [SerializeField] Transform LefthandTransform = null;
        [SerializeField] float weaponDelay = 1;
        GameObject weaponInHand = null;

        float attackTimer = Mathf.Infinity;

        Health health;
        Mover mover;
        Health target;
       

        private void Awake()
        {
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            target = null;

            EquipWeapon(defaultWeapon);
        }

        private void Update()
        {
            if (target == null) 
                return;

            if (target.IsDead() || health.IsDead())
            {
                Cancel();
                return;
            }
                

            float distance = Vector3.Distance(transform.position, target.transform.position);
            bool withinRange = distance <= currentWeapon.GetWeaponRange();
            if (withinRange)
            {
                if (attackTimer > weaponDelay)
                {
                    mover.Cancel();
                    transform.LookAt(target.transform.position);
                    GetComponent<Animator>().SetTrigger("Attack");
                    attackTimer = 0;
                }

                else
                {
                    attackTimer += Time.deltaTime;
                }
               
            }
            else
            {
                mover.MoveTo(target.transform.position);
            }


           
          
        }

        public void StartFighterAction(Health _target)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = _target;

        }

        public void Cancel()
        {
            target = null;
            attackTimer = Mathf.Infinity;
        }

        public void Hit()
        {
            if(target)
                target.TakeDamage(currentWeapon.GetWeaponDamage());
        }

        public void EquipWeapon(Weapon weapon)
        {
            if(weapon == null)
            {
                currentWeapon = defaultWeapon;
            }
            if(weaponInHand)
            {
                Destroy(weaponInHand);
            }

            Animator animator = GetComponent<Animator>();
            weaponInHand = weapon.Spawn(RighthandTransform, LefthandTransform,  animator);
            currentWeapon = weapon;
        }

    }
}
