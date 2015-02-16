using UnityEngine;
using System.Collections;

namespace Controller {
	public class Joystick {
		
		public Vector3 axis_mouse;
		public Vector3 axis_esdf;
		public Vector3 mouse_pos;
		public float mouse_wheel;
		
		public Joystick(){
			axis_esdf = new Vector3();
			axis_mouse = new Vector3();
			mouse_pos = new Vector3();
			mouse_wheel = 0.0f;
		}
		
		public void update_all(){
			_get_axis_esdf();
			_get_axis_mouse();
		}
		
		protected void _get_axis_esdf(){
			axis_esdf.x = Input.GetAxis("Horizontal");
			axis_esdf.z = Input.GetAxis("Vertical");
		}
		
		protected void _get_axis_mouse(){
			axis_mouse.x = helper.mouse.axis_x;
			axis_mouse.y = helper.mouse.axis_y;
			mouse_wheel = helper.mouse.wheel;
		}
		
		protected void _get_mouse_pos(){
			mouse_pos = Input.mousePosition;
		}
	}
}
