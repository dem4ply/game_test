using UnityEngine;
using System.Collections;


namespace Controller {
	namespace Motor {
		public class Motor_base : MonoBehaviour {

			public float move_speed = 10f;
			public float jump_force = 4f;
			public float terminal_speed = 45f;

			public float rotation_speed = 10f;

			public Vector3 max_move_speed = new Vector3( 10, 0, 10 ); 

			public float slide_threshold = 0.6f;
			public float max_slice_controllable = 0.4f;

			protected Transform _transform;
			//protected CharacterController _character_controller;
			protected Rigidbody _rididbody;
			public Vector3 _move_vector;
			public float _vertical_velocity;

			public Vector3 slice_direction = Vector3.zero;

			/// <summary>
			/// vector de movimiento
			/// </summary>
			public virtual Vector3 move_vector {
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

			/// <summary>
			/// si esta el personaje en el piso
			/// </summary>
			public bool is_grounded {
				get;
				protected set;
			}

			/// <summary>
			/// contructor
			/// </summary>
			protected void Awake() {
				_init_cache();
				_rididbody.freezeRotation = true;
			}

			protected void FixedUpdate() {
				update_motor();
			}

			/// <summary>
			/// actualiza todo el motor solo debe de ser llamada
			/// por el control
			/// </summary>
			public void update_motor() {
				_snap_align_character_with_camera();
				_proccess_motion();
			}

			/// <summary>
			/// procesa el vector de movimiento
			/// </summary>
			protected void _proccess_motion() {
				if ( _move_vector.magnitude > 1 )
					_move_vector.Normalize();
				//_move_vector *= move_speed;
				Vector3 desire_velocity = _transform.TransformDirection( _move_vector );
				//Vector3 desire_velocity = ( _move_vector );
				desire_velocity *= move_speed;

				Vector3 velocity = _rididbody.velocity;
				Vector3 change_velocity = desire_velocity - velocity;

				change_velocity = helper.vector3.clamp( change_velocity, -max_move_speed, max_move_speed);
				change_velocity.y = 0;

				_rididbody.AddForce( change_velocity, ForceMode.VelocityChange );
			}

			/// <summary>
			/// alinea al personaje ( _transform ) con la camara
			/// </summary>
			protected void _snap_align_character_with_camera() {
				if ( _move_vector.x != 0 || _move_vector.z != 0 ) {
					Quaternion rot = Quaternion.Euler( _transform.eulerAngles.x,
						Camera.main.transform.eulerAngles.y,
						_transform.eulerAngles.z );
					_transform.rotation = rot;
				}
			}

			/// <summary>
			/// aplica la fuerza para saltar
			/// </summary>
			/// <returns>cierto si logro saltar</returns>
			public bool jump() {
				if ( is_grounded && is_no_jumping ) {
					_rididbody.velocity = new Vector3( _rididbody.velocity.x, calculate_jump_vertical_speed(), _rididbody.velocity.z );
					is_jumping = true;
					return true;
				}
				return false;
			}

			/// <summary>
			/// calcula la fuerza del salto
			/// </summary>
			/// <returns>fuerza del salto</returns>
			protected float calculate_jump_vertical_speed() {
				return Mathf.Sqrt( 2 * jump_force * Physics.gravity.magnitude );
			}

			/// <summary>
			/// evento cuando esta sobre un collider
			/// </summary>
			protected void OnCollisionStay() {
				is_jumping = false;
				is_grounded = true;
			}

			/// <summary>
			/// inicializa el chache del script
			/// </summary>
			protected virtual void _init_cache() {
				_transform = transform;
				_rididbody = GetComponent<Rigidbody>();
			}
		}
	}
}
