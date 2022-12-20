using Assets._Project.Scripts.Entity;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Scripts.UI
{
    public class UI_HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _healthBarSlider;
        [SerializeField] private GameObject _healthSource;

        // Start is called before the first frame update
        void Start()
        {
            _healthBarSlider.value = _healthBarSlider.maxValue;
            _healthSource.GetComponent<IHealth>().OnHealthChangedEventHandler += HealthSource_OnHealthChangedEventHandler;
        }

        private void HealthSource_OnHealthChangedEventHandler(object sender, OnHealthChangedEventArgs e)
        {
            _healthBarSlider.value = e.CurrentHealth;
        }
    }
}