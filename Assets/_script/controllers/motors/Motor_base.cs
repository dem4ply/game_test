using UnityEngine;
using System.Collections;


namespace Controller {
	namespace Motor {
		public class Motor_base : MonoBehaviour {

			public float move_speed = 10f;
			public float jump_speed = 4f;

			protected Transform _transform;
			protected CharacterController _character_controller;
			protected Vector3 _move_vector;
			protected float _vertical_velocity;

			/// <summary>
			/// vector de movimiento
			/// </summary>
			public Vector3 move_vector {
				get {
					return _move_vector;
				}
				set {
					_move_vector = value;
				}
			}

			/// <summary>
			/// esta saltando el personaje
			/// </summary>
			public bool is_jumping {
				get;
				protected set;
			}

			/// <summary>
			/// no esta saltando el personaje
			/// </summary>
			public bool is_no_jumping {
				get {
					return !is_jumping;
				}
			}

			protected void Awake() {
				_init_cache();
			}

			protected void LateUpdate() {
				update_motor();
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
				// trasformar el vector de movimiento a WorldSpace
				_move_vector = _transform.TransformDirection( _move_vector );
				// normaliza el vector si su magnitud es mayor a 1
				if ( _move_vector.magnitude > 1 )
					_move_vector.Normalize();
				// multiplicar el vector de movimiento por la velocidad
				_move_vector *= move_speed;
				apply_gravity();
				_move_vector.y = _vertical_velocity;
				// convertir el movimiento de m/f a m/s
				_move_vector *= Time.deltaTime;
				// mandar el el vector de movimiento al character controller
				_character_controller.Move( _move_vector );
				is_jumping = false;
			}

			/// <summary>
			/// agrega la gravedad al vector de movimiento
			/// </summary>
			protected void apply_gravity() {
				if ( !_character_controller.isGrounded )
					_vertical_velocity -= Physics.gravity.magnitude * Time.deltaTime;
				else if (is_no_jumping)
					_vertical_velocity = 0f;
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
			/// aplica la fuerza para saltar
			/// </summary>
			/// <returns>cierto si logro saltar</returns>
			public bool jump() {
				Debug.Log( _character_controller.isGrounded );
				if ( _character_controller.isGrounded ) {
					_vertical_velocity = jump_speed;
					is_jumping = true;
					return true;
				}
				return false;
			}

			protected void OnControllerColliderHit( ControllerColliderHit hit ) {
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
