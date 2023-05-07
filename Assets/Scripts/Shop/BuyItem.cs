using Game.Inventories;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Shops
{
    public class BuyItem : MonoBehaviour
    {
        [SerializeField] private string _shopName;
        private Purse _purse;

        public event Action onChange;


        private void Start()
        {
            _purse = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Purse>();
        }

        public void Buy(Item item)
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
    }
}
