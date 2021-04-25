using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // Start is called before the first frame update
    public EntityInfo entityInfo;

    public EntityInfo getEntityInfo()
    {
        return entityInfo;
    }
}
