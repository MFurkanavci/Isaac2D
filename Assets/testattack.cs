using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testattack : MonoBehaviour
{
    Transform thyatisChild;

    void Start()
    {

        thyatisChild = gameObject.transform.Find("yarakfurkan/Thyatis");
        if (thyatisChild != null)
        {
            thyatisChild.GetComponent<Animator>().SetTrigger("Death");
        }
        else
        {
            Debug.Log("Belirtilen child bulunamadı.");
        }
    }


}
