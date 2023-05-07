using UnityEngine;

namespace Game.Inventories
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private float _price;

        public float GetPrice()
        {
            return _price;
        }
    }
}
