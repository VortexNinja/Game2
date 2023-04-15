using System.Collections.Generic;
using UnityEngine;


namespace RPG.Saving
{
    [ExecuteAlways]
    public class SaveableEntity : MonoBehaviour
    {

        [SerializeField] string uniqueIndentifier = "";

 

        private void Start()
        {
            if (uniqueIndentifier == "")
                uniqueIndentifier = System.Guid.NewGuid().ToString();
        }


        public string GetUniqueIdentifier()
        {
            return uniqueIndentifier;
        }

        public object SaveState()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            foreach(ISaveable saveable in GetComponents<ISaveable>())
            {
                data[saveable.GetType().ToString()] = saveable.SaveState();
            }
            return data;
        }

        public void RestoreState(object loadedState)
        {
                Dictionary<string, object> data = (Dictionary<string, object>)loadedState;
                
                foreach(ISaveable saveable in GetComponents<ISaveable>())
            {
                if (data.ContainsKey(saveable.GetType().ToString()))
                    saveable.LoadState(data[saveable.GetType().ToString()]);
            }

         }
       }

}