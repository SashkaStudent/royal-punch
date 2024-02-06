using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using Zenject;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    //[Inject]
    //private EnemyHealth enemyHealth;
    private Health health;

    [Inject]
    private Fight playerFight;

    [SerializeField]
    private GameObject floatingDamage;

    [SerializeField]
    private GameObject bar, barBack;

    TextMeshProUGUI label;
    void Start()
    {
        health = GetComponentInParent<Health>();

        label = GetComponentInChildren<TextMeshProUGUI>();
        health.OnHealthChanged += HealthChangedHandler;
        label.text = health.Current.ToString();
    }

    private void HealthChangedHandler(int health, int value)
    {
        label.text = health > 0 ? health.ToString() : string.Empty;

        if (health <= 0) GetComponentInChildren<Canvas>().gameObject.SetActive(false);
        else if(!GetComponentInChildren<Canvas>().gameObject.activeSelf) GetComponentInChildren<Canvas>().gameObject.SetActive(false);

        if (value <= 0) return;

        Instantiate(floatingDamage, transform).GetComponent<FloatingDamage>().SetDamage(value);
        label.transform.DOScale(1.5f, 0.1f).SetLoops(2, LoopType.Yoyo);

        bar.transform.DOScaleX(this.health.Percentage, 0.1f);
        barBack.transform.DOScaleX(this.health.Percentage, 0.1f).SetDelay(1f);

    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = playerFight.transform.forward;
    }
}
