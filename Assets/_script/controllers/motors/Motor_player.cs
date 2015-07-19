using UnityEngine;
using System.Collections;


namespace Controller {
	namespace Motor {
		public class Motor_player : Motor_base {

			protected Player_animator _animator;

			/// <summary>
			/// vector de movimiento
			/// </summary>
			public override Vector3 move_vector {
				get {
					return _move_vector;
				}
				set {
					_move_vector = value;
					float velocity = new Vector3( _rididbody.velocity.x, 0, _rididbody.velocity.z ).magnitude;
					float max_speed = max_move_speed.magnitude;
					//_animator.speed = Mathf.Clamp( velocity, -max_speed, max_speed );
					_animator.speed = velocity / max_speed;
				}
			}

			/// <summary>
			/// inicializa el chache del script
			/// </summary>
			protected override void _init_cache() {
				base._init_cache();
				_animator = GetComponent<Player_animator>();
			}
		}
	}
}
