using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FermersGrid : MonoBehaviour
{
    [SerializeField] private Vector2Int GridSize = new Vector2Int(8,8);
    private Bed[,] _grid; 
    private Bed _newBed;
    private Camera mainCamera;
    private Plant _newPlant;
    private Bed currentBed;
    private readonly int _maxGardenBed = 10;
    private int _currentGardenBed = 0;

    private void Awake()
    {
        _grid = new Bed[GridSize.x, GridSize.y];
        mainCamera = Camera.main;
    }
    void Update()
    {
        BedPlacement();
        BedPlant();
    }

    private void BedPlacement()
    {
        if (_newBed != null)
        {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (groundPlane.Raycast(ray, out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);
                int x = Mathf.RoundToInt(worldPosition.x);
                int y = Mathf.RoundToInt(worldPosition.z);
                bool canPutBeb = true;

                if (_currentGardenBed >= _maxGardenBed) canPutBeb = false;
                if (x < 0 || x > GridSize.x - _newBed.Size.x) canPutBeb = false;
                if (y < 0 || y > GridSize.y - _newBed.Size.y) canPutBeb = false;
                if (canPutBeb && IsPlaceTaken(x, y)) canPutBeb = false;

                _newBed.transform.position = new Vector3(x, 0, y);
                _newBed.SetTransparentColor(canPutBeb);

                if (canPutBeb && Input.GetMouseButtonDown(0))
                {
                    PlaceBed(x, y);
                }
            }
        }
    }
    private void BedPlant()
    {
        if (_newPlant != null)
        {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (groundPlane.Raycast(ray, out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);

                int x = Mathf.RoundToInt(worldPosition.x);
                int y = Mathf.RoundToInt(worldPosition.z);

                bool canPutPlant = true;

                if (x < 0 || x > GridSize.x - _newPlant.Size.x) canPutPlant = false;
                if (y < 0 || y > GridSize.y - _newPlant.Size.y) canPutPlant = false;
                if (canPutPlant && IsPlaceTakenPlant(x, y)) canPutPlant = false;

                _newPlant.transform.position = new Vector3(x, 0, y);
                _newPlant.SetTransparentColor(canPutPlant);

                if (canPutPlant && Input.GetMouseButton(0))
                {
                    PlacePlant(x, y);
                }
            }
        }
    }

    private void PlacePlant(int placeX, int placeY)
    {
        if (_grid[placeX, placeY] != null)
        {
            currentBed = _grid[placeX, placeY];
            _newPlant.transform.position = currentBed.transform.position;
            _newPlant.SetNormalColor();
        }

        if(currentBed._plant == null)
        {
            currentBed._plant = Instantiate(_newPlant, currentBed.transform.position, Quaternion.identity);
            currentBed._plant.FloweringPlant();
        }  
    }

    private bool IsPlaceTakenPlant(int placeX, int placeY)
    {

        if (_grid[placeX, placeY] != null && _grid[placeX, placeY].transform.position == _newPlant.transform.position) return false;

        return true;
    }

    private bool IsPlaceTaken(int placeX, int placeY)
    {
        for (int x = 0; x < _newBed.Size.x; x++)
        {
            for (int y = 0; y < _newBed.Size.y; y++)
            {
                if (_grid[placeX, placeY] != null) return true;
            }
        }
        return false;
    }

    private void PlaceBed(int placeX, int placeY)
    {
        for (int x = 0; x < _newBed.Size.x; x++)
        {
            for (int y = 0; y < _newBed.Size.y; y++)
            {
                _grid[placeX + x, placeY + y] = _newBed;
            }
        }
        _newBed.SetNormalColor();
        _newBed = null;

        _currentGardenBed++;
    }

    public void StartNewBed(Bed bedPrefab)
    { 
        if(_newBed != null)
        {
            Destroy(_newBed.gameObject);
        }
        if(_newPlant != null)
        {
            Destroy(_newPlant.gameObject);
        }
        _newBed = Instantiate(bedPrefab);
    }

    public void StartNewPlant(Plant PlantPrefab)
    {
        if (_newPlant != null)
        {
            Destroy(_newPlant.gameObject);
        }
        if (_newBed != null)
        {
            Destroy(_newBed.gameObject);
        }
        _newPlant = Instantiate(PlantPrefab);
    }
}
