using UnityEngine;
using System.Collections;

namespace Controller{
	namespace Eye {
		public class Third_person_camera : MonoBehaviour {
			public GameObject look_at;
			public GameObject eye;
			public float distance = 5.0f;
			public float distance_min = 3.0f;
			public float distance_max = 10.0f;

			public float x_mouse_sensitivity = 5f;
			public float y_mouse_sensitivity = 5f;
			public float mouse_wheel_sensitivity = 5f;

			public float y_min_limit = -40f;
			public float y_max_limit = 80f;

			private float mouse_x = 0f;
			private float mouse_y = 0f;
			private float start_distance = 0f;
			private float desired_distance = 0f;

			private Joystick _joystick;

			protected Transform _transform;

			protected void Awake() {
				_init_cache();
			}

			protected void Start() {
				distance = Mathf.Clamp( distance, distance_min, distance_max );
				start_distance = distance_max;
				Reset();
			}

			protected void UpdateLate() {
				if ( look_at == null )
					return;
				handled_input();
				calculate_desired_psoition();
				update_position();
			}

			protected void handled_input() {
				float dead_zone = 0.1f;
				_joystick.update_all();
				if ( Input.GetMouseButton( 1 ) ) {
					Vector3 mouse_axis = _joystick.axis_mouse;
					mouse_x += mouse_axis.x * x_mouse_sensitivity;
					mouse_y += mouse_axis.y * y_mouse_sensitivity;
				}

				mouse_y = helper.math.clamp_angle(mouse_y, y_min_limit, y_max_limit);

				float scroll_wheel = _joystick.mouse_wheel;
				if ( scroll_wheel < -dead_zone && scroll_wheel > dead_zone ) {
					desired_distance = Mathf.Clamp(distance - scroll_wheel * mouse_wheel_sensitivity, distance_min, distance_max);
				}

				
			}

			protected void calculate_desired_psoition() {
			}

			protected void update_position() {
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