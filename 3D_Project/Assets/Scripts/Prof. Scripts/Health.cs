using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class Health : MonoBehaviour {
	public float health = 100;
	public float maxHealth = 100;
	public Text displayText = null;
	public Text displayText2 = null;
	public string textPrefix = "", textSuffix = " HP";
	public Image displayBar = null;
	public bool changeBarScale = false;
	public bool changeBarColor = true;
	public Color fullHealth = Color.green;
	public Color midHealth = new Color(1,1,0);
	public Color noHealth = Color.red;
	public GameObject displayPanel = null;
	public UnityEvent onDeathCall = null;
	public bool destroyOnDeath = true;
	private RectTransform rt;
	private float rtmax;
	void Start() {
		if(displayPanel) {
			rt = displayPanel.GetComponent<RectTransform>();
			if(rt) rtmax = rt.sizeDelta.x;
		}
	}
	void Update() {
		if(displayText) displayText.text = textPrefix + ((int)health) + textSuffix;
		if(displayText2) displayText2.text = textPrefix + ((int)health) + textSuffix;
		float php = health/maxHealth;
		if(displayBar) {
			if(changeBarScale) {
				Vector3 sc = displayBar.transform.localScale; //unity forces duplicate vector
				sc.x = php;
				displayBar.transform.localScale = sc;
			}
			if(changeBarColor) {
				displayBar.color = (php > 0.5f) ? Color.Lerp(midHealth,fullHealth,2*php - 1) : Color.Lerp(noHealth,midHealth,2*php);
			}
		}
		if(rt) { Vector2 r = rt.sizeDelta; r.x = php*rtmax; rt.sizeDelta = r; }
	}
	public void Damage(float amount) {
		health = Mathf.Clamp(health - amount,0,maxHealth);
		if(health == 0) {
			if(onDeathCall != null) onDeathCall.Invoke();
			if(destroyOnDeath) Destroy(gameObject);
		}
	}
}
