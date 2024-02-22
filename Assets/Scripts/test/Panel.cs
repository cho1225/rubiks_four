using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    private Color MouseOverColor = Color.gray;      // マウスオーバー時の色
    private Color OriginalColor;                    // 元の色
    private MeshRenderer Renderer;                  // ゲームオブジェクトのRenderer

    private float x;
    private float z;
    private bool push;

    void Start()
    {
        // パネルの情報をセット
        x = this.transform.position.x;
        z = this.transform.position.z;
        push = false;

        // Rendererコンポーネントをとってくる
        Renderer = GetComponent<MeshRenderer>();
        // オリジナルの色をとってくる
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
        // マウスオーバー時に色を変える
        Renderer.material.color = MouseOverColor;
    }

    void OnMouseExit()
    {
        // オリジナルの色に戻す
        Renderer.material.color = OriginalColor;
    }
}
