using UnityEngine;
using System.Collections;

namespace GameObjectExtension {
	public static class GameObjectExtension {
		public static T AddComponentIfMissing<T>(this GameObject obj)where T : Component{
			T comp= obj.GetComponent<T>();
//			Debug.Log(typeof(T) + "" + comp);
			if(comp == null)
				comp = obj.AddComponent<T>();
//			Debug.Log(comp);
			return comp;
		}
		

	}
}