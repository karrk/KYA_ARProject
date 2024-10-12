using System.Collections;
using UnityEngine;

public class CullingFaceController : MonoBehaviour
{
    [SerializeField] private GameObject _fakeFace;
    [SerializeField] private RectTransform _textTr;
    private Coroutine _textRotRoutine;
    private Vector3 _tempVec;

    private void Start()
    {
        Manager.Instance.UI.AddBtnEvnet(Manager.Instance.UI._SetCullHeightBtn, () =>
        {
            _fakeFace.SetActive(true);
            _textRotRoutine = StartCoroutine(FollowCamText());
        });

        Manager.Instance.UI.AddBtnEvnet(Manager.Instance.UI._AcceptBtn, () =>
        {
            _fakeFace.SetActive(false);

            if(_textRotRoutine != null)
                StopCoroutine(_textRotRoutine);
        });
    }

    private IEnumerator FollowCamText()
    {
        while (true)
        {
            _textTr.LookAt(Camera.main.transform);
            _textTr.localEulerAngles = new Vector3(0, 0, -1 * _textTr.localEulerAngles.z);

            yield return null;
        }
    }
}
