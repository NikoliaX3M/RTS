using UnityEngine;

public class CameraMovment : MonoBehaviour
{
    [SerializeField] private float _normalSpeed;
    [SerializeField] private float _fastSpeed;
    [SerializeField] private Transform _mapZone;
 
    void Update()
    {
        StayInsideBoundary();
       
        MoveCamera();    
    }


    private void MoveCamera()
    {
        float moveSpeed = Input.GetKey(KeyCode.LeftShift) ? _fastSpeed : _normalSpeed;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new (moveHorizontal, 0.0f, moveVertical);

        gameObject.transform.position += movement * moveSpeed;
    }
    private void StayInsideBoundary()
    {
        if (transform.position.x < 0)
            transform.position += new Vector3(1,0);

        if(transform.position.z < 0)
            transform.position += new Vector3(0, 0, 1);

        if (transform.position.x > 700)
            transform.position += new Vector3(-1, 0);

        if (transform.position.z > 700)
            transform.position += new Vector3(0, 0, -1);
    }
}
