using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class ComboCounter : MonoBehaviour
{
    [Inject]
    EnemyHealth enemyHealth;
    [Inject]
    Fight heroFight;
    [SerializeField]
    TextMeshProUGUI label, counter;
    
    private float combo = 0.9f;
    public float Combo => combo;

    void Start()
    {
        label = GetComponentInChildren<TextMeshProUGUI>();
        enemyHealth.OnHealthChanged += HealthChangedHandler;
        DecreaseCounter();
        
    }

    private void HealthChangedHandler(int health, int value)
    {
        if (value <= 0) return;

        label.transform.DOScale(1.2f, 0.1f).SetLoops(2, LoopType.Yoyo);
        combo += 0.1f;
        counter.text = combo.ToString("0.0").Replace(',', '.');
        // UniTask.Delay(1500).ContinueWith(() => { combo -= 0.1f; counter.text = combo.ToString(); });
    }

    
    private void OnDestroy()
    {
        enemyHealth.OnHealthChanged -= HealthChangedHandler;

    }
    // Update is called once per frame
    void Update()
    {
        transform.forward = heroFight.transform.forward;
        if(combo >= 1 && enemyHealth.Current>0)
        {
            label.enabled = true;
            counter.enabled = true;
        } else
        {
            label.enabled = false;
            counter.enabled = false;
        }

    }

    async UniTask DecreaseCounter()
    {
        while (true)
        {
            await UniTask.Delay(2000);
            if (heroFight != null && !heroFight.isActiveAndEnabled || heroFight.transform.position.magnitude > heroFight.MinDistance)
            {
                combo = Mathf.Max(0.9f, combo - 0.1f); 
                counter.text = combo.ToString("0.0").Replace(',','.');
            }
        
        }

    }
}
