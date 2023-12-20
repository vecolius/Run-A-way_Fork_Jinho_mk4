using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaeyoung
{
    public class MyCharacterUI : MonoBehaviour
    {
        private Jinho.Player pl;
        [SerializeField] GameObject hpBar;


        public Jinho.Player Pl
        {
            get
            {
                return Pl; 
            } 
            set
            {
                Pl = value;
                
            }
        }

        private void UIUpdate()
        {
            // UI¿¬°á
        }
    }
}