using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    // �}�E�X�I�[�o�[���̐F
    private Color mouseOverColor = Color.gray;
    // ���̐F
    private Color originalColor;
    // �Q�[���I�u�W�F�N�g��Renderer
    private MeshRenderer meshRenderer;
    // �p�l����BoxCollider
    private BoxCollider boxCollider;
    // �p�l����x���W
    private float x;
    // �p�l����z���W
    private float z;
    // �p�l���������ꂽ���ǂ���
    private bool push;

    void Start()
    {
        // �p�l���̏����Z�b�g
        x = this.transform.position.x;
        z = this.transform.position.z;
        push = false;

        // Renderer�R���|�[�l���g���Ƃ��Ă���
        meshRenderer = GetComponent<MeshRenderer>();
        // Collider�R���|�[�l���g���Ƃ��Ă���
        boxCollider = GetComponent<BoxCollider>();
        // �I���W�i���̐F���Ƃ��Ă���
        originalColor = meshRenderer.material.color;
    }

    //------------�e�v���p�e�B

    public float X { get { return x; } }

    public float Z { get { return z; } }

    public bool Push 
    { 
        get { return push; } 
        set { push = value; }
    }

    //------------

    // �p�l���̃R���C�_�[���L�����ǂ�����ݒ�
    public void SetEnabledPanel(bool _bool) { boxCollider.enabled = _bool; }

    // �p�l�������Ƃ̏�Ԃɖ߂�
    public void ResetPanel()
    {
        Push = false;
        meshRenderer.material.color = originalColor;

    }

    // �p�l�����N���b�N���ꂽ�Ƃ��̏���
    public void OnClick() { Push = true; }

    // �}�E�X�I�[�o�[���̏���
    void OnMouseOver() { meshRenderer.material.color = mouseOverColor; }
    // �}�E�X�I�[�o�[�ł͂Ȃ����̏���
    void OnMouseExit() { meshRenderer.material.color = originalColor; }
}
