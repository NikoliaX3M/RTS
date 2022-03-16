using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Builing/Profile")]
public class BuildingProfile : ScriptableObject
{
    public Building BuildingWive;
    public string Name;
    public Sprite Icon;
    public int Price;
}
