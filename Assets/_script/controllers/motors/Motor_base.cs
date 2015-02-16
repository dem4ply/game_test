using UnityEngine;
using System.Collections;


namespace Controller {
	namespace Motor {
		public class Motor_base : MonoBehaviour {

			public float move_speed = 10.0f;

			protected Transform _transform;
			protected CharacterController _character_controller;
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
				// trasformar el vector de movimiento a WordSpace
				_move_vector = _transform.TransformDirection( _move_vector );
				// normaliza el vector si su magnitud es mayor a 1
				if ( _move_vector.magnitude > 1 )
					_move_vector.Normalize();
				// multiplicar el vector de movimiento por la velocidad
				_move_vector *= move_speed;
				// convertir el movimiento de m/f a m/s
				_move_vector *= Time.deltaTime;
				// mandar el el vector de movimiento al character controller
				_character_controller.Move( _move_vector );
			}

			/// <summary>
			/// alinea al personaje ( _transform ) con la camara
			/// </summary>
			protected void _snap_align_character_with_camera() {
				if ( _move_vector.x != 0 || _move_vector.z != 0 ) {
					_transform.rotation = Quaternion.Euler(_transform.eulerAngles.x,
							Camera.main.transform.eulerAngles.y,
							_transform.eulerAngles.z);
				}
			}

			/// <summary>
			/// inicializa el chache del script
			/// </summary>
			protected virtual void _init_cache() {
				_transform = transform;
				_character_controller = GetComponent<CharacterController>();
			}
		}
	}
}
