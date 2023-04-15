using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        private void Start()
        {
            //string path = Path.Combine(Application.persistentDataPath, "Data.sav");
            //if (File.Exists(path))
                //Load();
        }

        public void Save()
        {
            string path = Path.Combine(Application.persistentDataPath, "Data.sav");
            object state = SaveState();

            using (FileStream stream = File.Open(path, FileMode.Create) )
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);

            }


                print("Saving game at " + path);
        }

        public void Load(bool usingPortal = false)
        { 
            StartCoroutine( RestoreState(LoadFile(), usingPortal) );
            print("Loading game ");
        }

        Dictionary<string, object> LoadFile()
        {
            string path = Path.Combine(Application.persistentDataPath, "Data.sav");

            if (!File.Exists(path))
                return new Dictionary<string, object>();
           
            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (Dictionary<string, object>)formatter.Deserialize(stream);
            }
        }

        object SaveState()
        {
            Dictionary<string, object> state = LoadFile();

            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                state[saveable.GetUniqueIdentifier()] = saveable.SaveState();
            }
            state["currentScene"] = SceneManager.GetActiveScene().buildIndex;
            return state;
        }

        IEnumerator RestoreState(Dictionary<string, object> state, bool usingPortal = false)
        {
            if(usingPortal == false)
            {
                int scene = (int)state["currentScene"];
                if (SceneManager.GetActiveScene().buildIndex != scene)
                    yield return SceneManager.LoadSceneAsync(scene);
            }


            foreach(SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                if(state.ContainsKey(saveable.GetUniqueIdentifier()))
                {
                    saveable.RestoreState(state[saveable.GetUniqueIdentifier()]);
                }
            }
        }
    }
}
