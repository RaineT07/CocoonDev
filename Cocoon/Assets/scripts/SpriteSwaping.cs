using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwaping : MonoBehaviour
{

    public Sprite oSprite;
    public Sprite swappedSprite;

    public bool isSwapped;


    public RuntimeAnimatorController anim1;
    public RuntimeAnimatorController anim2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isSwapped)
        {
            Debug.Log("Swapped");
            GetComponent<SpriteRenderer>().sprite = swappedSprite;
            GetComponent<Animator>().runtimeAnimatorController = anim2 as RuntimeAnimatorController;
            //gameObject.GetComponent<Animation>.controller = swappedAnimation;
        }
        else {
            GetComponent<SpriteRenderer>().sprite = oSprite;
            GetComponent<Animator>().runtimeAnimatorController = anim1 as RuntimeAnimatorController;

        }

    }
}
