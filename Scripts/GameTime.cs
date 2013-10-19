using UnityEngine;
using System.Collections;

public class GameTime : MonoBehaviour {
	public Transform[] sun;
	public float DayCycleInMinutes = 1;
	public float StartTimeInHours = 0;
	
	private const float SECOND = 1;
	private const float MINUTE = 60 * SECOND;
	private const float HOUR = 60 * MINUTE;
	private const float DAY = 24 * HOUR;
	
	private const float DEGREES_PER_SECOND = 360 / DAY;
	
	private float _degreeRotation;
	
	public static float _dayCycleInSeconds;
	public static float _timeOfDay;
	public static float _sunrise;
	public static float _sunset;
	
	// Use this for initialization
	void Start () {
		_dayCycleInSeconds = DayCycleInMinutes * MINUTE;
		
		RenderSettings.skybox.SetFloat("_Blend",0);
		_timeOfDay = (StartTimeInHours / 24) * _dayCycleInSeconds;
		_degreeRotation = DEGREES_PER_SECOND * DAY / _dayCycleInSeconds;
		for (int i = 0; i < this.sun.Length; i++) {
			float num = DEGREES_PER_SECOND * StartTimeInHours * HOUR;
			sun[i].Rotate(new Vector3(num, 0, 0));
		}
		_sunrise = _dayCycleInSeconds / 4;
		_sunset = (_dayCycleInSeconds / 4) * 3;
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < this.sun.Length; i++) {
			sun[i].Rotate(new Vector3(_degreeRotation, 0, 0) * Time.deltaTime);
		}
		
		_timeOfDay += Time.deltaTime;
		
		if (_timeOfDay > _dayCycleInSeconds) {
			_timeOfDay -= _dayCycleInSeconds;
		}
		
		BlendSkybox();
	}
	
	private void BlendSkybox() {
		float temp = _timeOfDay / _dayCycleInSeconds * 2;
		
		if (temp > 1) {
			temp = 1 - (temp - 1);
		}
		RenderSettings.skybox.SetFloat("_Blend",temp);
	}
}
