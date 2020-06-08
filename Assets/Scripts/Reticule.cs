using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticule : MonoBehaviour
{
    public pointer m_Pointer;

    public SpriteRenderer m_CircleRenderer;

    public Sprite m_OpenSprite;
    public Sprite m_ClosedSprite;

    private Camera m_Camera = null;


    private void Awake()
    {
        Debug.Log("Redicule Awake");
        m_Pointer.OnPointerUpdate += UpdateSprite;
        m_Camera = Camera.main;
    }


    private void OnDestroy()
    {
        m_Pointer.OnPointerUpdate -= UpdateSprite;
    }

    private void UpdateSprite(Vector3 point, GameObject hitObject)
    {
        Debug.Log("UpdateSprite");
        transform.position = point;

        if (hitObject)
        {
            m_CircleRenderer.sprite = m_ClosedSprite;
        }
        else
        {
            m_CircleRenderer.sprite = m_OpenSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {

        transform.LookAt(m_Camera.gameObject.transform);
    }
}
