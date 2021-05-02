using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BlockBehavior : MonoBehaviour
{
    public Block block;
    public SpriteRenderer blockSR;
    public Rigidbody2D rgbd2D;

    public GameObject blockParticle;

    public int myCol;  
    public int myRow;
    

    public Sprite blockSprite;

    private void Awake()
    {
        block = GetComponent<Block>();
        blockSR = GetComponent<SpriteRenderer>();
        rgbd2D = GetComponent<Rigidbody2D>();
    }

    public void SetColor(int num)
    {
        blockSR.sprite = block.blocks[Random.Range(0, num)];
        blockSprite = blockSR.sprite;
    } 

    public void OnBlockEffect()
    {
        GameObject child = this.transform.Find("BlockEffect").gameObject;
        child.transform.parent = null;
        child.SetActive(true);
    }

    public Sprite GetSprite()
    {
        return blockSprite;
    }
}
