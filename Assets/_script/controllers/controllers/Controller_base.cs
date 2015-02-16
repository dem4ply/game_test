using UnityEngine;
using System.Collections;
using Controller.Motor;

namespace Controller {
	namespace Controller {
		public class Controller_base : MonoBehaviour {
			protected Transform _transform;
			protected Motor_base _motor;

			protected void Awake() {
			}

			protected void Update() {
				if ( Camera.main == null ) {
					return;
				}
			}

			/// <summary>
			/// inicializa el chache del script
			/// </summary>
			protected virtual void _init_cache() {
				_transform = transform;
				_motor = GetComponent<Motor_base>();
			}
		}
	}
}

