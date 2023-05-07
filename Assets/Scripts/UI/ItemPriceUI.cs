using Game.Inventories;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI 
{
    public class ItemPriceUI : MonoBehaviour
    {
        [SerializeField] private Text _itemPriceFild;

        private Item _item;

        private void Start()
        {
            _item = GetComponent<Item>();

            _itemPriceFild.text = $"${_item.GetPrice()}";
        }
    }
}

