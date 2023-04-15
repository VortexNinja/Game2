using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        public enum state { fighting, moving, pickingItem, doingNothing };
        public state playerState;

        [SerializeField] Transform playerCamera;
        [Header("Mouse Cursors")]
        [SerializeField] Texture2D FighterCursor;
        [SerializeField] Texture2D PickupCursor;
        [SerializeField] Texture2D MovementCursor;

        bool controlEnabled = true;
        
        Mover mover;
        Fighter fighter;
        Health health;

        
        void Awake()
        {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            playerState = state.doingNothing;

        }


        void Update()
        {
            if (controlEnabled == false || health.IsDead())
                return;

            CameraMovementBehaviour();

            if (CombatBehaviour())
                return;

            if (PickupBehaviour()) 
                return;
                
            
            if (MovementBehaviour())
                return;

            

            
        }

        private bool PickupBehaviour()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray, 100);

            foreach(RaycastHit hit in hits)
            {
                

                if(hit.transform.tag == "Item")
                {
                    Cursor.SetCursor(PickupCursor, new Vector2(50, 50), CursorMode.Auto);

                    if (Input.GetMouseButtonDown(0))
                        mover.StartMovementAction(hit.transform.position);

                    playerState = state.pickingItem;
                    return true;
                }
            }
            return false;
        }

        private bool CombatBehaviour()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray, 100);

            foreach(RaycastHit hit in hits)
            {
                if(hit.transform.tag == "Enemy")
                {
                    Health enemyHealth = hit.transform.GetComponent<Health>();
                    if (enemyHealth.IsDead())
                        continue;

                    Cursor.SetCursor(FighterCursor, new Vector2(50,50), CursorMode.Auto);

                    if (Input.GetMouseButtonDown(0))
                        fighter.StartFighterAction(enemyHealth);
                    playerState = state.fighting;

                    return true;

                }
            }
            return false;
        }

        private bool MovementBehaviour()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray, 100);


            foreach(RaycastHit hit in hits)
            {
                
                if (hit.transform.tag == "Ground")
                {
                    Cursor.SetCursor(MovementCursor, new Vector2(50, 50), CursorMode.Auto);
                    if (Input.GetMouseButtonDown(0))
                        mover.StartMovementAction(hit.point);

                    playerState = state.moving;
                    return true;
                }
                    
               

            }
            return false;
        }

        private void CameraMovementBehaviour()
        {
            if (Input.GetMouseButton(1))
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                playerCamera.Rotate(0, Input.GetAxis("Mouse X") * 10, 0, Space.World);

            }
            if (Input.GetMouseButtonUp(1))
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

            }
        }

        public void EnableControl(PlayableDirector pd = null)
        {
            controlEnabled = true;
        }

        public void DisableControl(PlayableDirector pd = null)
        {
            GetComponent<ActionScheduler>().CancelAction();
            controlEnabled = false;

        }
    }
}
