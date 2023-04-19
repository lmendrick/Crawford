using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Inventory
{
   public event EventHandler OnItemListChanged;
   private List<Item> itemList;
   private Action<Item> useItemAction;

   public Inventory(Action<Item> useItemAction)
   {

      itemList = new List<Item>();
      this.useItemAction = useItemAction;
      
   }

   public void AddItem(Item item)
   {
      if (item.IsStackable())
      {
         bool itemAlreadyInInventory = false;
         foreach (Item inventoryItem in itemList)
         {
            if (inventoryItem.itemType == item.itemType)
            {
               inventoryItem.amount += item.amount;
               itemAlreadyInInventory = true;
            }
            
         }

         if (!itemAlreadyInInventory)
         {
            itemList.Add(item);
         }
      }
      else
      {
         itemList.Add(item);
      }

      OnItemListChanged?.Invoke(this, EventArgs.Empty);
      
   }

   public void UseItem(Item item)
   {
      useItemAction(item);

   }

   public void RemoveItem(Item item)
   {
      if (item.IsStackable())
      {
         Item itemInInventory = null;
         foreach (Item inventoryItem in itemList)
         {
            if (inventoryItem.itemType == item.itemType)
            {
               inventoryItem.amount -= item.amount;
               itemInInventory = inventoryItem;
            }
         }

         if (itemInInventory != null && itemInInventory.amount <= 0)
         {
            itemList.Remove(itemInInventory);
         }
         else
         {
            itemList.Remove(item);
         }


      }
   }

   public List<Item> GetItemList()
   {
      return itemList;
   }
   
   
}
