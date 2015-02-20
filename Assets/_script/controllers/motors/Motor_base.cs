using UnityEngine;
using System.Collections;


namespace Controller {
	namespace Motor {
		public class Motor_base : MonoBehaviour {

			public float move_speed = 10f;
			public float jump_speed = 4f;

			public float slide_threshold = 0.6f;
			public float max_slice_controllable = 0.4f;

			protected Transform _transform;
			protected CharacterController _character_controller;
			public Vector3 _move_vector;
			protected float _vertical_velocity;

			public Vector3 slice_direction = Vector3.zero;

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
				apply_slice();
				_move_vector *= move_speed;
				_move_vector.y = _vertical_velocity;
				apply_gravity();
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
				else if (_character_controller.isGrounded && _move_vector.y < -1)
					_move_vector.y = -1;
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

			protected void apply_slice() {
				if ( !_character_controller.isGrounded )
					return;
				slice_direction = Vector3.zero;
				RaycastHit hit_info;
				Debug.DrawRay( _transform.position + Vector3.up , Vector3.down);
				if ( Physics.Raycast( _transform.position + Vector3.up, Vector3.down, out hit_info ) ) {
					Debug.Log( hit_info.normal.ToString() );
					if ( hit_info.normal.y < slide_threshold ) {
						slice_direction = new Vector3(hit_info.normal.x, -hit_info.normal.y, hit_info.normal.z);
					}
				}
				Debug.Log( slice_direction.ToString() + " : " + slice_direction.magnitude.ToString() );
				if ( slice_direction.magnitude < max_slice_controllable )
					_move_vector += slice_direction;
				else
					_move_vector = slice_direction;
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
