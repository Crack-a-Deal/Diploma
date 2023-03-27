using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IYellow
{
    void Move(Vector3 minPosition, Vector3 maxPosition);
    IEnumerator Movable();
}
