using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraResizer : MonoBehaviour
{
    private float _startSize;
    [SerializeField]
    private float _targetSize;
    private float _resizeProgress;
    private Camera _cameraComponent;


    public float ResizeTime = 3f;
    

    public float TargetSize
    {
        get { return _targetSize; }
        set
        {
            _startSize = _cameraComponent.orthographicSize;
            _resizeProgress = 0;
            _targetSize = value;
        }
    }

    void Awake()
    {
        _cameraComponent = GetComponent<Camera>();
        _resizeProgress = 1;
    }



    void Update()
    {
        if(TargetSize < 1) return;
        _resizeProgress += Time.deltaTime / ResizeTime;
        _cameraComponent.orthographicSize = Mathf.Lerp(_startSize, TargetSize, _resizeProgress);
    }
}