using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshObstacle))]
public class Building : MonoBehaviour
{
    [SerializeField] private GameObject _goodState;
    [SerializeField] private GameObject _badState;
    [SerializeField] private Vector2Int _size;

    private bool _inContactAnotherBuilding;
    private bool _isBuilt;

    public Vector2Int Size 
    { 
        get
        {
            return _size; 
        } 
    }
    public bool InContactAnotherBuilding
    {
        get
        {
            return _inContactAnotherBuilding;
        }
    }
    public bool IsBuilt 
    { 
        get
        {
            return _isBuilt;
        } 
        set
        {
            _isBuilt = value;

            if (_isBuilt == true)
            {
                SetNormalColor();
                gameObject.GetComponent<NavMeshObstacle>().enabled = true;
            }
        }
    }

    public void SwitchAvalableColor(bool avalable)
    {
        _goodState.SetActive(avalable);
        _badState.SetActive(!avalable);
    }

    private void SetNormalColor()
    {
        _goodState.SetActive(false);
        _badState.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < _size.x; x++)
        {
            for (int y = 0; y < _size.y; y++)
            {
                Gizmos.color = new Color(0.88f, 0f, 1f, 0.3f);
                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1, .1f, 1));
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.GetComponent<Building>() || other.gameObject.GetComponent<Unit>()) && _isBuilt == false)
        {
            SwitchAvalableColor(false);
            _inContactAnotherBuilding = true;
        }
    }

    private void OnTriggerExit()
    {
        _inContactAnotherBuilding = false;
    }
}
