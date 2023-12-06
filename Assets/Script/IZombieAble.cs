using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Hojun 
{


    public interface IHearStrategy
    {
        public GameObject Owner { get;}
        public void Hear();

    }

    public interface IHearable
    {
        public IHearStrategy HearStrategy { get; set; }
        public void Hear();
    }

    public interface IMoveStrategy 
    {
        public GameObject Owner { get;}
        public void Move(GameObject target);
    }

    public interface IMoveAble
    {
        public IMoveStrategy MoveStrategy { get; set; }
        public void Move(GameObject target);
    }

<<<<<<< Updated upstream
=======
    //public interface IHearAble 
    //{
    //    public IHearStrategy HearStrategy { get; set; }

    //    public float GetHearValue(GameObject target);
    //}

    //public interface IHearStrategy
    //{
    //    public float Hear( GameObject target );
    //}

    public interface IZombieAble : IMoveAble
    {
>>>>>>> Stashed changes


}
