using RPG.Combat;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] Transform[] paths;
        bool chaseFailed = true;
        float chaseTimer = Mathf.Infinity;

        int currentPath;

        Health player;
        Mover mover;
        Fighter fighter;
        Health health;

        void Awake()
        {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
            player = playerGO.GetComponent<Health>();
            currentPath = 0;
        }


        void Update()
        {
            if (health.IsDead() || player.IsDead())
                return;

            
            if(ISeeThePlayer())
            {
                AttackBehaviour();
            }
            else
            {
                if (chaseFailed == false)
                    SuspicionBehaviour();
                else
                    PatrolBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, (player.transform.position - transform.position).normalized, out hit))
            {
                if(hit.transform.tag == "Player")
                {
                    chaseFailed = false;
                    chaseTimer = 0;
                    fighter.StartFighterAction(player);
                }
            } 
        }

        private void SuspicionBehaviour()
        {
            fighter.Cancel();
            mover.Cancel();
            chaseTimer += Time.deltaTime;

            if(chaseTimer > 5)
            {
                chaseFailed = true;
            }
            
        }

        private void PatrolBehaviour()
        {
            if (Vector3.Distance(transform.position, paths[currentPath].position) > 2)
                mover.StartMovementAction(paths[currentPath].position, 0.5f);
            else
            {
                currentPath++;
                if (currentPath >= paths.Length)
                    currentPath = 0;
            }

        }

        bool ISeeThePlayer()
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, direction);
            float distance = Vector3.Distance(player.transform.position, transform.position);

            bool withinSight = angle < 50;
            bool closeEnough = distance < 10;
            bool tooClose = distance < 3;
            return (withinSight && closeEnough) || tooClose;
        }

      
    }
}
