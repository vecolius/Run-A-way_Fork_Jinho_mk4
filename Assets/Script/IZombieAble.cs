using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Hojun 
{

    public interface IMoveStrategy 
    {
        public GameObject Owner { get;}
        public void Move();
    }

    public interface IMoveAble
    {
        public IMoveStrategy MoveStrategy { get; set; }
        public void Move();
    }

    public interface IHearAble
    {

        public void Hear(GameObject soundOwner);
    }



    //public interface IHearStrategy
    //{
    //    public void Hear( GameObject target );

    //}


}
