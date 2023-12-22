using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Jaeyoung
{
    public class MissionUI : MonoBehaviour
    {
        public TextMeshProUGUI title;
        public TextMeshProUGUI content;
        public TextMeshProUGUI completePer;
        public Image gauge;

        public void CompleteUpdate(float value = 0)
        {
            if (value > 1)
                value = 1;

            gauge.transform.localScale = new Vector3(value, 1, 1);
            completePer.text = Mathf.FloorToInt(value * 100).ToString() + " %";
        }
    }
}