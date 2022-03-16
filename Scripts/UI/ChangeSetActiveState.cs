using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSetActiveState : MonoBehaviour
{
    [SerializeField] private GameObject _interfaceElement;

    public void ChangeSetActive()
    {
        _interfaceElement.SetActive(!_interfaceElement.activeInHierarchy);
    }
}
