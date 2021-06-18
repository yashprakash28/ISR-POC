using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour
{
    // Start is called before the first frame update

    public Slider sliderLeft;
    public Slider sliderRight;
    private float xvalue;
    private float yvalue;
    bool Flage = false;
    public Material DefaultDaySky;
    public Button Mode_Button;
    public Sprite LightMode, DarkMode;

    bool dis = false;

    float Limit = 1f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActiveObj(GameObject obj)
    {
        // bool Active = false;
        if (obj.activeInHierarchy == false)
        {
            obj.SetActive(true);
        }
        else
            obj.SetActive(false);
    }

    public void changeDistanceLeftCube(GameObject _Cube)
    {

            _Cube.GetComponent<Rigidbody>().constraints = (~RigidbodyConstraints.FreezePositionX) | (~RigidbodyConstraints.FreezePositionY) | RigidbodyConstraints.FreezePositionZ |
                                                           RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            dis = true;
         
    }

    public void LeftSlider(GameObject leftCube)
    {
        leftCube.gameObject.transform.position = new Vector3(sliderLeft.value, 2.5f, 0f);
    }

    public void RightSlider(GameObject rightCube)
    {
        // rightCube.transform.position = new Vector3(rightCube.transform.position.x - sliderLeft.value * Limit / sliderLeft.value, 1.399f ,0f);
        // yvalue = sliderRight.value * Limit;
        rightCube.gameObject.transform.position = new Vector3(-sliderRight.value, 2.5f, 0f);
    }

    public void OnClick_Mode(Material Sky)
    {
        if(Flage)
        {
            RenderSettings.skybox = Sky;
            Mode_Button.image.sprite = LightMode;
        }
        else 
        {
            RenderSettings.skybox = DefaultDaySky;
            Mode_Button.image.sprite = DarkMode;
        }

        Flage = !Flage;
    }
}
