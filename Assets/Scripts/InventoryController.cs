using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Inventory.Model;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private UI.UIInventoryPage inventoryUI;

        [SerializeField] private Model.InventorySO inventoryData;

        public List<InventoryItem> initialItems = new List<InventoryItem>();

        private void Start()
        {
            PrepareUI();
            PrepareInventoryData();
            inventoryUI.Hide(); // Start with the inventory hidden
        }

        private void PrepareInventoryData()
        {
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach (InventoryItem item in initialItems)
            {
                if (item.isEmpty) continue;
                inventoryData.AddItem(item);
            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> InventoryState)
        {
            inventoryUI.ResetAllItems();
            foreach (var item in InventoryState)
            {
                inventoryUI.UpdateData
                (
                item.Key,
                item.Value.item.ItemImage,
                item.Value.quantity
                );
            }
        }

        private void PrepareUI()
        {
            inventoryUI.InitializeInventoryUI(inventoryData.Size);
            inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            inventoryUI.OnSwapItems += HandleSwapItems;
            inventoryUI.OnStartDragging += HandleDragging;
            inventoryUI.OnItemActionRequested += HandleItemActionRequest;
        }

        private void HandleItemActionRequest(int itemIndex)
        {

        }

        private void HandleDragging(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.isEmpty) return;
            inventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
        }

        private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
        {
            inventoryData.SwapItems(itemIndex_1, itemIndex_2);
            
        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            Model.InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.isEmpty)
            {
                inventoryUI.ResetSelection();
                return;
            }
            Model.ItemSO item = inventoryItem.item;
            inventoryUI.UpdateDescription(itemIndex, item.Name, item.Description, item.ItemImage);
        }

        public void Update()
        {
            if (Keyboard.current.iKey.wasReleasedThisFrame)
            {
                if (inventoryUI.isActiveAndEnabled == false)
                {
                    inventoryUI.Show();
                    foreach (var item in inventoryData.GetCurrentInventoryState())
                    {
                        inventoryUI.UpdateData
                        (
                        item.Key,
                        item.Value.item.ItemImage,
                        item.Value.quantity
                        );
                    }
                }
                else
                {
                    inventoryUI.Hide();
                }
            }
        }

    }
}
