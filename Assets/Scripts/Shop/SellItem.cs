using Game.Inventories;
using System;
using UnityEngine;

namespace Game.Shops
{
    public class SellItem : MonoBehaviour
    {

        [SerializeField] private string _shopName;
        [Range(0f, 100)]
        [SerializeField] private float _sellingPercentage = 80f;
        private Purse _purse;

        public event Action onChange;

        private void Start()
        {
            _purse = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Purse>();
        }

        public void Sell(Item item)
        {

            float sellingPrice = item.GetPrice() * _sellingPercentage / 100f;
            _purse.UpdateBalance(sellingPrice);

            if (onChange != null)
            {
                onChange();
            }
        }
    }
}
