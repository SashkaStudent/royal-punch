using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class FloatingDamage : MonoBehaviour
{
    private Fight playerFight;
    [SerializeField]
    Color outColor;
    TextMeshProUGUI label;
    // Start is called before the first frame update
    void Awake()
    {
        transform.localPosition += Vector3.right * Random.Range(-0.25f, 0.26f); 
        label = GetComponentInChildren<TextMeshProUGUI>();
        label.DOColor(outColor, 1f);
        transform.DOMove(transform.position + Vector3.up * 0.5f, 1f);
        label.transform.DOScale(2f, 1.1f).OnComplete(()=>Destroy(gameObject));
        playerFight = FindObjectOfType<Fight>();
        
    }

    public void SetDamage(int value)
    {
        label.text = value.ToString();
    }

    private void Update()
    {
    }

}
