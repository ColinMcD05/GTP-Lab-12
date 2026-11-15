using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] List<InventoryItem> inventory;

    public void Start()
    {
        //Initialize array
        inventory = new List<InventoryItem>
        {
            new InventoryItem(Random.Range(1, 1000), "Armor", Random.Range(1, 1000)),
            new InventoryItem(Random.Range(1, 1000), "Sword", Random.Range(1, 1000)),
            new InventoryItem(Random.Range(1, 1000), "Helmet", Random.Range(1, 1000)),
            new InventoryItem(Random.Range(1, 1000), "Chest", Random.Range(1, 1000)),
            new InventoryItem(Random.Range(1, 1000), "Belt", Random.Range(1, 1000)),
            new InventoryItem(Random.Range(1, 1000), "Steel", Random.Range(1, 1000))
        };
    }

    public InventoryItem LinearSearchByName(string itemName)
    {
        //Searches through array until found by name
        for(int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].GetName() == itemName)
                return inventory[i];
        }
        return null;
    }

    public InventoryItem BinarySearchByID(int ID)
    {
        //Sort inventory by ID
        MergeSortByID(0, inventory.Count-1);

        //Get bounds
        int left = 0;
        int right = inventory.Count - 1;
        
        while(left <= right)
        {
            //Get mid way point of current search
            int mid = left + (right - left) / 2;

            //Return mid if equal to ID
            if (inventory[mid].GetID() == ID)
            {
                return inventory[mid];
            }
            //Search right side of search
            else if (inventory[mid].GetID() < ID)
            {
                left = mid + 1;
            }
            //Search left side of search
            else 
            {
                right = mid - 1;
            }
        }

        return null;
    }

    public void MergeSortByID(int low, int high)
    {
        //Merge only if high is higher than low
        if (low < high)
        {
            //Get mid point
            int mid = (high + low) / 2;

            //Recusive calls
            MergeSortByID(low, mid);
            MergeSortByID(mid + 1, high);


            //Start merge

            //Get sizes of left and right list
            int leftSize = mid - low + 1;
            int rightSize = high - mid;

            //Create left and right lists
            List<InventoryItem> left = new List<InventoryItem>();
            List<InventoryItem> right = new List<InventoryItem>();

            for (int i = 0; i < leftSize; i++)
                left.Add(inventory[i + low]);

            for (int j = 0; j < rightSize; j++)
                right.Add(inventory[mid + 1 + j]);

            //index for inventory, left, and right
            int l = 0;
            int r = 0;
            int k = low;

            //Loop through while left and right lists have values
            while(l < leftSize && r < rightSize)
            {
                if (left[l].GetID() <= right[r].GetID())
                {
                    inventory[k] = left[l];
                    l++;
                }
                else
                {
                    inventory[k] = right[r];
                    r++;
                }
                k++;
            }

            //Copy any remaining values
            while(l < leftSize)
            {
                inventory[k] = left[l];
                l++;
                k++;
            }

            //Copy any remaining values
            while(r < rightSize)
            {
                inventory[k] = right[r];
                k++;
                r++;
            }
        }
    }

    public int Partition(int first, int last)
    {
        //Get currnet pivot positions
        InventoryItem pivot = inventory[last];
        int smaller = (first - 1);

        //Loop through and swap items
        for (int element = first; element < last; element++)
        {
            if (inventory[element].GetValue() < pivot.GetValue())
            {
                smaller++;

                InventoryItem temporary = inventory[smaller];
                inventory[smaller] = inventory[element];
                inventory[element] = temporary;
            }
        }

        //Swap items
        InventoryItem temporaryNext = inventory[smaller + 1];
        inventory[smaller + 1] = inventory[last];
        inventory[last] = temporaryNext;

        return smaller + 1;

    }

    public void QuickSortByValue(int first, int last)
    {
        //Sort until through list
        if (first < last)
        {
            int pivot = Partition(first, last);

            QuickSortByValue(first, pivot - 1);
            QuickSortByValue(pivot + 1, last);

        }
    }
}