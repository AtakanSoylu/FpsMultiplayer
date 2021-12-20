using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiFps.Network
{
    public class MenuController : MonoBehaviour
    {
        public string _menuName;
        public bool _open;
        public void Open() 
        {
            _open = true;
            gameObject.SetActive(true);
        }
        public void Close() 
        {
            _open = false;
            gameObject.SetActive(false);
        }
    }
}