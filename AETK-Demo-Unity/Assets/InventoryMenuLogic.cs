using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenuLogic : MonoBehaviour
{

    private List<InventoryItem> inventory;

    public class InventoryItem {

        private string Name;
        private int Number;

        public InventoryItem(string s, int n)
        {
            Name = s;
            Number = n;
        }

        public InventoryItem(string s)
        {
            Name = s;
            Number = 1;
        }

        public string getName()
        {
            return this.Name;
        }

        public int getNumber()
        {
            return this.Number;
        }

        public void changeNumber(int additive)
        {
            if (Number + additive <= 0)
            {
                Number = 0;
            }
            else
            {
                this.Number = Number + additive;
            }
        }

    }





    // Start is called before the first frame update
    void Start()
    {
        inventory = new List<InventoryItem>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void testBoy() {

        inventory.Add(new InventoryItem("apple", 1));

        showInventory();

    }

    public void showInventory()
    {
        foreach (InventoryItem i in inventory)
        {
            Debug.Log(i.getName());

        }


    }

    
}
