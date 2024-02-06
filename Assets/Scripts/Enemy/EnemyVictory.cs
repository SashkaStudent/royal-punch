using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyVictory : Victory
{
    public override void Apply()
    {
        Animator animator = GetComponentInChildren<Animator>();

        animator.SetLayerWeight(animator.GetLayerIndex("Victory"), 1);
        UniTask.Delay(4000).ContinueWith(() => SceneManager.LoadScene(0,LoadSceneMode.Single));
    }
}
