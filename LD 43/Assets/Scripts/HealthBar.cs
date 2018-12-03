using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
    private HealthSystem _healthSystem;

	public void Setup(HealthSystem healthSystem)
    {
        this._healthSystem = healthSystem;

        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
    }

    private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e)
    {
        transform.Find("Bar").localScale = new Vector2(_healthSystem.GetHealthPercent(), 1);
    }
}
