using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private Renderer _mainRenderer;
    [HideInInspector] public Vector2Int Size = new Vector2Int(2,2);
    private Color _baseColor;
    public bool IsBlossomed { get; private set; } = false;
    [SerializeField] private float _timeBlossomed = 3f;
    private readonly float _timeDestroy = 0.5f;
    private readonly float _speedMove = 10f;
    private readonly float _timeToStep = 0.01f;

    private void Awake()
    {
        _baseColor = _mainRenderer.material.color;
    }

    private void Update()
    {
        if(transform.position.y >10)
        {
            UIController.Singleton.UpdateCountPlant();
            Destroy(gameObject);
        }
    }
   

    private void OnDrawGizmos()
    {
        for (int x = 0; x < Size.x; x++)
        {
            for (int y = 0; y < Size.y; y++)
            {
                if ((x + y) % 2 == 0) Gizmos.color = new Color(0.88f, 0f, 1f, 0.3f);
                else Gizmos.color = new Color(1f, 0.68f, 0f, 0.3f);

                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1, 0.1f, 1));
            }
        }
    }

    public void SetTransparentColor(bool canPutBeb)
    {
        if (canPutBeb)
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

    public void FloweringPlant()
    {
        StartCoroutine(Flowering());
    }

    public void DeletePlant()
    {
        StartCoroutine(MovePlant());
        
    }

    IEnumerator Flowering()
    {
      
        yield return new WaitForSeconds(_timeBlossomed);
        transform.localScale = new Vector3(1, 3, 1);
        IsBlossomed = true;
        
    }
  
    IEnumerator MovePlant()
    {
        while(true)
        {
            transform.position += Vector3.up * Time.deltaTime * _speedMove;
            yield return new WaitForSeconds(_timeToStep);
        }
       
    }

}
