using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroVictory : Victory
{
    [SerializeField]
    GameObject mainCamera;
    [SerializeField]
    private GameObject mesh;

    [SerializeField]
    GameObject particlesRoot;
    public override void Apply()
    {


        Animator animator = GetComponentInChildren<Animator>();

        animator.SetLayerWeight(animator.GetLayerIndex("Victory"), 1);
        mesh.transform.forward = transform.position;

        GetComponentInChildren<HealthBar>().gameObject.SetActive(false);

        Vector3 forward = mainCamera.transform.forward;
        Vector3 pos = mainCamera.transform.position;

        DOVirtual.Float(0f, 1f, 10f, (v) => {
            mainCamera.transform.forward =  Vector3.Lerp(mainCamera.transform.forward, transform.position - mainCamera.transform.position, v);
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, transform.forward + Vector3.up * 0.5f, v);
        });

        particlesRoot.GetComponentsInChildren<ParticleSystem>().ToList().ForEach(ps => {
            ps.Play();
        });

        UniTask.Delay(4000).ContinueWith(() => { SceneManager.LoadScene(0, LoadSceneMode.Single); });
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
     
    }

}
