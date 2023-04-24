using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Puzzle_Inventory: MonoBehaviour
{
  public Inventory inventory;
  void Start()
  {
    inventory.AddItem(new Item { itemType = Item.ItemType.PuzzlePart1, amount =1});
    inventory.AddItem(new Item { itemType = Item.ItemType.PuzzlePart2, amount =1});
    inventory.AddItem(new Item { itemType = Item.ItemType.PuzzlePart3, amount =1});
  }
   
}
