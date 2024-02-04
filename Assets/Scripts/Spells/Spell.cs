using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    // Start is called before the first frame update
    public virtual async UniTask Preparation()
    {
       
    }

    public virtual async UniTask AttackFirst()
    {

    }

    public virtual async UniTask AttackSecond()
    {

    }
}
