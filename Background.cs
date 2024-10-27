using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float speed;
    public int StratIndex, endIndex;
    public Transform[] sprites;

    float viewHeight;
    private void Awake ()
    {
        viewHeight = Camera.main.orthographicSize * 2;
    }
    void Update()
    {
        Vector3 curPos = transform.position;
        Vector3 nextPos = Vector3.down * speed * Time.deltaTime;
        transform.position = curPos + nextPos;

        if(sprites[endIndex].position.y < viewHeight*(-1)) 
        {
            Vector3 backSpritePos = sprites[StratIndex].localPosition;
            Vector3 frontSpritePos = sprites[endIndex].localPosition;
            sprites[endIndex].transform.localPosition = backSpritePos + Vector3.up * 10 ;

            int StratIndexSave = StratIndex;
            StratIndex = endIndex;
            endIndex = (StratIndexSave-1 == -1) ? sprites.Length-1 : StratIndexSave-1 ;
        }
    }
}
