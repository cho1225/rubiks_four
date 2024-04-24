// キューブを追加するパネルを制御
using UnityEngine;

public class Panel : MonoBehaviour
{
    // マウスオーバー時の色
    private Color mouseOverColor = Color.gray;
    // 元の色
    private Color originalColor;
    // ゲームオブジェクトのRenderer
    private MeshRenderer meshRenderer;
    // パネルのBoxCollider
    private BoxCollider boxCollider;
    // パネルのx座標
    private float x;
    // パネルのz座標
    private float z;
    // パネルが押されたかどうか
    private bool push;

    //------------各プロパティ

    public float X { get { return x; } }

    public float Z { get { return z; } }

    public bool Push 
    { 
        get { return push; } 
        set { push = value; }
    }

    //------------

    // Panelの初期化
    public void InitializePanel()
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

    // パネルのコライダーが有効かどうかを設定
    public void SetEnabledPanel(bool _bool) { boxCollider.enabled = _bool; }

    // パネルをもとの状態に戻す
    public void ResetPanel()
    {
        Push = false;
        meshRenderer.material.color = originalColor;

    }

    // パネルがクリックされたときの処理
    public void OnClick() { Push = true; }

    // マウスオーバー時の処理
    void OnMouseOver() { meshRenderer.material.color = mouseOverColor; }
    // マウスオーバーではない時の処理
    void OnMouseExit() { meshRenderer.material.color = originalColor; }
}
