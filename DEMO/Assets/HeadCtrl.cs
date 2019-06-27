using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeadDir
{
    Up,
    Down,
    Left,
    Right,
}

public class HeadCtrl : MonoBehaviour
{
    //移动速度，米/秒
    public float speed;

    //计时器用来记录移动时间
    private float _Timer = 0f;

    //蛇头当前的朝向
    private HeadDir _CurrentDir = HeadDir.Up;

    //蛇头改变的方向
    private HeadDir _NextDir;

    // Update is called once per frame
    private void Update()
    {
        Turn();
        Move();
    }
    //控制蛇头转向
    private void Turn()
    {
        //监听用户事件,上
        if(Input.GetKey(KeyCode.W)){
            _NextDir = HeadDir.Up;
            if (_CurrentDir == HeadDir.Down)
            {
                _NextDir = _CurrentDir;
            }  
        }
        //监听用户事件,下
        if(Input.GetKey(KeyCode.S)){
             _NextDir = HeadDir.Down;
            if (_CurrentDir == HeadDir.Up)
            {
                _NextDir = _CurrentDir;
            }  
        }
        //监听用户事件,左
        if(Input.GetKey(KeyCode.A)){
            _NextDir = HeadDir.Left;
            if (_CurrentDir == HeadDir.Right)
            {
                _NextDir = _CurrentDir;
            }  

        }
        //监听用户事件,右   
        if(Input.GetKey(KeyCode.D)){
            _NextDir = HeadDir.Right;
            if (_CurrentDir == HeadDir.Left)
            {
                _NextDir = _CurrentDir;
            }  
        }


    }
    //控制蛇头移动
    private void Move()
    {
        //将计时器累加时间增量
        _Timer += Time.deltaTime;
        if (_Timer >= (1f / speed))
        {
            //使蛇头旋转
            switch (_NextDir)
            {
                case HeadDir.Up:
                    transform.forward = Vector3.forward;
                    _CurrentDir = HeadDir.Up;
                    break;
                case HeadDir.Down:
                    transform.forward = Vector3.back;
                    _CurrentDir = HeadDir.Down;
                    break;

                case HeadDir.Left:
                    transform.forward = Vector3.left;
                    _CurrentDir = HeadDir.Left;
                    break;

                case HeadDir.Right:
                    transform.forward = Vector3.right;
                    _CurrentDir = HeadDir.Right;
                    break;

            }

            //向前移动一个单位
            transform.Translate(Vector3.forward);
            //重置计时器
            _Timer = 0f;
        }
    }
}
