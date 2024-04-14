using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    // �Q�Ƃ���X�N���v�g
    [SerializeField] private TutrialManager tutrialManager;
    // �`���[�g���A���\���p�̃p�l��
    [SerializeField] private GameObject tutrialPanel;

    // �^�C�g���V�[���̃L���[�u����]
    void Update()
    {
        transform.Rotate(new Vector3(-7, -3, 5) * Time.deltaTime);

        if (tutrialPanel.activeSelf)
        {
            tutrialManager.UpdateImage();
        }
    }

    // �`���[�g���A����\�����邩�𐧌䂷��
    public void SetTutrial()
    {
        tutrialPanel.SetActive(!tutrialPanel.activeSelf);
        tutrialManager.InitializeTutrial();
    }
}
