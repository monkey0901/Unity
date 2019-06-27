using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    //下一个身子的引用
    public Body next;

    //使当前身子移动
    public void MOve(Vector3 pos)
    {
        //来记录移动前的位置
        Vector3  nextPos = transform.position;
        //移动当前身子
        transform.position = pos;
        //如果后面还有身子
        if (next != null)
        {
            //移动后面的身子
            next.MOve(nextPos);
        }
    }
}
