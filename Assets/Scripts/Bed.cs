using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    [SerializeField] private Renderer _mainRenderer;
    [HideInInspector] public Vector2Int Size = new Vector2Int(2, 2);
    private Color _baseColor;
    [HideInInspector] public Plant _plant;


    private void Awake()
    {
        _baseColor = _mainRenderer.material.color;
    }

    public void SetTransparentColor(bool canPutBeb)
    {
        if(canPutBeb)
        {
            _mainRenderer.material.color = Color.green;
        }
        else
        {
            _mainRenderer.material.color = Color.red;
        }  
    }

    public void SetNormalColor()
    {
        _mainRenderer.material.color = _baseColor;
    }
    private void OnDrawGizmos()
    {
        for (int x = 0; x < Size.x; x++)
        {
            for (int y = 0; y < Size.y; y++)
            {
                if ((x+y) % 2 == 0) Gizmos.color = new Color(0.88f, 0f, 1f, 0.3f);
                else Gizmos.color = new Color(1f, 0.68f, 0f, 0.3f);

                Gizmos.DrawCube(transform.position + new Vector3(x,0,y), new Vector3(1,0.1f,1));
            }
        }
    }


}
