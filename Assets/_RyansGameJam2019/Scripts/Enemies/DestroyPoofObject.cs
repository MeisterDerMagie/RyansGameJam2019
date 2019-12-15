using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RGJ{
public class DestroyPoofObject : MonoBehaviour
{
    public void DestroyPoof()
    {
        Destroy(gameObject);
    }
}
}