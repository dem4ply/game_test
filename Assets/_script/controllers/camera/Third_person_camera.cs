using UnityEngine;
using System.Collections;

namespace Controller{
	namespace Eye {
		public class Third_person_camera : MonoBehaviour {
			public Transform look_at;
			public GameObject eye;

			public float current_distance = 5.0f;
			public float distance_min = 3.0f;
			public float distance_max = 10.0f;

			public float orbit_smooth = 0.05f;
			public float distance_smooth = 0.05f;

			public float sensitivity_horizontal = 5f;
			public float sensitivity_vertical = 5f;
			public float sensitivity_distance = 5f;

			public float y_min_limit = -40f;
			public float y_max_limit = 80f;

			private float _start_distance = 0f;
			private float _desired_distance = 0f;
			private float _velocity_distance = 0f;
			private Vector3 _velocity_vector = Vector3.zero;
			private Vector3 _position = Vector3.zero;

			private Vector3 _desired_position = Vector3.zero;

			private Vector2 _rotation_vector = Vector2.zero;

			protected Transform _transform;

			protected void Awake() {
				_init_cache();
			}

			protected void Start() {
				current_distance = Mathf.Clamp( current_distance, distance_min, distance_max );
				_start_distance = current_distance;
				Reset();
			}

			protected void LateUpdate() {
				if ( look_at == null )
					return;
				calculate_desired_psoition();
				update_position();
			}
			/// <summary>
			/// añade la posicion de la camara en su orbita
			/// </summary>
			/// <param name="moving_vector">vector con la posicion que se agregara</param>
			public void add_moving_vector( Vector2 moving_vector ) {
				moving_vector.y *= sensitivity_vertical;
				moving_vector.x *= sensitivity_horizontal;
				
				this._rotation_vector += moving_vector;
				_rotation_vector.y = helper.math.clamp_angle( _rotation_vector.y,
					y_min_limit, y_max_limit );
			}

			/// <summary>
			/// agrega la distancia de la camara al objetivo sin que se pase de los minimos y maximos
			/// </summary>
			/// <param name="add_distance">distancia a añadir a la camara</param>
			public void add_distance(float add_distance) {
				_desired_distance = Mathf.Clamp( current_distance - add_distance * sensitivity_distance,
					distance_min, distance_max );
			}

			/// <summary>
			/// calcula la posion desea de la camara
			/// </summary>
			protected void calculate_desired_psoition() {
				current_distance = Mathf.SmoothDamp(current_distance, _desired_distance, ref _velocity_distance, distance_smooth);
				_desired_position = calculate_position( current_distance );
			}

			/// <summary>
			/// mueve la camara a la posicion deseada
			/// </summary>
			protected void update_position() {
				_position = Vector3.SmoothDamp(_position, _desired_position, ref _velocity_vector, orbit_smooth);
				eye.transform.position = _position;
				eye.transform.LookAt( look_at );
			}

			/// <summary>
			/// asigna los parametros a los de default
			/// </summary>
			public void Reset() {
				_rotation_vector = Vector2.zero;
				current_distance = _start_distance;
				_desired_distance = current_distance;
			}

			/// <summary>
			/// asigna la camara principal en caso de no existir crea una camara principal
			/// </summary>
			protected void _use_existing_or_create_new_camera() {
				if ( Camera.main != null ) {
					eye = Camera.main.gameObject;
				}
				else {
					eye = new GameObject("main camera");
					eye.AddComponent("Camera");
					eye.tag = "MainCamera";
				}
			}

			/// <summary>
			/// calcula la posicion a la que tiene que moverse la camara
			/// </summary>
			/// <param name="distace"> distancia a la que estara la camara del objetivo </param>
			/// <returns> vector de la posicion a la que tiene que moverse la camara </returns>
			protected Vector3 calculate_position(float distace) {
				Vector3 direction = new Vector3(0, 0, -current_distance);
				Quaternion rotation = Quaternion.Euler(_rotation_vector.y, _rotation_vector.x, 0f);
				return look_at.position + rotation * direction;
			}

			/// <summary>
			/// inicializa el chache del script
			/// </summary>
			protected virtual void _init_cache() {
				_transform = transform;
				if (eye == null)
					_use_existing_or_create_new_camera();
				//_joystick = new Joystick();
			}
		}
	}
}