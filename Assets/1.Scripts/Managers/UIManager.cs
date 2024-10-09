using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class UIManager
{
    [SerializeField] public Button _setGroundBtn;
    [SerializeField] public GameObject _BtnContainer;
    [SerializeField] public Button _AcceptBtn;
    [SerializeField] public Button _ResetGroundBtn;
    [SerializeField] public Button _GameStartBtn;
    [SerializeField] public Button _SetHeightBtn;
    [SerializeField] public Button _SetRotationBtn;

    public void AddBtnEvnet(Button m_btn,UnityAction m_action)
    {
        m_btn.onClick.AddListener(m_action);
    }

    public void RemoveBtnEvent(Button m_btn,UnityAction m_action)
    {
        m_btn.onClick.RemoveListener(m_action);
    }

    public void Init()
    {
        AddBtnEvnet(_setGroundBtn,SetGroundBtnAction);
        AddBtnEvnet(_ResetGroundBtn, ResetGroundBtnAction);
        AddBtnEvnet(_AcceptBtn, AcceptBtnAction);
        AddBtnEvnet(_GameStartBtn, GameStartBtnAction);
        AddBtnEvnet(_SetHeightBtn, SetHeightBtnAction);
        AddBtnEvnet(_SetRotationBtn, SetRotationBtnAction);

        _setGroundBtn.gameObject.SetActive(true);
    }

    private void SetGroundBtnAction()
    {
        _setGroundBtn.gameObject.SetActive(false);
        _BtnContainer.SetActive(true);
    }

    private void ResetGroundBtnAction()
    {
        _BtnContainer.SetActive(false);
        _setGroundBtn.gameObject.SetActive(true);
    }

    private void GameStartBtnAction()
    {
        _BtnContainer.SetActive(false);
        Manager.Instance.Data.SetPlayMode(true);
        // 캐릭터 스폰
    }

    private void SetHeightBtnAction()
    {
        _BtnContainer.SetActive(false);
        _AcceptBtn.gameObject.SetActive(true);
    }

    private void SetRotationBtnAction()
    {
        _BtnContainer.SetActive(false);
        _AcceptBtn.gameObject.SetActive(true);
    }

    private void AcceptBtnAction()
    {
        _BtnContainer.SetActive(true);
        _AcceptBtn.gameObject.SetActive(false);
    }


}
