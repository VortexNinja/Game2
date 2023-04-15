using RPG.Saving;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float healthPoints = 100;

        bool isDead = false;

        public void TakeDamage(float damage)
        {
            if (isDead)
                return;

            healthPoints -= damage;
            if (healthPoints <= 0)
                Die();
        }

        public void Heal(float healingPower)
        {
            healthPoints += healingPower;
        }

        void Die()
        {
            isDead = true;
            GetComponent<Animator>().SetTrigger("Die");
        }

        void Revive()
        {
            isDead = false;
            GetComponent<Animator>().SetTrigger("Revive");
        }

        public bool IsDead()
        {
            return isDead;
        }

        public object SaveState()
        {
            return healthPoints;
        }

        public void LoadState(object loadedState)
        {
            healthPoints = (float)loadedState;
            if (healthPoints <= 0)
                Die();

            else if(isDead == true)
                    Revive();
        }
    }

}

