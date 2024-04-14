using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutrialManager : MonoBehaviour
{
    // �\�����̉摜
    [SerializeField] private Image tutrialImage;
    // �\������摜�̔z��
    [SerializeField] private Sprite[] tutrialSprites;
    // �\�����̉摜�̃y�[�W�ԍ���\��text
    [SerializeField] private TextMeshProUGUI pageIndexText;
    // ���̃y�[�W�ɐi�߂�{�^��
    [SerializeField] private Button nextButton;
    // �O�̃y�[�W�ɖ߂�{�^��
    [SerializeField] private Button backButton;
    // �\�����̉摜�̃y�[�W�ԍ�
    private int pageIndex;
    // �`���[�g���A���̃y�[�W��
    private int pageIndexMax = 6;
    // �y�[�W�ԍ����ς�������ǂ���
    private bool isPageIndexChange = false;

    // �`���[�g���A���̐ݒ��������
    public void InitializeTutrial()
    {
        pageIndex = 0;
        tutrialImage.sprite = tutrialSprites[0];
        pageIndexText.text = (pageIndex + 1).ToString();
        nextButton.interactable = true;
        backButton.interactable = false;

    }

    // �\�����̉摜�̃y�[�W�ԍ���\��text�Ƀy�[�W�ԍ���ݒ�
    public void UpdateImage()
    {
        // �y�[�W�ԍ����ς�����Ƃ��̂ݎ��s
        if (isPageIndexChange)
        {
            // �{�^�����C���^���N�e�B�u����ݒ�
            if (pageIndex >= pageIndexMax)
            {
                nextButton.interactable = false;
            }
            else if (pageIndex <= 0)
            {
                backButton.interactable = false;
            }
            else
            {
                nextButton.interactable = true;
                backButton.interactable = true;
            }
            // �\�����̉摜�̃y�[�W�ԍ���\��text�Ƀy�[�W�ԍ���ݒ�
            pageIndexText.text = (pageIndex + 1).ToString();
            // �\�����̉摜��ݒ�
            tutrialImage.sprite = tutrialSprites[pageIndex];
            isPageIndexChange = false;
        }
    }

    // �y�[�W�ԍ������ɐi�߂�
    public void NextPage()
    {
        if (pageIndex < pageIndexMax)
        {
            pageIndex++;
            isPageIndexChange = true;
        }
    }

    // �y�[�W�ԍ���O�ɖ߂�
    public void BackPage()
    {
        if (pageIndex > 0)
        {
            pageIndex--;
            isPageIndexChange = true;
        }
    }
}
