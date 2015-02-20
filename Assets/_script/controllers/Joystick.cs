using UnityEngine;
using System.Collections;

namespace Controller {
	public class Joystick {

		public Vector3 axis_mouse;
		public Vector3 axis_esdf;
		public Vector3 mouse_pos;
		public float mouse_wheel;

		public float dead_zone_esdf_axis;
		public float dead_zone_mouse_axis;
		public float dead_zone_mouse_wheel;

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

		public Joystick() {
			axis_esdf = new Vector3();
			axis_mouse = new Vector3();
			mouse_pos = new Vector3();
			mouse_wheel = 0.0f;

			dead_zone_mouse_axis = 0.01f;
			dead_zone_mouse_wheel = 0.01f;
			dead_zone_esdf_axis = 0.01f;
		}

		public void update_all() {
			_get_axis_esdf();
			_get_axis_mouse();
			_get_mouse_pos();
		}

		protected void _get_axis_esdf() {
			axis_esdf.x = Input.GetAxis( "Horizontal" );
			axis_esdf.z = Input.GetAxis( "Vertical" );
			pass_dead_zone_esdf_axis = Joystick.pass_dead_zone(axis_esdf.magnitude, dead_zone_esdf_axis);
		}

		protected void _get_axis_mouse() {
			axis_mouse.x = helper.mouse.axis_x;
			axis_mouse.y = helper.mouse.axis_y;
			mouse_wheel = helper.mouse.wheel;
			pass_dead_zone_mouse_axis = Joystick.pass_dead_zone( mouse_wheel, dead_zone_mouse_axis );
			pass_dead_zone_mouse_wheel = Joystick.pass_dead_zone( axis_mouse.magnitude, dead_zone_mouse_wheel );
		}

		protected void _get_mouse_pos() {
			mouse_pos = Input.mousePosition;
		}

		public static bool pass_dead_zone( float value, float dead_zone ) {
			return value < -dead_zone || dead_zone < value;
		}
	}
}
