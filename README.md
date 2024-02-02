# rubiks_four
## 概要
・3×3×3のフィールドで行われる立体の〇×ゲーム。  
・盤面に追加するキューブは重力によって下に落ちる。  
・赤色（Player1）と青色（Player2）で戦い、最初に3つそろえたプレイヤーの勝ち。  
・プレイヤーは1ターンの間に以下の行動ができる。  
①盤面に自分のキューブを1つ追加する。  
②3×3×3の盤面を4方向のいずれかに90度回転できる。  
・回転後もプレイヤーたちが盤面に追加したキューブは重力によって下に落ちる。

## なぜこれを作ったか
・3Dのゲームを作った経験が少なかったため、経験を積みたかったから。  
・世界一難しいパズルゲームを作ったら面白いのではないかと考えたから。  
・既存の〇×ゲームを作るだけではつまらないと感じたから。

## アピールポイント
・〇×ゲームを立体にしたもの、または、類似したゲームで上下さかさまにすることができるものは存在したため、90度回転を取り入れることで差別化を図ったこと。

## 力を入れた技術的な要素
・Rigidbodyを用いた物理演算ではうまくいかなかったため、positionを操作して厳密にオブジェクトの挙動を管理している点。

## 今後実装したいこと
・制限時間を追加する  
・タイトルなどのUIの作成
・ステージをいろいろ用意する  
・ルービックキューブのような回転を追加する  
・四目ならべモードを追加する  
・AI対戦モードを追加する  
・ネット対戦モードを追加する  

## フィードバック 
・~~回転するモーションがないとみにくい~~（修正済み）  
・~~回転後キューブが落ちてこないバグがある~~（修正済み）  
・~~回転の仕方によっては回転の意味がなくなる（右上→左下など）~~（修正済み）  
・長考されるとテンポが悪い  
・ボタンの文字がガビガビ
