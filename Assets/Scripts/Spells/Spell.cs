using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public abstract class Spell : MonoBehaviour
{
    protected virtual void Start()
    {
        FindObjectOfType<EnemyHealth>().OnDead += DeadHandler;
    }

    private void DeadHandler()
    {
        FindObjectOfType<EnemyHealth>().OnDead -= DeadHandler;
        if(gameObject!=null)
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    public virtual async UniTask Preparation()
    {
       
    }

    public virtual async UniTask AttackFirst(int damage)
    {

    }

    public virtual async UniTask AttackSecond(int damage)
    {

    }
}
