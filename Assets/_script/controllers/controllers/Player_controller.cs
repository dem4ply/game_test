using UnityEngine;
using System.Collections;

namespace Controller {
	namespace Controller {
		public class Player_controller : Controller_base {

			public Eye.Third_person_camera eye;
			public Player_animator _player_animator;

			protected void Update() {
				//_joystick.update_all();
				//if (_joystick.pass_dead_zone_esdf_axis)
				//	change_moving_vector(_joystick.axis_esdf);
			}

			public void moving_camera( Vector2 moving_vector ) {
				eye.add_moving_vector( moving_vector );
			}

			public void add_distace( float distance ) {
				eye.add_distance( distance );
			}

			public void jump() {
				_motor.jump();
			}

			protected override void _init_cache() {
				base._init_cache();
				if ( eye == null )
					eye = GetComponent<Eye.Third_person_camera>();
				_player_animator = GetComponent<Player_animator>();
			}
		}
	}
}
