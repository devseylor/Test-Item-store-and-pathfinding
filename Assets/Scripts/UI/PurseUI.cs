using Game.Inventories;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class PurseUI : MonoBehaviour
    {
        [SerializeField] private Text _balanceFild;

        Purse _playerPurse = null;

        private void Start()
        {
            _playerPurse = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Purse>();

            if(_playerPurse != null)
            {
                _playerPurse.onChange += RefreshUI;
            }
            RefreshUI();
        }

        private void RefreshUI()
        {
            _balanceFild.text = $"${_playerPurse.GetBalance()}";
        }
    }
}