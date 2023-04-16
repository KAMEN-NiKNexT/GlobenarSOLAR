using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    [SerializeField] private GameObject _classicCamera;
    [SerializeField] private GameObject _arCamera;
    [SerializeField] private GameObject _arSession;
    [SerializeField] private bool _isAR;


    public void Click()
    {
        if (!_isAR)
        {
            _classicCamera.gameObject.SetActive(false);
            _arCamera.gameObject.SetActive(true);
            _arSession.gameObject.SetActive(true);
            _isAR = true;
        }
        else
        {
            _classicCamera.gameObject.SetActive(true);
            _arCamera.gameObject.SetActive(false);
            _arSession.gameObject.SetActive(false);
            _isAR = false;
        }
    }
}
