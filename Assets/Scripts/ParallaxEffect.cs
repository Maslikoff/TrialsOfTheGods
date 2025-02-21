using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private Transform[] _layers; 
    [SerializeField] private float[] _parallaxSpeeds;
    [SerializeField] private Transform _cameraTransform; 

    private Vector3 _previousCameraPosition; 

    private void Start()
    {
        _previousCameraPosition = _cameraTransform.position;
    }

    private void Update()
    {
        for (int i = 0; i < _layers.Length; i++)
        {
            float parallax = (_previousCameraPosition.x - _cameraTransform.position.x) * _parallaxSpeeds[i];

            Vector3 layerPosition = _layers[i].position;
            layerPosition.x += parallax;
            _layers[i].position = layerPosition;
        }

        _previousCameraPosition = _cameraTransform.position;
    }
}
