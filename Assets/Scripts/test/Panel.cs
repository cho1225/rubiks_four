using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    private Color MouseOverColor = Color.gray;      // �}�E�X�I�[�o�[���̐F
    private Color OriginalColor;                    // ���̐F
    private MeshRenderer Renderer;                  // �Q�[���I�u�W�F�N�g��Renderer

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
        Renderer = GetComponent<MeshRenderer>();
        // �I���W�i���̐F���Ƃ��Ă���
        OriginalColor = Renderer.material.color;
    }

    public float GetX() { return x; }

    public float GetZ() { return z; }

    public bool GetPush()
    {
        return push;
    }

    public void SetPush(bool _push) { this.push = _push; }

    public void ResetPanel()
    {
        SetPush(false);
        Renderer.material.color = OriginalColor;

    }

    public void OnClick()
    {
        SetPush(true);
    }

    void OnMouseOver()
    {
        // �}�E�X�I�[�o�[���ɐF��ς���
        Renderer.material.color = MouseOverColor;
    }

    void OnMouseExit()
    {
        // �I���W�i���̐F�ɖ߂�
        Renderer.material.color = OriginalColor;
    }
}
