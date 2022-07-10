using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetFPS : MonoBehaviour
{
    [SerializeField] FPSDropdown GameFPS;

    enum FPSDropdown
    {
        _30FPS,
        _60FPS,
        _120FPS,
        _144FPS,
        _165FPS
    }

    private void Update()
    {
        switch (GameFPS)
        {
            case FPSDropdown._30FPS:
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = 30;
                break;
            case FPSDropdown._60FPS:
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = 60;
                break;
            case FPSDropdown._120FPS:
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = 120;
                break;
            case FPSDropdown._144FPS:
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = 144;
                break;
            case FPSDropdown._165FPS:
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = 165;
                break;
            default:
                QualitySettings.vSyncCount = 1;
                break;
        }
    }
}
