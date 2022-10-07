using UnityEngine;

namespace ProjectE.InputScope
{
    public static class Inputs
    {
		public static int GetTouchCount()
		{
			return UnityEngine.Input.touchCount;
		}

		public static void GetTouch(int index, out Vector2 position, out TouchPhase phase)
		{
			var touch = UnityEngine.Input.GetTouch(index);

			position = touch.position;
			phase = touch.phase;
		}

		public static void GetTouch(int index, out Vector2 position)
		{
			var touch = UnityEngine.Input.GetTouch(index);

			position = touch.position;
		}

		public static Touch GetTouch(int index)
		{
			return UnityEngine.Input.GetTouch(index);
		}
	}
}