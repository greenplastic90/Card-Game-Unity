using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    public Sprite spriteFaceUp;
    public Sprite spriteFaceDown;
    public bool faceUp;
    public bool allowedToBePickedUp = true;

    public int index;

    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateSprite();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateSprite()
    {
        spriteRenderer.sprite = faceUp ? spriteFaceUp : spriteFaceDown;
    }

    [ContextMenu("Flip Card")]
    public void Flip()
    {
        faceUp = !faceUp;
        UpdateSprite();
    }
}
