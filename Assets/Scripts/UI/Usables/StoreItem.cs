using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "StoreItem", menuName = "Objects/StoreItem")]
public class Item : ScriptableObject
{
    public float value;
    public Sprite photo;
    public float price;
}
