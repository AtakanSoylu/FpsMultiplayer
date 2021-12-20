using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiFps.Network
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance;
        [SerializeField] private MenuController[] _menuArray;
        private void Awake()
        {
            Instance = this;
        }

        public void OpenMenu(string menuName)
        {
            for (int i = 0; i < _menuArray.Length; i++)
            {
                if(_menuArray[i]._menuName == menuName)
                {
                    _menuArray[i].Open();
                }
                else if (_menuArray[i]._open)
                {
                    CloseMenu(_menuArray[i]);
                }
            }
        }

        public void OpenMenu(MenuController menu)
        {
            for (int i = 0; i < _menuArray.Length; i++)
            {
                if (_menuArray[i]._open)
                {
                    CloseMenu(_menuArray[i]);
                }
            }
            menu.Open();
        }

        public void CloseMenu(MenuController menu)
        {
            menu.Close();
        }
    }
}