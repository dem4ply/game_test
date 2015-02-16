using UnityEngine;
using System.Collections;


namespace Controller {
	namespace Motor {
		public class Motor_base : MonoBehaviour {

			public float move_speed = 10.0f;

			protected Transform _transform;
			protected Vector3 _move_vector;

			public Vector3 move_vector {
				get {
					return _move_vector;
				}
				set {
					_move_vector = value;
				}
			}

			protected void Awake() {
				_init_cache();
			}

			/// <summary>
			/// actualiza todo el motor solo debe de ser llamada
			/// por el controll
			/// </summary>
			public void update_motor() {
				_snap_align_character_with_camera();
				_proccess_motion();
			}

			/// <summary>
			/// procesa el vector de movimiento
			/// </summary>
			/// <param name="move_vector">vector de movimiento que genero el control</param>
			protected void _proccess_motion() {

				if ( _move_vector.magnitude > 1 )
					_move_vector.Normalize();
			}

			/// <summary>
			/// alinea al personaje ( _transform ) con la camara
			/// </summary>
			protected void _snap_align_character_with_camera() {
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
