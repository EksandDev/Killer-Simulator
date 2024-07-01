using UnityEngine;

public class CameraLinker : MonoBehaviour
{
    [SerializeField] private GameObject _objectForLink;
    [SerializeField] private float _offset;

    private void FixedUpdate()
    {
        Vector3 newPosition = new Vector3(_objectForLink.transform.position.x, transform.position.y,
            _objectForLink.transform.position.z - _offset);
        transform.position = newPosition;
    }
}
