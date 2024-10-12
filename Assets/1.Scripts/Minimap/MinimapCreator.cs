using UnityEngine;

public class MinimapCreator : MonoBehaviour
{
    private Camera _mainCam;
    [SerializeField] GameObject _miniMap;
    private bool _wasVisible;

    private void Start()
    {
        _mainCam = Camera.main;
    }

    private void Update()
    {
        CheckVisible();
    }

    private void CheckVisible()
    {
        // 0~1 ������ Ȯ��
        Vector3 viewportPoint = _mainCam.WorldToViewportPoint(transform.position);

        Manager.Instance.Data.GroundViewpoint.x = viewportPoint.x;
        Manager.Instance.Data.GroundViewpoint.y = viewportPoint.y;

        bool inView = viewportPoint.x >= 0 && viewportPoint.x <= 1 &&
                        viewportPoint.y >= 0 && viewportPoint.y <= 1 &&
                        viewportPoint.z > 0;

        // �Ⱥ����ٰ� ���̸�
        if (inView && !_wasVisible)
        {
            _miniMap.SetActive(false);
        }
        // �����ٰ� �Ⱥ��̸�
        else if (!inView && _wasVisible)
        {
            _miniMap.SetActive(true);
        }

        _wasVisible = inView;
    }
}
