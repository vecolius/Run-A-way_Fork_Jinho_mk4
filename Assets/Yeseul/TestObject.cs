using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yeseul;

namespace Yeseul
{

    public class TestObject : MonoBehaviour, IInteractive
    {
        GameObject interactiveObj;
     
        public void Interaction(GameObject interactivePlayer)
        {

            if(interactiveObj.TryGetComponent<Player>( out Player player))
            {
                player.Hp -= 10;

                Debug.Log(player + "의 체력 10 깍음");
            }

        }

    }


    public class Door : MonoBehaviour, IInteractive
    {
        public float rotationAngle = 90f; // 회전 각도를 설정합니다.
        [SerializeField] bool rotateDir = false; // 회전 여부를 확인하는 플래그입니다.

        // rotatedir 을 바탕으로 왼쪽 회전 오른쪽 회전 

        public void Interaction(GameObject interactivePlayer)
        {
            if (!rotateDir)
            {
                transform.Rotate(Vector3.left * rotationAngle);
                rotateDir = true; 
            }
            else
            {
                transform.Rotate(Vector3.right * rotationAngle);
                rotateDir = false; 
            }
        }
    }


    public class AmmoSupply : MonoBehaviour, IInteractive
    {
        public void Interaction(GameObject interactivePlayer)
        {
                

        }
    }



}
