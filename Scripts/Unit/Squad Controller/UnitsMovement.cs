using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsMovement : MonoBehaviour
{
    public List<Unit> SelectedUnits;
    public List<Unit> Units;

    [SerializeField] private Transform _targetPoint;
    [SerializeField] private GestureClick _gestor;

    private ISquadPositionGenerator _generator;

    private void Start()
    {
        _generator = GetComponent<ISquadPositionGenerator>();

        _gestor.OnClick += pos =>
        {            
            if (SelectedUnits.Count > 0)
            {
                _targetPoint.position = pos;
                SetSquadCenter(pos);
            }
        };
    }

    public void SetSquadCenter(Vector3 center)
    {
        Vector3[] position = _generator.GetPosition(SelectedUnits.Count);
        int distanceFromCenter = SelectedUnits.Count - 1;

        for (int i = 0; i < position.Length; i++)
        {
            SelectedUnits[i].MoveUnit(position[i] * distanceFromCenter + center);
        }
    }
}
