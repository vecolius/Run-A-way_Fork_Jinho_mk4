using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CharacterRay : MonoBehaviour
{
    public RAYFLAGS flags;
    RaycastHit hitItem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetCharacter();
    }
    public void SetCharacter()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                
            }
        }


    }


}
