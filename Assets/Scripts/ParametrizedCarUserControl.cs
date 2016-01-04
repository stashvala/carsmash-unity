using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof (CarController))]
public class ParametrizedCarUserControl : MonoBehaviour {
	
	private CarController m_Car; // the car controller we want to use
	public string vertical = "VerticalP1";
	public string horizontal = "HorizontalP1";

	private void Awake()
	{
		// get the car controller
		m_Car = GetComponent<CarController>();
	}
	
	
	private void FixedUpdate()
	{
		// pass the input to the car!
		float h = CrossPlatformInputManager.GetAxis(horizontal);
		float v = CrossPlatformInputManager.GetAxis(vertical);
		#if !MOBILE_INPUT
		m_Car.Move(h, v, v, 0f);
		#else
		m_Car.Move(h, v, v, 0f);
		#endif
	}
}



