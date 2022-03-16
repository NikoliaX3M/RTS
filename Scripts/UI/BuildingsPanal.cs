using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingsPanal : MonoBehaviour
{
    [SerializeField] private GameObject _buildingPanalTemplate;
    [SerializeField] private Transform _buttonParant;

    private List<BuildingProfile> _buildings;

    private void Start()
    {
        _buildings = Resources.LoadAll<BuildingProfile>("Buildings/").ToList();

        foreach (var building in _buildings)
        {
            var buttonGo = Instantiate(_buildingPanalTemplate, _buttonParant);
            buttonGo.SetActive(true);
            buttonGo.GetComponent<BuildingPresenterOnButton>().Present(building);             
        }
    }
}
