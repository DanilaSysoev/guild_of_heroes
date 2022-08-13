using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceVisualizer : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    
    public void SetResource(ITileResource resource)
    {
        if(resource != null)
            spriteRenderer.sprite = resource.Sprite;
    }

    public void HideResource()
    {
        spriteRenderer.enabled = false;
    }
    public void ShowResource()
    {
        spriteRenderer.enabled = true;
    }
}
