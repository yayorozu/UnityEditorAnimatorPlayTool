# UnityEditorAnimatorPlayTool

UnityEditor で Animator の確認をより簡単に行うためのツールです。  
従来、Project Window から Animator を選択して State を確認し、さらに State の AnimationClip を選択して AnimationClip Window から再生するという手順が必要でしたが、このツールでは State 一覧を表示し、各種操作ボタンを提供することで作業を大幅に効率化します。

   <img src="https://cdn-ak.f.st-hatena.com/images/fotolife/h/hacchi_man/20201207/20201207235928.png" width="500" alt="AnimatorPlayTool ウィンドウ">
---

## 使い方

1. **ツールの起動**  
   Unity エディタのメニューから `Tools/AnimatorPlayTool` を選択すると、専用ウィンドウが開きます。

2. **対象オブジェクトの選択**  
   Hierarchy から Animator コンポーネントを持つオブジェクトを選択すると、ウィンドウ上にそのオブジェクトに関連付けられた State 一覧と各種操作ボタンが表示されます。

3. **各操作ボタンの機能**

   - **Play:**  
     確認したい State の AnimationClip を再生します。

   - **Select AnimationClip:**  
     該当 State の AnimationClip を Project Window で選択状態にします。

   - **Focus Animator Window:**  
     Animator Window にフォーカスを移し、詳細なアニメーションの確認を行えるようにします。

   - **Select Hierarchy Object:**  
     現在対象としているオブジェクトを Hierarchy で選択状態にします。

   - **Stop Animation:**  
     再生中の Animation を停止します。

   <img src="https://cdn-ak.f.st-hatena.com/images/fotolife/h/hacchi_man/20201207/20201207235155.png" width="500" alt="State 一覧と操作ボタン">
