using System;
using UnityEngine;
using UnityEngine.Events;

public class BuildingGrid : MonoBehaviour
{
    private static (Building, BuildingPresenterOnButton) _flyingBuilding;

    private Vector2Int _gridSize = new (1000, 1000);
    private Plane _groundPlane = new (Vector3.up, Vector3.zero);

    public event UnityAction<BuildingProfile> CreateBuilding;
    public event UnityAction<BuildingProfile> DestroyBuilding;

    public void StartPlacingBuilding(BuildingPresenterOnButton button)
    {
        if (_flyingBuilding.Item1 != null)
        {
            DestroyBuilding?.Invoke(_flyingBuilding.Item2.Profile);
            Destroy(_flyingBuilding.Item1.gameObject);
        }

        if (button.Profile.Price < WalletLogic.AmountMoney)
        {
            _flyingBuilding = (Instantiate(button.Profile.BuildingWive), button);
            CreateBuilding?.Invoke(button.Profile);
        }
    }

    private void Update()
    {
        if (_flyingBuilding.Item1 != null)
        {            
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (_groundPlane.Raycast(ray, out float position))
            {
                MoveBuilding(ray, position, out int x, out int y);                

                if (Input.GetMouseButtonDown(1))
                    PlaceBuilding(CheckInsideBoundary(x, y));
            }
        }
    }

    private void MoveBuilding(Ray ray, float position, out int x, out int y)

    {
        Vector3 worldPosition = ray.GetPoint(position);

        Action<float> rotateBuilding = x => _flyingBuilding.Item1.transform.Rotate(new Vector3(0, x));
        float angel = 90;

        x = Mathf.RoundToInt(worldPosition.x);
        y = Mathf.RoundToInt(worldPosition.z);
        _flyingBuilding.Item1.transform.position = new Vector3(x, 0, y);

        if (Input.GetKeyDown(KeyCode.Z))
            rotateBuilding(-angel);

        if (Input.GetKeyDown(KeyCode.X))
            rotateBuilding(angel);
    }

    private void PlaceBuilding(bool avalableToBuild)
    {
        if (_flyingBuilding.Item1.InContactAnotherBuilding)
            avalableToBuild = false;

        if (avalableToBuild)
        {
            _flyingBuilding.Item1.IsBuilt = true;
            _flyingBuilding.Item1 = null;
        }
    }

    private bool CheckInsideBoundary(int x, int y)
    {
        bool isInsideBoundary = !(x < 0 || x > _gridSize.x - _flyingBuilding.Item1.Size.x);
        if (y < 0 | y > _gridSize.y - _flyingBuilding.Item1.Size.y)
            isInsideBoundary = false;

        return isInsideBoundary;
    }
}
