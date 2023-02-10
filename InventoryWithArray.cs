using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryWithArray : MonoBehaviour
{
    public string[,] inventoryOfCodes = {
        {"","","",""},
        {"","","",""},
        {"","","",""},
        {"","","",""}};
    private string[,] items = {
        {"Gears", "Hammer", "Key", "Knife"},
        {"Fuel", "Tire", "Oil", ""},
        {"Coat", "Glove", "Shoes", "Pants"},
        {"Sandwich", "Water", "Beef", "Apple"}};

    private int holdRandomNumber_1;
    private int holdRandomNumber_2;
    private bool controlOfStartOfCode = false;

    private void Start() {
        StoreItemsInInvetory();
        ShowItemsOfInvetory();
        ConvertStringToIntCode("Salmao");
    }

    void Update()
    {

    }

    private void ShowItemsOfInvetory()
    {
        for(int i = 0;i < 4;i++)
        {
            for(int j = 0;j < 4;j++)
            {
                Debug.Log(inventoryOfCodes[i,j]);
            }
        }
    }

    private void StoreItemsInInvetory()
    {
        for(int i = 0;i < 4;i++)
        {
            for(int j = 0;j < 4;j++)
            {
                this.holdRandomNumber_1 = UnityEngine.Random.Range(0,3);
                this.holdRandomNumber_2 = UnityEngine.Random.Range(0,3);
                this.inventoryOfCodes[i,j] = ConvertStringToIntCode(items[holdRandomNumber_1,holdRandomNumber_2]);
            }
        }
    }

    private string ConvertStringToIntCode(string item)
    {
        string temporaryNumberToReturn = "";
        int maxValueToIterLoop = item.Length;

        for(int i = 0; i < maxValueToIterLoop; i++)
        {
            temporaryNumberToReturn += Convert.ToByte(item[i]);
        }
        return(temporaryNumberToReturn);
    }
}
