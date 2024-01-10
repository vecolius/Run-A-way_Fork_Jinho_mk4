using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCam : MonoBehaviour
{
    public GameObject target;
    [SerializeField] private float camDistance = 0;

    private void Start()
    {
        this.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
    }

    private void Update()
    {
        if (target == null)
            return;

        this.transform.position = new Vector3(target.transform.position.x,target.transform.position.y + camDistance, target.transform.position.z);
    }
}
