using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class BuildingPresenterOnButton : MonoBehaviour
{
    public TextMeshProUGUI BuildingName;
    public Image Icon;
    public BuildingProfile Profile;

    public void Present(BuildingProfile profile)
    {
        Icon.sprite = profile.Icon;
        BuildingName.text = profile.Name;
        Profile = profile;
    }
}
