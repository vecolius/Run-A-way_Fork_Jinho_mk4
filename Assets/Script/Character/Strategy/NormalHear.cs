using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Hojun
{
    public class NormalHear : IHearStrategy
    {
        private GameObject owner;
        public GameObject Owner  => owner;



        public NormalHear( GameObject owner ) 
        {
            this.owner = owner;
        }

        public void Hear()
        {



        }
    }
}

