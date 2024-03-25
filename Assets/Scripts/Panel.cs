using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    private Color mouseOverColor = Color.gray;      // マウスオーバー時の色
    private Color originalColor;                    // 元の色
    private MeshRenderer meshRenderer;              // ゲームオブジェクトのRenderer
    private BoxCollider boxCollider;

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
        meshRenderer = GetComponent<MeshRenderer>();
        // Colliderコンポーネントをとってくる
        boxCollider = GetComponent<BoxCollider>();
        // オリジナルの色をとってくる
        originalColor = meshRenderer.material.color;
    }

    // プロパティはどちらも実装するべき？
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
