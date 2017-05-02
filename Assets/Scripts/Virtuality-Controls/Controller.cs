using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class Controller : MonoBehaviour
{
    // finding of other controller //
    public GameObject other_controller;

    // finding of detection components //
    SteamVR_TrackedObject tracked_object;
    SteamVR_Controller.Device detector;
    void Awake()
    {
        tracked_object = GetComponent<SteamVR_TrackedObject>();
    }

    // detection - trigger //
    public bool trigger_shallowed()
    {
        detector = SteamVR_Controller.Input((int)tracked_object.index);
        return detector.GetTouch(SteamVR_Controller.ButtonMask.Trigger);
    }
    public bool trigger_shallowing()
    {
        detector = SteamVR_Controller.Input((int)tracked_object.index);
        return detector.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger);
    }
    public bool trigger_unshallowing()
    {
        detector = SteamVR_Controller.Input((int)tracked_object.index);
        return detector.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger);
    }
    public bool trigger_deeped()
    {
        detector = SteamVR_Controller.Input((int)tracked_object.index);
        return detector.GetPress(SteamVR_Controller.ButtonMask.Trigger);
    }
    public bool trigger_deeping()
    {
        detector = SteamVR_Controller.Input((int)tracked_object.index);
        return detector.GetPressDown(SteamVR_Controller.ButtonMask.Trigger);
    }
    public bool trigger_undeeping()
    {
        detector = SteamVR_Controller.Input((int)tracked_object.index);
        return detector.GetPressUp(SteamVR_Controller.ButtonMask.Trigger);
    }

    // detection - touchpad //
    public bool touchpad_touched()
    {
        detector = SteamVR_Controller.Input((int)tracked_object.index);
        return detector.GetTouch(SteamVR_Controller.ButtonMask.Touchpad);
    }
    public bool touchpad_touching()
    {
        detector = SteamVR_Controller.Input((int)tracked_object.index);
        return detector.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad);
    }
    public bool touchpad_untouching()
    {
        detector = SteamVR_Controller.Input((int)tracked_object.index);
        return detector.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad);
    }
    public bool touchpad_pressed()
    {
        detector = SteamVR_Controller.Input((int)tracked_object.index);
        return detector.GetPress(SteamVR_Controller.ButtonMask.Touchpad);
    }
    public bool touchpad_pressing()
    {
        detector = SteamVR_Controller.Input((int)tracked_object.index);
        return detector.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad);
    }
    public bool touchpad_unpressing()
    {
        detector = SteamVR_Controller.Input((int)tracked_object.index);
        return detector.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad);
	}
	public float touchpad_x()
	{
		detector = SteamVR_Controller.Input((int)tracked_object.index);
		Vector2 coordinates = detector.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
		return coordinates.x;
	}
	public float touchpad_y()
	{
		detector = SteamVR_Controller.Input((int)tracked_object.index);
		Vector2 coordinates = detector.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
		return coordinates.y;
	}

	// detection - menu button //
	public bool menu_button_pressed()
    {
        detector = SteamVR_Controller.Input((int)tracked_object.index);
        return detector.GetPress(SteamVR_Controller.ButtonMask.ApplicationMenu);
    }
    public bool menu_button_pressing()
    {
        detector = SteamVR_Controller.Input((int)tracked_object.index);
        return detector.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu);
    }
    public bool menu_button_unpressing()
    {
        detector = SteamVR_Controller.Input((int)tracked_object.index);
        return detector.GetPressUp(SteamVR_Controller.ButtonMask.ApplicationMenu);
	}

	// detection - grip //
	public bool grip_pressed()
	{
		if (gameObject.activeSelf)
		{
			detector = SteamVR_Controller.Input((int)tracked_object.index);
			return detector.GetPress(SteamVR_Controller.ButtonMask.Grip);
		}
		return false;
	}
	public bool grip_pressing()
	{
		detector = SteamVR_Controller.Input((int)tracked_object.index);
		return detector.GetPressDown(SteamVR_Controller.ButtonMask.Grip);
	}
	public bool grip_unpressing()
	{
		detector = SteamVR_Controller.Input((int)tracked_object.index);
		return detector.GetPressUp(SteamVR_Controller.ButtonMask.Grip);
	}

	// collating patterned velocity (based on the tracked velocity of the controller) to the rigidbody aspect of a released object //
	public void collate_velocity(Rigidbody released_object_rigidbody)
    {
        detector = SteamVR_Controller.Input((int)tracked_object.index);
        // attempted transform origin determination //
        Transform origin = tracked_object.origin ? tracked_object.origin : tracked_object.transform.parent.transform;
        if (origin != null)     // successful transform origin determination //
        {
            // setting velocity to the controller's patterned velocity based on the determined transform origin //
            released_object_rigidbody.velocity = origin.TransformVector(detector.velocity);
            released_object_rigidbody.angularVelocity = origin.TransformVector(detector.angularVelocity);
        }
        else       // fallback for failed attempts //
        {
            // setting velocity to the controller's patterned velocity based on the controller's transform origin (which may be different) //
            released_object_rigidbody.velocity = detector.velocity;
            released_object_rigidbody.angularVelocity = detector.angularVelocity;
        }
    }
	
	// delayed prevention of any vibration at the start //
	float vibration_prevention_delay = 1, vibration_prevention_timer = 0;
	bool vibration_allowed = false;
	void Update()
	{
		if (!vibration_allowed)
		{
			if (vibration_prevention_timer < vibration_prevention_delay)
			{
				vibration_prevention_timer += Time.deltaTime;
				if (vibration_prevention_timer > vibration_prevention_delay)
					vibration_prevention_timer = vibration_prevention_delay;
				if (vibration_prevention_timer == vibration_prevention_delay)
					vibration_allowed = true;
			}
		}
	}
    // vibration //
    public void vibrate(ushort intensity)
    {
		if (vibration_allowed)
		{
			detector = SteamVR_Controller.Input((int) tracked_object.index);
			detector.TriggerHapticPulse(intensity);
		}
    }
}