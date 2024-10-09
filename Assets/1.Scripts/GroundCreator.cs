using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class GroundCreator : MonoBehaviour
{
    [SerializeField] private GameObject _ground;
    [SerializeField] private GameObject _aim;

    [SerializeField] private bool _fixedPos;
    [SerializeField] private bool _setHeightMode;
    [SerializeField] private bool _setRotationMode;
    [SerializeField] private LayerMask _planeLayerMask;

    [SerializeField] private ARSession _arSession;
    [SerializeField] private ARPlaneManager _arPlaner;

    [SerializeField] private Transform _areas;

    private Ray _ray;
    private RaycastHit _hit;

    [SerializeField] private GameObject _setBtn;
    [SerializeField] private GameObject _btnContainer;
    [SerializeField] private GameObject _acceptBtn;

    private float _lastXValue = float.MinValue;
    private float _curXValue;

    private float _lastYValue = float.MinValue;
    private float _curYValue;

    private void Start()
    {
        InitGround();

        _setBtn.SetActive(true);
        _btnContainer.SetActive(false);

        RegistGroundPoses();
    }

    private void RegistGroundPoses()
    {
        for (int i = 0; i < (int)E_GroundPos.Size; i++)
        {
            Manager.Instance.Data.GroundPoese[i] = _areas.GetChild(i).position;
        }
    }

    private void InitGround()
    {
        _ground.transform.localScale = Manager.Instance.Data.GroundSize;
        _aim.transform.localScale = Manager.Instance.Data.GroundSize * 0.1f;

        _ground.SetActive(false);
        _aim.SetActive(true);
    }

    public void SetPos()
    {
        _fixedPos = true;
        _ground.transform.position = _aim.transform.position;
        _ground.transform.up = _aim.transform.up;
        _aim.SetActive(false);
        _ground.SetActive(true);
        _setBtn.SetActive(false);
        _btnContainer.SetActive(true);

        _arPlaner.requestedDetectionMode = UnityEngine.XR.ARSubsystems.PlaneDetectionMode.None;

        _arPlaner.SetTrackablesActive(false);
    }

    public void ResetPos()
    {
        RemoveDectectPlanes();

        _arPlaner.requestedDetectionMode = UnityEngine.XR.ARSubsystems.PlaneDetectionMode.Horizontal;

        _btnContainer.SetActive(false);
        _setBtn.SetActive(true);

        _fixedPos = false;
        _aim.SetActive(true);

        _ground.SetActive(false);
    }

    private void Update()
    {
        if (!_fixedPos)
        {
            _ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            if(Physics.Raycast(_ray,out _hit,1000f,_planeLayerMask))
            {
                if (!_aim.activeSelf)
                    _aim.SetActive(true);

                _aim.transform.position = _hit.point;

                _aim.transform.up = _hit.normal;
            }
            else
            {
                if(_aim.activeSelf)
                    _aim.SetActive(false);
            }
        }
        else if(_setHeightMode)
        {
            _curXValue = Camera.main.transform.localEulerAngles.x;

            float delta = _lastXValue - _curXValue;

            _ground.transform.position =
                new Vector3(_ground.transform.position.x,
                _ground.transform.position.y + (delta * 0.001f),
                _ground.transform.position.z);
        }
        else if(_setRotationMode)
        {
            _curYValue = Camera.main.transform.localEulerAngles.y;

            float delta = _lastYValue - _curYValue;

                delta = _lastYValue - _curYValue;

            _ground.transform.localRotation = Quaternion.Euler(new Vector3(
                _ground.transform.rotation.x,
                _ground.transform.rotation.y + delta,
                _ground.transform.rotation.z));
        }
    }

    private void RemoveDectectPlanes()
    {
        _arSession.Reset();
    }

    public void SetHeight()
    {
        _lastXValue = Camera.main.transform.localEulerAngles.x;

        _setHeightMode = true;
        _btnContainer.SetActive(false);
        _acceptBtn.SetActive(true);
    }

    public void SetRotation()
    {
        _lastYValue = Camera.main.transform.localEulerAngles.y;

        _setRotationMode = true;
        _btnContainer.SetActive(false);
        _acceptBtn.SetActive(true);
    }

    public void AcceptFunction()
    {
        _acceptBtn.SetActive(false);
        _setHeightMode = false;
        _setRotationMode = false;
        _aim.transform.position = _ground.transform.position;

        _btnContainer.SetActive(true);
    }
}
