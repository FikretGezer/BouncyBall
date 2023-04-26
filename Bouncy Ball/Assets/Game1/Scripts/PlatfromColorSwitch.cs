using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatfromColorSwitch : MonoBehaviour
{
    public GameObject platform;
    public GameObject[] platforms;
    

    Color _colorGreen, _colorYellow, _colorRed;
    List<Color> colors = new List<Color>();

    public float speed = 1f;

    
    Renderer platformRenderer;
    

    int _baseColorHash;

    private void OnEnable()
    {
        BallController.onTouchedPlatform += ChangeColorAndRandom;
        //BallController.onTouchedPlatform += ChangeColor;
    }
    private void OnDisable()
    {
        BallController.onTouchedPlatform -= ChangeColorAndRandom;
        //BallController.onTouchedPlatform -= ChangeColor;
    }
    private void Awake()
    {
        _colorGreen = Color.green;
        _colorYellow = Color.yellow;
        _colorRed = Color.red;

        colors.Add(_colorGreen);
        colors.Add(_colorYellow);
        colors.Add(_colorRed);

        _baseColorHash = Shader.PropertyToID("_BaseColor");

        platformRenderer = platform.GetComponent<Renderer>();
        platformRenderer.sharedMaterial.SetColor(_baseColorHash,_colorGreen);

        //foreach (var item in platforms)
        //{
        //    item.GetComponent<Renderer>().material.SetColor(_baseColorHash, colors[Random.Range(0, colors.Count)]);
        //}
       
    }
    Color _randomColor=new Color();
    Color _tempColor=new Color();
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ChangeColorAndRandom();        
            //ChangeColor();
        }
        platform.transform.Rotate(Vector3.up * speed * Time.deltaTime);
        foreach (var item in platforms)
        {
            item.transform.Rotate(Vector3.up * speed * Time.deltaTime);
        }
    }
    private void ChangeColorAndRandom()
    {
        while (true)
        {
            _tempColor = colors[Random.Range(0, colors.Count)];
            if (_randomColor != _tempColor)
            {
                _randomColor = _tempColor;
                platformRenderer.sharedMaterial.SetColor(_baseColorHash, _randomColor);
                speed = -speed;
                break;
            }
        }
    }
    int _colorCount=1;
    private void ChangeColor()
    {
        _tempColor = colors[_colorCount];
        platformRenderer.sharedMaterial.SetColor(_baseColorHash, _tempColor);
        if (_colorCount < colors.Count - 1)
            _colorCount++;
        else
            _colorCount = 0;

    }
}
