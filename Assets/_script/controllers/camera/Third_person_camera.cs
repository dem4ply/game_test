using UnityEngine;
using System.Collections;

namespace Controller{
	namespace Eye {
		public class Third_person_camera : MonoBehaviour {
			public Transform look_at;
			public GameObject eye;
			public float distance = 5.0f;
			public float distance_min = 3.0f;
			public float distance_max = 10.0f;

			public float distance_smooth = 0.05f;

			public float x_mouse_sensitivity = 5f;
			public float y_mouse_sensitivity = 5f;
			public float mouse_wheel_sensitivity = 5f;

			public float y_min_limit = -40f;
			public float y_max_limit = 80f;

			public float x_smoot = 0.05f;
			public float y_smoot = 0.1f;

			private float mouse_x = 0f;
			private float mouse_y = 0f;
			private float start_distance = 0f;
			private float desired_distance = 0f;
			private float velocity_distance = 0f;
			private float velocity_x = 0;
			private float velocity_y = 0;
			private float velocity_z = 0;
			private Vector3 position = Vector3.zero;

			private Vector3 desired_position = Vector3.zero;

			private Joystick _joystick;

			protected Transform _transform;

			protected void Awake() {
				_init_cache();
			}

			protected void Start() {
				distance = Mathf.Clamp( distance, distance_min, distance_max );
				start_distance = distance;
				Reset();
			}

			protected void LateUpdate() {
				if ( look_at == null )
					return;
				handled_input();
				calculate_desired_psoition();
				update_position();
			}

			protected void handled_input() {
				float dead_zone = 0.01f;
				_joystick.update_all();
				if ( Input.GetMouseButton( 1 ) ) {
					Vector3 mouse_axis = _joystick.axis_mouse;
					mouse_x += mouse_axis.x * x_mouse_sensitivity;
					mouse_y += mouse_axis.y * y_mouse_sensitivity;
				}
				mouse_y = helper.math.clamp_angle(mouse_y, y_min_limit, y_max_limit);

				float scroll_wheel = _joystick.mouse_wheel;
				Debug.Log( scroll_wheel );
				if ( scroll_wheel < -dead_zone || dead_zone < scroll_wheel) 
					desired_distance = Mathf.Clamp(distance - scroll_wheel * mouse_wheel_sensitivity, distance_min, distance_max);
			}

			protected void calculate_desired_psoition() {
				// evaluar la distacia
				distance = Mathf.SmoothDamp(distance, desired_distance, ref velocity_distance, distance_smooth);

				// calcular la posicion deseada
				desired_position = calculate_position( mouse_y, mouse_x, distance );
			}

			protected void update_position() {
				float pos_x = Mathf.SmoothDamp( position.x, desired_position.x, ref velocity_x, x_smoot );
				float pos_y = Mathf.SmoothDamp( position.y, desired_position.y, ref velocity_y, y_smoot );
				float pos_z = Mathf.SmoothDamp( position.z, desired_position.z, ref velocity_x, x_smoot );

				position = new Vector3(pos_x, pos_y, pos_z);
				eye.transform.position = position;
				eye.transform.LookAt( look_at );
			}

			public void Reset() {
				mouse_x = 0f;
				mouse_y = 10f;
				distance = start_distance;
				desired_distance = distance;
			}

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
			/// <param name="rotation_x"> angulo de la orbita x </param>
			/// <param name="rotation_y"> angulo de la orbita y </param>
			/// <param name="distace"> distancia a la que estara la camara del objetivo </param>
			/// <returns> vector de la posicion a la que tiene que moverse la camara </returns>
			protected Vector3 calculate_position(float rotation_x, float rotation_y, float distace) {
				Vector3 direction = new Vector3(0, 0, -distance);
				Quaternion rotation = Quaternion.Euler(rotation_x, rotation_y, 0f);
				return look_at.position + rotation * direction;
			}

			//protected Vector3 calculate_position(Vector3) {
			//	Vector3 
			//}

			/// <summary>
			/// inicializa el chache del script
			/// </summary>
			protected virtual void _init_cache() {
				_transform = transform;
				if (eye == null)
					_use_existing_or_create_new_camera();
				_joystick = new Joystick();
			}
		}
	}
}