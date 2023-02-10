using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsGraphics : MonoBehaviour
{
    [SerializeField] private List<StatGraphics> statsGraphics = new List<StatGraphics>();

    public void Setup(Product product)
    {
        statsGraphics[0].Setup("Badassery: ", product.Badassery);
        statsGraphics[1].Setup("Coolness: ", product.Coolness);
        statsGraphics[2].Setup("Cuteness: ", product.Cuteness);
    }

    public void Setup(int badassery, int coolness, int cuteness)
    {
        statsGraphics[0].Setup("Badassery: ", badassery);
        statsGraphics[1].Setup("Coolness: ", coolness);
        statsGraphics[2].Setup("Cuteness: ", cuteness);
    }
}
