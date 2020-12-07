using System;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Yorozu
{
	public class AnimatorPlayTool : EditorWindow
	{
		[MenuItem("Tools/AnimatorPlayTool")]
		private static void ShowWindow()
		{
			var window = GetWindow<AnimatorPlayTool>("AnimationPlay");
			window.Show();
		}

		[SerializeField]
		private GameObject _object;
		[SerializeField]
		private Animator _animator;

		private DateTime _targetTime = DateTime.Now;

		private void OnEnable()
		{
			Selection.selectionChanged += SelectionChanged;
		}

		private void OnDisable()
		{
			Selection.selectionChanged -= SelectionChanged;
		}

		private void SelectionChanged()
		{
			var obj = Selection.activeGameObject;
			if (obj == null)
			{
				return;
			}

			_object = obj;
			_animator = obj.GetComponent<Animator>();
			Repaint();
		}

		private void OnGUI()
		{
			if (_animator == null)
			{
				EditorGUILayout.HelpBox("Select Animator Object", MessageType.Info);

				return;
			}

			using (new EditorGUI.DisabledScope(true))
			{
				EditorGUILayout.ObjectField("Animator", _animator, typeof(AnimatorController));
			}

			if (GUILayout.Button("Focus Animator Window"))
			{
				var type = typeof(UnityEditor.Graphs.Graph).Assembly.GetType(
					"UnityEditor.Graphs.AnimatorControllerTool");
				var findView = Resources.FindObjectsOfTypeAll(type);
				var window = findView.Length <= 0
					? GetWindow(typeof(SceneView))
					: findView[0] as EditorWindow;
				window.Focus();
			}

			if (GUILayout.Button("Select Hierarchy Object"))
			{
				Selection.activeGameObject = _object;
				EditorGUIUtility.PingObject(_object);
			}

			if (GUILayout.Button("Stop Animation"))
			{
				EditorApplication.update -= UpdateAnimation;
			}

			var ac = _animator.runtimeAnimatorController as AnimatorController;
			var layerIndex = 0;
			foreach (var layer in ac.layers)
			{
				EditorGUILayout.LabelField(layer.name, EditorStyles.boldLabel);
				using (new EditorGUI.IndentLevelScope())
				{
					foreach (var state in layer.stateMachine.states)
					{
						using (new EditorGUILayout.HorizontalScope())
						{
							EditorGUILayout.LabelField(state.state.name);
							if (GUILayout.Button("Play"))
							{
								var animationClip = state.state.motion as AnimationClip;
								PlayAnimation(state.state.name, layerIndex, animationClip.length);
							}

							if (GUILayout.Button("Select AnimationClip"))
							{
								Selection.activeObject = state.state.motion;
							}
						}
					}
				}

				layerIndex++;
			}
		}

		public void PlayAnimation(string name, int layer, float length)
		{
			if (_animator == null)
				return;

			_animator.Play(name, layer);
			_animator.speed = 1f;
			_targetTime = DateTime.Now.AddSeconds(length);

			EditorApplication.update -= UpdateAnimation;
			EditorApplication.update += UpdateAnimation;
		}

		private void UpdateAnimation()
		{
			if (_targetTime < DateTime.Now)
			{
				EditorApplication.update -= UpdateAnimation;

				return;
			}

			_animator.Update(Time.deltaTime);
		}
	}
}
