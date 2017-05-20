using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{

  List<GameObject> _objects = new List<GameObject>();

  public float _maxZoom;
  public float _minZoom;
  public float _speed;

  float _original_zoom = 0;

  // Use this for initialization
  void Start()
  {
    _original_zoom = GetComponent<Camera>().orthographicSize;
  }

  void AddObject(GameObject obj)
  {
    _objects.Add(obj);
  }

  // Update is called once per frame
  void Update()
  {
    var mid = FindMidPoint();
    var zoom = GetZoom();

    var x = Mathf.Lerp(gameObject.transform.position.x, mid.x, Time.deltaTime * _speed);
    var y = Mathf.Lerp(gameObject.transform.position.y, mid.y, Time.deltaTime * _speed);

    GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, zoom, Time.deltaTime * _speed);

    gameObject.transform.position = new Vector3(x, y, -10);
  }

  Vector3 FindMidPoint()
  {
    Vector3 mid = new Vector3();

    foreach (var obj in _objects)
    {
      mid += obj.transform.position;
    }

    mid /= _objects.Count;

    return mid;
  }

  float GetZoom()
  {
    float zoom = GreatestDistance();

    zoom /= _maxZoom;

    zoom *= _original_zoom;

    if (zoom > _maxZoom) zoom = _maxZoom;
    if (zoom < _minZoom) zoom = _minZoom;

    return zoom;
  }

  float GreatestDistance()
  {
    float greatest = 0;

    for(int i = 0; i < _objects.Count; ++i)
    {
      for(int j = 0; j < _objects.Count; ++j)
      {
        if (i >= j) continue;

        var dist = Vector3.Distance(_objects[i].transform.position, _objects[j].transform.position);

        if (dist > greatest) greatest = dist;
      }
    }

    return greatest;
  }
}
