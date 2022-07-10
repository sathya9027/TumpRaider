using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] bool Camera_Move;
    [Header("Layer Setting")]
    [SerializeField] float[] Layer_Speed = new float[7];
    [SerializeField] GameObject[] Layer_Objects = new GameObject[7];

    Transform _camera;
    float[] startPos = new float[7];
    float boundSizeX;
    float sizeX;
    float Camera_MoveSpeed;
    GameObject Layer_0;
    SurfaceEffector2D[] sf2D;
    void Start()
    {
        sf2D = FindObjectsOfType<SurfaceEffector2D>();
        _camera = Camera.main.transform;
        sizeX = Layer_Objects[0].transform.localScale.x;
        boundSizeX = Layer_Objects[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        for (int i = 0; i < 5; i++)
        {
            startPos[i] = _camera.position.x;
        }
    }

    void Update()
    {
        for (int i = 0; i < sf2D.Length; i++)
        {
            Camera_MoveSpeed=sf2D[i].speed;
        }
        //Moving camera
        if (Camera_Move)
        {
            _camera.position += Vector3.right * Time.deltaTime * Camera_MoveSpeed;
        }
        for (int i = 0; i < 5; i++)
        {
            float temp = (_camera.position.x * (1 - Layer_Speed[i]));
            float distance = _camera.position.x * Layer_Speed[i];
            Layer_Objects[i].transform.position = new Vector2(startPos[i] + distance, _camera.position.y);
            if (temp > startPos[i] + boundSizeX * sizeX)
            {
                startPos[i] += boundSizeX * sizeX;
            }
            else if (temp < startPos[i] - boundSizeX * sizeX)
            {
                startPos[i] -= boundSizeX * sizeX;
            }

        }
    }
}
