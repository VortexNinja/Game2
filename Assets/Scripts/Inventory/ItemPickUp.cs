using RPG.Control;
using RPG.Core;
using UnityEngine;

namespace RPG.Inventory
{
    public class ItemPickUp : MonoBehaviour
    {
        [SerializeField] Item item;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag != "Player") return;

            PlayerController player = other.GetComponent<PlayerController>();

            if (player.playerState != PlayerController.state.pickingItem)
                return;

            Inventory playerbag = FindObjectOfType<Inventory>();
            if (playerbag.IsFull()) return;

            other.transform.LookAt(transform);
            other.GetComponent<ActionScheduler>().CancelAction();
            other.GetComponent<Animator>().SetTrigger("Pickup");
            playerbag.AddItem(item);
            Destroy(gameObject, 0.5f);
        }


    }

}