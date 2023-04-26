using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructObjects : MonoBehaviour
{
    public Transform _brokenParts;
    public List<GameObject> parts = new List<GameObject>();
    public float _force = 100f;
    Dictionary<GameObject,Vector3> _objectInfos = new Dictionary<GameObject,Vector3>();
    private void Awake()
    {
        foreach (Transform item in _brokenParts)
        {
            parts.Add(item.gameObject);
            _objectInfos.Add(item.gameObject, item.transform.localPosition);
        }
    }
    bool _isBroken = false;
    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            _isBroken = true;
        }
        if(_isBroken)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            _brokenParts.gameObject.SetActive(true);
            foreach (var item in parts)
            {               
                item.SetActive(true);
                //item.transform.rotation = Quaternion.Euler(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
                item.GetComponent<Rigidbody>().AddExplosionForce(_force,_brokenParts.transform.position,5f);
                item.GetComponent<Collider>().isTrigger = true;
            }
            StartCoroutine(WaitSomeTime());
            _isBroken = false;
        }
        if(Input.GetKeyDown(KeyCode.A) || _isBroken)
        {
            RePositionObject();
        }
    }
    public void DestroyGameObjects()
    {
        _isBroken = true;
    }
    public void RePositionObject()
    {
        _brokenParts.gameObject.SetActive(false);
        foreach (var item in parts)
        {
            if (_objectInfos.ContainsKey(item))
            {
                item.transform.localPosition = _objectInfos[item];
                item.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                item.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                item.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
    }

    IEnumerator WaitSomeTime()
    {
        yield return new WaitForSeconds(2f);
        RePositionObject();
    }
}
/*
 * objeleri yok et
 * daha sonra objeleri orijinal posizyonuna getir puzzle gibi
 * ve objeyi en alta taþý
 */
