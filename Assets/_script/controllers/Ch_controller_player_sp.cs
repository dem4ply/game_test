using UnityEngine;
using System.Collections;

public class Ch_controller_player_sp : Ch_controller {

	protected Controller.Joystick _joystick;

	protected void Update(){
		_joystick.update_all();
		change_moving_vector(_joystick.axis_esdf);
	}

	protected override void _init_cache(){
		base._init_cache();
		_joystick = new Controller.Joystick();
	}
}
