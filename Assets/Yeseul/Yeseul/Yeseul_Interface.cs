using UnityEngine;
using Photon.Pun;

namespace Yeseul
{


    public interface IInteractive
    {
        [PunRPC]
        void Interaction(GameObject interactivePlayer);
    }


}
