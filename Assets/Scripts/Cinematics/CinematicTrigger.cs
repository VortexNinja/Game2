using RPG.Control;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool played = false;
        GameObject player;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            GetComponent<PlayableDirector>().played += player.GetComponent<PlayerController>().DisableControl;
            GetComponent<PlayableDirector>().stopped += player.GetComponent<PlayerController>().EnableControl;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag != "Player")
                return;

            if (played)
                return;

            GetComponent<PlayableDirector>().Play();

            played = true;
        }
    }
}
