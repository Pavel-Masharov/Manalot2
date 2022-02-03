using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionPlant : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private LayerMask layerMask;
    private readonly float distance = 10f;

    private void Awake()
    {
        mainCamera = Camera.main;
    }
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, distance, layerMask))
            {
                if(hit.collider.GetComponent<Bed>()._plant != null)
                {
                    if (hit.collider.GetComponent<Bed>()._plant.IsBlossomed)
                    {
                        hit.collider.GetComponent<Bed>()._plant.DeletePlant();
                    }
                }
            }
            else
                return;
        }
       
    }
}
