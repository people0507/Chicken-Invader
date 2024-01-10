using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NuclearController : MonoBehaviour
{
    [SerializeField] Text textNuclear;
    private int nuclear;
    public static NuclearController instance;

    private void Awake()
    {
        instance = this;
    }

    public void getNuclear(int nuclear)
    {
        this.nuclear = nuclear;
        textNuclear.text = this.nuclear.ToString();
    }
}
