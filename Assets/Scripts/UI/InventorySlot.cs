using Game.Inventories;
using Game.Shops;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.UI
{
    public class InventorySlot : MonoBehaviour, IDropHandler
    {
        [SerializeField] private BuyItem _buyItem;
        [SerializeField] private SellItem _sellItem;

        public void OnDrop(PointerEventData eventData)
        {
            if (transform.childCount == 0)
            {
                GameObject dropped = eventData.pointerDrag;
                DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();

                if (_sellItem != null)
                {
                    Item item = dropped.GetComponent<Item>();
                    _sellItem.Sell(item);
                }
                else if (_buyItem != null)
                {
                    Item item = dropped.GetComponent<Item>();
                    _buyItem.Buy(item);
                }
                

                draggableItem._parentAfterDrag = transform;
            }
        }
    }
}
