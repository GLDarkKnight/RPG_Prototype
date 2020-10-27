/*LightingcycleManger.cs
 * 10-24-2020
 * Light cycle manger
 * RPG.DayCycle 
 */
using UnityEngine;

namespace RPG.DayCycle
{
    //[ExecuteInEditMode]
    public class LightingcycleManger : MonoBehaviour
    {
        [Header("Time"), Tooltip("Day Lenght in Minutes"), SerializeField] private float _targetDayLength = 13f; //day in lenght via Minutes
        [SerializeField, Range(0f, 1f)] private float _TimeofDay; // from 0 to 1 time of day
        [SerializeField] private int _dayNumber = 0; //How many days have passed
        [SerializeField] private int _monthNumber = 0; //How many Months have passed
        [SerializeField] private int _yearNumber =0 ; //How many years have passed
        private float _timeScale = 100f; //Time Scale its set
        [SerializeField, Tooltip("How many days equals a month")] private int _monthLength;//How many days equals a month
        [SerializeField, Tooltip("How many months equals a year")] private int _yearLength; //How many months equals a year
        [SerializeField] private Transform dailyRotation; //The Rotation
        [SerializeField] private Light sun; //Light Referance
        [SerializeField] private float intensity; // How strong is the light
        [SerializeField] private float sunVarition = 1.5f; //Sun Varition so random up to Max 1.5
        [SerializeField] private float sunBaseIntensity = 2f; //Base Intensity
        [SerializeField] private Gradient sunColor; //Sun colors

        public bool pause = false;

        private void UpdateTimeScale()
        {
            _timeScale = 24 / (_targetDayLength / 60);
        }
        private void Update()
        {
            if(!pause)
            {
                UpdateTimeScale();
                UpdateTime();
            }
            AdjustSunRotation();
            SunIntensity();
            AjustColor();
        }
        private void UpdateTime()
        {
            _TimeofDay += Time.deltaTime * _timeScale / 86400; //Seconds in a day
            if (_TimeofDay > 1) //its a new day
            {
                _dayNumber++;
                _TimeofDay -= 1;
                print("Its day " + _dayNumber);
            }
            if (_dayNumber > _monthLength)
            {
                _monthNumber++;
                _dayNumber = 1;//set to Day 1 because theres not Day 0
                print("Its next month - " + _monthNumber);
            }
            if (_monthNumber > _yearLength)//New Year Dont forget to change to Months
            {
                _yearNumber++;
                _monthNumber = 1; //set to month 1 because theres no month 0
                print("Its year " + _yearNumber);
            }
        }
        //rotates the sun daily!
        private void AdjustSunRotation()
        {
            float sunAngle = _TimeofDay * 360f;
            dailyRotation.transform.localRotation = Quaternion.Euler(new Vector3(sunAngle, 0f, 0f));//local rotation of the sun angel
        }
        private void SunIntensity()
        {
            intensity = Vector3.Dot(sun.transform.forward, Vector3.down);
            intensity = Mathf.Clamp01(intensity);

            sun.intensity = intensity * sunVarition * sunBaseIntensity;
        }
        private void AjustColor()
        {
            sun.color = sunColor.Evaluate(_TimeofDay);
        }
    }
}
