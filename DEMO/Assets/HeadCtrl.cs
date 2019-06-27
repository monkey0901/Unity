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
    //食物预设体
    public GameObject FoodPrefab;
    
    //身子预设体
    public GameObject BodyPrefab;
    
    //第一节身子的引用
    private Body _FirstBody;
    private Body _LastBody;

    //移动速度，米/秒 
    public float speed;
    
    //计时器用来记录移动时间
    private float _Timer = 0f;

    //蛇头当前的朝向
    private HeadDir _CurrentDir = HeadDir.Up;

    //蛇头改变的方向
    private HeadDir _NextDir;

    private bool _IsOver = false;

    public void CreateFood()
    {
        float x = Random.Range(-9.5f, 9.5f);
        float z = Random.Range(-9.5f, 9.5f);
        GameObject obj = Instantiate(FoodPrefab,new Vector3 (x, 0f, z), Quaternion.identity) as GameObject;
    }


    //触发事件，当触发开始时
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Bound"))
        {
            _IsOver = true;

        }
        if(other.tag.Equals("Food"))
        {
            Destroy(other.gameObject);
            Grow();
            CreateFood();
        }
    }
    private void Grow()
    {
        //动态创建身子
        GameObject obj = Instantiate(BodyPrefab, new Vector3(1000f, 1000f, 1000f), Quaternion.identity);
        //获取身子上的body脚本
        Body b = obj.GetComponent<Body>();
        if(_FirstBody == null)
        {
            //当前创建的身子就是第一节身子
            _FirstBody = b;
        }
        if(_LastBody != null)
        {
            //新创建的身子设置在最后一节身子后面
            _LastBody.next = b;
        }
        //更新最后一节身子
        _LastBody = b;
    }
    private void Start ()
    {
        //游戏开始时需要有一个食物
        CreateFood();
    }
    // Update is called once per frame
    private void Update()
    {
        if (!_IsOver)
        {
            Turn();
            Move();
        }
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
            //记录头部移动之前的位置
            Vector3 nextPos = transform.position;
            //向前移动一个单位
            transform.Translate(Vector3.forward);
            //重置计时器
            _Timer = 0f;
            //如果有身子
            if(_FirstBody != null)
            {
                //就让第一节身子移动
                _FirstBody.MOve(nextPos);
            }
        }
    }
}
