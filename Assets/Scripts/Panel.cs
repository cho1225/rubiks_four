using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    private Color mouseOverColor = Color.gray;      // �}�E�X�I�[�o�[���̐F
    private Color originalColor;                    // ���̐F
    private MeshRenderer meshRenderer;              // �Q�[���I�u�W�F�N�g��Renderer
    private BoxCollider boxCollider;

    private float x;
    private float z;
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

    // �v���p�e�B�͂ǂ������������ׂ��H
    public float X { get { return x; } }

    public float Z { get { return z; } }

    public bool Push 
    { 
        get { return push; } 
        set { push = value; }
    }

    public void SetEnabledPanel(bool _bool) { boxCollider.enabled = _bool; }

    public void ResetPanel()
    {
        Push = false;
        meshRenderer.material.color = originalColor;

    }

    public void OnClick() { Push = true; }

    void OnMouseOver() { meshRenderer.material.color = mouseOverColor; }

    void OnMouseExit() { meshRenderer.material.color = originalColor; }
}
