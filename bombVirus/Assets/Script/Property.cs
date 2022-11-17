using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum propertiesType
{
    AddHP,
    AddSpeed,
    AddBomb,
    AddRange
}

[System.Serializable]
public class PropertyType_Sprite
{
    public propertiesType type;
    public Sprite sp;
}

public class Property : MonoBehaviour
{
    public PropertyType_Sprite[] propertyType_Sprites;
    private SpriteRenderer spriteRenderer;
    private propertiesType propertiesType;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag(ManageTags.bombAnimation))
        {
            GetComponent<Collider2D>().isTrigger = true;
            int propertiesIndedx = Random.Range(0, propertyType_Sprites.Length);
            spriteRenderer.sprite = propertyType_Sprites[propertiesIndedx].sp;
            propertiesType = propertyType_Sprites[propertiesIndedx].type;
        }
        // determine the player hits property or not;
        if (collision.CompareTag(ManageTags.Player))
        {
            PlayerController plc = collision.gameObject.GetComponent<PlayerController>();
            print(propertiesType);
            switch (propertiesType)
            {
                case propertiesType.AddHP:
                    
                    plc.HP ++ ;
                    print(plc.HP);
                    break;
                case propertiesType.AddBomb:
                    plc.bombNumber ++;
                    print(plc.bombNumber);
                    break;
                case propertiesType.AddRange:
                    plc.bombingRange++;
                    print(plc.bombingRange);
                    break;
                case propertiesType.AddSpeed:
                    print(plc.moveSpeed);
                    if (plc.moveSpeed > 0.2f)
                    {
                        plc.moveSpeed = 0.2f;
                    }
                    plc.moveSpeed += 0.03f;
                    break;
                default:
                    break;
            }
            Destroy(gameObject);
        }

    }
}
