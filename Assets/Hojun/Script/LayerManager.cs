using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hojun
{

    public class LayerManager : DontDestroySingle<LayerManager>
    {

        public enum CustomLayerValue
        {
            GROUND = 1 << 10,

        }

        // bullet 같은 obj가 맞고 사라질 레이어 단.
        public LayerMask Nature
        {
            get => nature;
        }
        LayerMask nature;

        private new void Awake()
        {
            base.Awake();
            AddNatureLayer(CustomLayerValue.GROUND);
        }

        public void AddNatureLayer(CustomLayerValue layerValue)
        {
            nature += (int)layerValue;
        }

    }
}