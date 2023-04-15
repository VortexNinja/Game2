using RPG.Core;
using RPG.Movement;
using RPG.Saving;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum destinationIdentifier {A,B,C,D,E }
        [SerializeField] int sceneToLoad;
        [SerializeField] Transform spawnPoint;
        [SerializeField] destinationIdentifier destination;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag != "Player")
                return;

            StartCoroutine(Load());
        }
        
        IEnumerator Load()
        {
            DontDestroyOnLoad(gameObject);
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(0.5f, "Loading...");
            //saving scene
            SavingSystem savingSystem = FindObjectOfType<SavingSystem>();
            savingSystem.Save();
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            Portal newPortal = null;

            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this)
                    continue;

                if(portal.destination == destination)
                {
                    newPortal = portal;
                    break;
                }
            }

            savingSystem.Load(true);
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Mover>().Teleport(newPortal.spawnPoint.position, newPortal.spawnPoint.rotation);
            savingSystem.Save();
            yield return fader.FadeIn(1f);
            Destroy(gameObject);
        }

        
    }
}
