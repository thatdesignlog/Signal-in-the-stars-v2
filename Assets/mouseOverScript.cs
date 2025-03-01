using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class mouseOverScript : MonoBehaviour
{
    // Start is called before the first frame update
    //When the mouse hovers over the GameObject, it turns to this color (yellow)
    Color m_MouseOverColor = Color.yellow;

    //This stores the GameObject’s original color
    Color m_OriginalColor;

    //Get the GameObject’s mesh renderer to access the GameObject’s material and color
     TextMeshProUGUI textMesh;

    void Start()
    {
        //Fetch the mesh renderer component from the GameObject
        textMesh = GetComponent<TextMeshProUGUI>();
        //Fetch the original color of the GameObject
        m_OriginalColor = textMesh.color;
    }

    public void OnMouseOver()
    {
        // Change the color of the GameObject to red when the mouse is over GameObject
        textMesh.color = m_MouseOverColor;
    }

    public void OnMouseExit()
    {
        // Reset the color of the GameObject back to normal
        textMesh.color = m_OriginalColor;
    }

    public void resetColor()
    {
        textMesh.color = m_OriginalColor;
    }
}
