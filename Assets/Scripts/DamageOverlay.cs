using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class DamageOverlay : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEngine.UI.Image overlayImage;
    public UnityEngine.UI.Image smallerImage;
    public UnityEngine.UI.Image red;
    private float r;
    private float g;
    private float b;
    private float a = 0;

    private float r_small;
    private float g_small;
    private float b_small;
    private float a_small = 0;

    private float r_red;
    private float g_red;
    private float b_red;
    private float a_red = 0;
    void Start()
    {
        r = overlayImage.color.r;
        g = overlayImage.color.g;
        b = overlayImage.color.b;
        a = overlayImage.color.a;

        r_red = red.color.r;
        g_red = red.color.g;
        b_red = red.color.b;
        a_red = red.color.a;

        r_small = smallerImage.color.r;
        g_small = smallerImage.color.g;
        b_small = smallerImage.color.b;
        a_small = smallerImage.color.a;
        AdjustColor(0f, 0f);
    }

    public void AdjustColor(float transparency_large, float transparency_small)
    {
        
        Color c = new Color(r, g, b, transparency_large);
        overlayImage.color = c;
        Color d = new Color(r_small, g_small, b_small, transparency_small);
        smallerImage.color = d;
        Color e = new Color(r_red, g_red, b_red, transparency_small);
        red.color = e;
    }
    // Update is called once per frame
    public void setObjectInactive()
    {
        gameObject.SetActive(false);
    }

}
