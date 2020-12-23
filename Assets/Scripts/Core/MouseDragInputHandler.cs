using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace Core
{
	public class MouseDragInputHandler : MonoBehaviour
	{
		private MouseButton _trackedButton;
		
		private Func<bool> _onDragBegin;
		private Action _onDrag;
		private Action _onDragEnd;

		public void Setup(MouseButton trackedButton, Func<bool> onDragBegin, Action onDrag, Action onDragEnd)
		{
			_trackedButton = trackedButton;
			
			_onDragBegin = onDragBegin;
			_onDrag = onDrag;
			_onDragEnd = onDragEnd;
		}

		private void OnEnable()
		{
			StartCoroutine(WaitForMouseDrag());
		}

		private IEnumerator WaitForMouseDrag()
		{
			var mouseButtonDown = false;

			while (true)
			{
				if (Input.GetMouseButtonDown((int) _trackedButton) && !mouseButtonDown)
				{
					mouseButtonDown = true;
					if (_onDragBegin?.Invoke() == false)
						mouseButtonDown = false;
				}
				else if (Input.GetMouseButtonUp((int) _trackedButton))
				{
					mouseButtonDown = false;
					_onDragEnd?.Invoke();
				}
				else if (mouseButtonDown)
				{
					_onDrag?.Invoke();
				}

				yield return null;
			}
		}
	}
}