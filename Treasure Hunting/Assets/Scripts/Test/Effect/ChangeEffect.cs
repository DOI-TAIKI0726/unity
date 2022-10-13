using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject buff;
    [SerializeField]
    private GameObject confetti;
    [SerializeField]
    private GameObject Kirakira;
    [SerializeField]
    private int Count;
    // Start is called before the first frame update
    void Start()
    {
        buff = GameObject.Find("Buff");
        confetti = GameObject.Find("Confetti");
        Kirakira = GameObject.Find("KiraKira");

        Count = 0;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("t"))
        {
            Count++;
            if (Count >= 3)
            {
                Count = 0;
            }
        }
        switch (Count)
        {
            case 0:
                buff.SetActive(true);
                confetti.SetActive(false);
                Kirakira.SetActive(false);
                break;
            case 1:
                buff.SetActive(false);
                confetti.SetActive(true);
                Kirakira.SetActive(false);
                break;
            case 2:
                buff.SetActive(false);
                confetti.SetActive(false);
                Kirakira.SetActive(true);
                break;
        }
    }
}
