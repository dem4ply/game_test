using UnityEngine;
using System.Collections;
using Controller.Motor;

namespace Controller {
	namespace Controller {
		public class Controller_base : MonoBehaviour {

			public float dead_zone_joystick = 0.1f;
			
			protected Transform _transform;
			protected Motor_base _motor;
			//protected Joystick _joystick;

			protected void Awake() {
				_init_cache();
			}

			/// <summary>
			/// modifica el movimiento del vector del motor
			/// </summary>
			/// <param name="move_vector"> vector de movimiento del motor </param>
			public void change_moving_vector(Vector3 move_vector) {
				_motor.move_vector = move_vector;
			}

			/// <summary>
			/// inicializa el chache del script
			/// </summary>
			protected virtual void _init_cache() {
				_transform = transform;
				_motor = GetComponent<Motor_base>();
				//_joystick = new Joystick();
			}
		}
	}
}

