using RPG.Core;
using RPG.Saving;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Control
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] GameObject gameMenu;
        [SerializeField] Button loadButton;

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                gameMenu.SetActive(true);
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().DisableControl();

            }
            string path = Path.Combine(Application.persistentDataPath, "Data.sav");
            if (!File.Exists(path))
                loadButton.interactable = false;
            else
                loadButton.interactable = true;
        }

        public void OnClick_ResumeGame()
        {
            gameMenu.SetActive(false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().EnableControl();
        }

        public void OnClick_SaveGame()
        {
            StartCoroutine(SaveGame());
        }

        IEnumerator SaveGame()
        {
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(0.5f, "Saving...");
            SavingSystem savingSystem = FindObjectOfType<SavingSystem>();
            savingSystem.Save();
            OnClick_ResumeGame();
            yield return fader.FadeIn(0.5f);

        }

        public void OnClick_LoadGame()
        {
            StartCoroutine(LoadGame());
        }
        IEnumerator LoadGame()
        {
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(0.5f, "Loading...");
            SavingSystem savingSystem = FindObjectOfType<SavingSystem>();
            savingSystem.Load();
            yield return new WaitForSeconds(0.5f);
            OnClick_ResumeGame();
            yield return fader.FadeIn(0.5f);

        }


        public void OnClick_QuitGame()
        {
            Application.Quit() ;
        }
    }

}