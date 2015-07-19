using UnityEngine;
using System.Collections;

namespace Controller {
	namespace Joystick {
		public class Joystick : MonoBehaviour {

			public string key_map = "player 1";
			public Controller.Player_controller controller;

			public Vector2 axis_mouse = Vector2.zero;
			public Vector3 axis_esdf = Vector3.zero;
			public Vector3 mouse_pos = Vector3.zero;
			public float mouse_wheel = 0f;

			public float dead_zone_esdf_axis = 0.01f;
			public float dead_zone_mouse_axis = 0.01f;
			public float dead_zone_mouse_wheel = 0.01f;

			public bool pass_dead_zone_esdf_axis {
				get;
				protected set;
			}
			public bool pass_dead_zone_mouse_axis {
				get;
				protected set;
			}
			public bool pass_dead_zone_mouse_wheel {
				get;
				protected set;
			}

			protected void Awake() {
				_init_cache();
			}

			protected void Update() {
				update_all_axis();
				//if ( pass_dead_zone_esdf_axis )
				controller.change_moving_vector( axis_esdf );
				if (Input.GetMouseButton(1) && pass_dead_zone_mouse_axis )
					controller.moving_camera( axis_mouse );
				if ( pass_dead_zone_mouse_wheel )
					controller.add_distace( mouse_wheel );

				if ( Input.GetButton( "Jump" ) ) {
					controller.jump();
				}
			}

			public void update_all_axis() {
				_get_axis_esdf();
				_get_axis_mouse();
				_get_mouse_pos();
			}

			protected void _get_axis_esdf() {
				axis_esdf.x = Input.GetAxis( "Horizontal" );
				axis_esdf.z = Input.GetAxis( "Vertical" );
				pass_dead_zone_esdf_axis = Joystick.pass_dead_zone( axis_esdf.magnitude, dead_zone_esdf_axis );
			}

			protected void _get_axis_mouse() {
				axis_mouse.x = helper.mouse.axis_x;
				axis_mouse.y = helper.mouse.axis_y;
				mouse_wheel = helper.mouse.wheel;
				pass_dead_zone_mouse_axis = Joystick.pass_dead_zone( axis_mouse.magnitude, dead_zone_mouse_axis );
				pass_dead_zone_mouse_wheel = Joystick.pass_dead_zone( mouse_wheel, dead_zone_mouse_wheel );
			}

			protected void _get_mouse_pos() {
				mouse_pos = Input.mousePosition;
			}

			public static bool pass_dead_zone( float value, float dead_zone ) {
				return value < -dead_zone || dead_zone < value;
			}

			protected void _init_cache() {
				if ( controller == null )
					controller = GetComponent<Controller.Player_controller>();
			}
		}
	}
}