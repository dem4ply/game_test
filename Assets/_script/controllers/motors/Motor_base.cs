using UnityEngine;
using System.Collections;


namespace Controller {
	namespace Motor {
		public class Motor_base : MonoBehaviour {
			protected Transform _transform;

			protected void Awake() {
			}

			protected void Update() {
			}

			/// <summary>
			/// inicializa el chache del script
			/// </summary>
			protected virtual void _init_cache() {
				_transform = transform;
			}
		}
	}
}
