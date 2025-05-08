using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class StarterScript : MonoBehaviour
{
    [SerializeField] GameObject myGate;

    [SerializeField] GameObject fadeIn;

    [SerializeField] AudioSource Sus;

    [SerializeField] GameObject ammoDisplay;
    public static int fiveSevenAmmoCount = 20;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(MySequence());
    }

    // Update is called once per frame
    void Update()
    {
        ammoDisplay.GetComponent<TMPro.TMP_Text>().text = "" + fiveSevenAmmoCount;

    }
    public void OpenGate()
    {
        Sus.Play();
        myGate.GetComponent<Animator>().Play("GateSwing");
    }

   
    IEnumerator MySequence()
    {
        yield return new WaitForSeconds(3f);
        fadeIn.SetActive(false);
        OpenGate();
        yield return new WaitForSeconds(30f);
        // Make sure the object is active *before* setting parameters
        fadeIn.SetActive(true);

        Animator animator = fadeIn.GetComponent<Animator>();
        animator.SetFloat("FadeMultiplier", -1f);
        animator.Play("FadeIn", 0, 1f); // Start at the end

    }
}
