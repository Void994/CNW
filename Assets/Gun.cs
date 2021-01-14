using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{

    public float damage = 10f;
    public float range = 100f;

    public int maxAmmo = 5;
    private int currentAmmo;
    public float reloadTime = 10f;
    private bool isReloading = false;

    public MoveCamera fpsCam;
    public ParticleSystem Shots;

    public Animator animator;

    void Start() 
    {
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading)
           return;
        if (currentAmmo <= 0) 
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        IEnumerator Reload () 
        {
            isReloading = true;
            Debug.Log("Reloading..");

            animator.SetBool("Reloading", true);

            yield return new WaitForSeconds(reloadTime);
           

            currentAmmo = maxAmmo;
            isReloading = false;
         if (isReloading == false) 
         {animator.SetBool("Reloading", false);};
        }

        void Shoot()
        {
            Shots.Play();

            currentAmmo--;

            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
               Debug.Log(hit.transform.name);

	       Target target = hit.transform.GetComponent<Target>();
	       if (target != null) 
	       {
              
	          target.TakeDamage(damage);
	       }
	    }	
    
	}

    }
}
