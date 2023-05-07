using System;
using UnityEngine;

namespace Game.Inventories
{
    public class Purse : MonoBehaviour
    {
        [SerializeField] private float _startingBalance = 400f;

        private float _balance = 0;

        public event Action onChange;

        private void Awake()
        {
            _balance = _startingBalance;
        }

        public float GetBalance()
        {
            return _balance;
        }

        public void UpdateBalance(float amount)
        {
            _balance += amount;
            if(onChange != null)
            {
                onChange();
            }
        }

        public void AddItems(Item item)
        {
           UpdateBalance(item.GetPrice());
        }
    }
}

