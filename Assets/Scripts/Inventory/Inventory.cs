using RPG.Saving;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Inventory
{
    public class Inventory : MonoBehaviour, ISaveable
    {
        [SerializeField] Image[] slotsUI;
        Item[] slots;
        

        private void Start()
        {
            slots = new Item[10];
            
        }

        public void AddItem(Item item)
        {
            for(int i =0; i<slots.Length; i++)
            {
                if (slots[i] == null)
                {
                    slots[i] = item;
                    Instantiate(slots[i].itemImage, slotsUI[i].transform);
                    break;
                }

            }
        }

        public void UseItem(int i)
        {
            if (slots[i] == null) return;

            print("using item " + slots[i]);

            slots[i].Use();
            DropItem(i);
        }

        public void DropItem(int i)
        {
            if (slots[i] == null) return;

            print("Dropping item " + slots[i]);
            
            slots[i] = null;
            Destroy(slotsUI[i].transform.GetChild(0).gameObject);

                   

            for(int k = i; k<slots.Length-1; k++)
            {
                if (slots[k + 1] == null) return;
                slots[k] = slots[k + 1];
                slotsUI[k + 1].transform.GetChild(0).SetParent(slotsUI[k].transform, false);
                slots[k + 1] = null;
            }

        }

        public object SaveState()
        {
            string[] savedItems = new string[10];

            for(int i = 0; i < savedItems.Length; i++)
            {
                if (slots[i] == null)
                    savedItems[i] = "null";
                else
                    savedItems[i] = slots[i].name;
            }

           
            
            return savedItems;
        }

        public void LoadState(object loadedState)
        {
            string[] savedItems = (string[])loadedState;

            for(int i = slots.Length -1; i > 0; i--)
            {
                DropItem(i);
            }
            
            for(int i = 0; i<savedItems.Length; i++)
            {
                if (savedItems[i] == "null")
                    return;

                AddItem( Resources.Load<Item>(savedItems[i]) );
            }
        }

        public bool IsFull()
        {
            if (slots[slots.Length - 1] == null)
                return false;
            else
                return true;
        }
    }

     
}
