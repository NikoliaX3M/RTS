using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISquadPositionGenerator
{
    Vector3[] GetPosition(int count);
}
