using UnityEngine;

namespace RPG.Core
{
    public class PersistentObjects : MonoBehaviour
    {
        private void Awake()
        {
            if (FindObjectsOfType<PersistentObjects>().Length > 1)
                Destroy(gameObject);
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
