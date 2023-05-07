using Game.Inventories;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Shops
{
    public class Shop : MonoBehaviour
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

        public void BuyItem(Item item)
        {
            if (_purse.GetBalance() >= item.GetPrice())
            {
                _purse.UpdateBalance(-item.GetPrice());

                if (onChange != null)
                {
                    onChange();
                }
            }
            else
            {
                Debug.Log("Not enough money to buy item!");
            }
        }

        public void SellItem(Item item)
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
