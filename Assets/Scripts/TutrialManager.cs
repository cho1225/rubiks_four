using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutrialManager : MonoBehaviour
{
    // �`���[�g���A���\���p�̃p�l��
    [SerializeField] GameObject tutrialPanel;

    // �`���[�g���A����\�����邩�𐧌䂷��
    public void SetTutrial()
    {
        tutrialPanel.SetActive(!tutrialPanel.activeSelf);
    }
}
