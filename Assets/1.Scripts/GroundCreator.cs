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
    [SerializeField] private bool _setCullHegihtMode;
    [SerializeField] private LayerMask _planeLayerMask;

    [SerializeField] private ARSession _arSession;
    [SerializeField] private ARPlaneManager _arPlaner;

    [SerializeField] private Transform _areas;
    [SerializeField] private Transform _cullFace;

    private Ray _ray;
    private RaycastHit _hit;

    private float _lastXValue = float.MinValue;
    private float _curXValue;

    private float _lastYValue = float.MinValue;
    private float _curYValue;

    private void Start()
    {
        InitGround();

        Manager.Instance.UI.AddBtnEvnet(Manager.Instance.UI._setGroundBtn, SetPos);
        Manager.Instance.UI.AddBtnEvnet(Manager.Instance.UI._ResetGroundBtn, ResetPos);
        Manager.Instance.UI.AddBtnEvnet(Manager.Instance.UI._SetHeightBtn, SetHeight);
        Manager.Instance.UI.AddBtnEvnet(Manager.Instance.UI._SetRotationBtn, SetRotation);
        Manager.Instance.UI.AddBtnEvnet(Manager.Instance.UI._SetCullHeightBtn, SetCullHeight);
        Manager.Instance.UI.AddBtnEvnet(Manager.Instance.UI._AcceptBtn, () => {
            _setHeightMode = false;
            _setRotationMode = false;
            _setCullHegihtMode = false;
            _aim.transform.position = _ground.transform.position;
        });

        Manager.Instance.UI.AddBtnEvnet(Manager.Instance.UI._GameStartBtn, () => {
            RegistGroundPoses();
            WarningController.SetStandardDir(_ground.transform.forward.normalized);
        });
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
        _ground.transform.localScale *= DataManager.ObjectScaleRate;
        _aim.transform.localScale *= DataManager.ObjectScaleRate;

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

        _arPlaner.requestedDetectionMode = UnityEngine.XR.ARSubsystems.PlaneDetectionMode.None;

        _arPlaner.SetTrackablesActive(false);
    }

    public void ResetPos()
    {
        RemoveDectectPlanes();

        _arPlaner.requestedDetectionMode = UnityEngine.XR.ARSubsystems.PlaneDetectionMode.Horizontal;

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
                _ground.transform.rotation.y + delta * 1.5f,
                _ground.transform.rotation.z));
        }
        else if(_setCullHegihtMode)
        {
            _curXValue = Camera.main.transform.localEulerAngles.x;

            float delta = _lastXValue - _curXValue;

            _cullFace.transform.position =
                new Vector3(_cullFace.transform.position.x,
                _cullFace.transform.position.y + (delta * 0.001f),
                _cullFace.transform.position.z);
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
    }

    public void SetRotation()
    {
        _lastYValue = Camera.main.transform.localEulerAngles.y;

        _setRotationMode = true;
    }

    public void SetCullHeight()
    {
        _lastXValue = Camera.main.transform.localEulerAngles.x;

        _setCullHegihtMode = true;
    }
}
