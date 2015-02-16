using UnityEngine;
using System.Collections;

namespace helper {
	public class instantiate {
		public static GameObject position(GameObject original, Vector3 pos){
			return _(original, pos);
		}
		
		public static GameObject parent(GameObject original, Transform parent_target){
			GameObject result = _(original);
			result.transform.parent = parent_target;
			return result;
		}
		
		public static GameObject parent(GameObject original, GameObject parent_target){
			return parent(original, parent_target.transform);
		}
		
		public static GameObject parent(GameObject original, Transform parent_target, bool reset_pos){
			GameObject result = parent(original, parent_target);
			if (reset_pos){
				result.transform.localPosition = Vector3.zero;
			}
			return result;
		}
		
		public static GameObject parent(GameObject original, GameObject parent_target, bool reset_pos){
			return parent(original, parent_target.transform, reset_pos);
		}
		
		public static T parent<T>(T original, Transform parent) where T : MonoBehaviour{
			T result = _<T>(original);
			result.transform.parent = parent;
			return result;
		}
		
		public static T parent<T>(T original, GameObject parent_target) where T : MonoBehaviour{
			return parent<T>(original, parent_target.transform);
		}
		
		public static T parent<T>(T original, Transform parent_target, bool reset_pos) where T : MonoBehaviour{
			T result = parent<T>(original, parent_target);
			if (reset_pos){
				result.transform.localPosition = Vector3.zero;
			}
			return result;
		}
		
		public static T parent<T>(T original, GameObject parent_target, bool reset_pos) where T : MonoBehaviour{
			return parent<T>(original, parent_target.transform, reset_pos);
		}
		
		public static T _<T>(T obj) where T : MonoBehaviour{
			return MonoBehaviour.Instantiate(obj) as T;
		}
		
		public static T _<T>(T obj, Vector3 pos) where T : MonoBehaviour{
			T result = _(obj);
			result.transform.position = pos;
			return result;
		}
		
		public static T _<T>(T obj, Vector3 pos, Quaternion rot) where T : MonoBehaviour{
			T result = _(obj, pos);
			result.transform.rotation = rot;
			return result;
		}
		
		public static GameObject _(GameObject obj){
			return MonoBehaviour.Instantiate(obj) as GameObject;
		}
		
		public static GameObject _(GameObject obj, Vector3 pos){
			GameObject result = _(obj);
			result.transform.position = pos;
			return result;
		}
		
		public static GameObject _(GameObject obj, Vector3 pos, Quaternion rot){
			GameObject result = _(obj, pos);
			result.transform.rotation = rot;
			return result;
		}
		
		public static class inactive{
			
			public static GameObject position(GameObject original, Vector3 pos){
				return _(original, pos);
			}
			
			public static T parent<T>(T original, Transform parent) where T : MonoBehaviour{
				T result = _<T>(original);
				result.transform.parent = parent;
				return result;
			}
			
			public static T parent<T>(T original, GameObject parent_target) where T : MonoBehaviour{
				return parent<T>(original, parent_target.transform);
			}
			
			public static T parent<T>(T original, Transform parent_target, bool reset_pos) where T : MonoBehaviour{
				T result = parent<T>(original, parent_target);
				if (reset_pos){
					result.transform.localPosition = Vector3.zero;
				}
				return result;
			}
			
			public static GameObject parent(GameObject original, Transform parent_target){
				GameObject result = _(original);
				result.transform.parent = parent_target;
				return result;
			}
			
			public static GameObject parent(GameObject original, GameObject parent_target){
				return parent(original, parent_target.transform);
			}
			
			public static GameObject parent(GameObject original, Transform parent_target, bool reset_pos){
				GameObject result = parent(original, parent_target);
				if (reset_pos){
					result.transform.localPosition = Vector3.zero;
				}
				return result;
			}
			
			public static GameObject parent(GameObject original, GameObject parent_target, bool reset_pos){
				return parent(original, parent_target.transform, reset_pos);
			}
			
			public static T _<T>(T obj) where T: MonoBehaviour{
				T result = instantiate._<T>(obj);
				result.gameObject.SetActive(false);
				return result;
			}
			
			public static T _<T>(T obj, Vector3 pos) where T : MonoBehaviour{
				T result = _<T>(obj);
				result.transform.position = pos;
				return result;
			}
			
			public static T _<T>(T obj, Vector3 pos, Quaternion rot) where T : MonoBehaviour{
				T result = _<T>(obj, pos);
				result.transform.rotation = rot;
				return result;
			}
			
			public static GameObject _(GameObject obj){
				GameObject result = instantiate._(obj);
				result.SetActive(false);
				return result;
			}
			
			public static GameObject _(GameObject obj, Vector3 pos){
				GameObject result = _(obj);
				result.transform.position = pos;
				return result;
			}
			
			public static GameObject _(GameObject obj, Vector3 pos, Quaternion rot){
				GameObject result = _(obj, pos);
				result.transform.rotation = rot;
				return result;
			}
		}
	}
}

