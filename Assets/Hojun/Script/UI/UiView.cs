using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hojun
{
    public abstract class UiView : MonoBehaviour
    {
        public virtual CharacterUiModel Model
        {
            get
            {
                return model;
            }
            set
            {
                model = value;
            }
        }
        CharacterUiModel model;
        public abstract void ViewData();
    }

}