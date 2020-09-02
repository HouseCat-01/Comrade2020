using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewManager : MonoBehaviour
{
    public Camera m_camera;
    public Canvas main;
    public Canvas farming;
    public Canvas housing;
    public Canvas enterprise;
    public Canvas industry;
    private Canvas currentCanvas;
    
    private void Start() {
        m_camera.transform.position = new Vector3(0, 0, -10);
        m_camera.orthographicSize = 5;
        currentCanvas = main;
        farming.enabled = false;
        housing.enabled = false;
        enterprise.enabled = false;
        industry.enabled = false;
    }
    public void MainView() {
        main.enabled = true;
        m_camera.transform.position = new Vector3(0, 0, -10);
        m_camera.orthographicSize = 5;
        currentCanvas.enabled = false;
        currentCanvas = main;
    }
    public void FarmingView() {
        farming.enabled = true;
        m_camera.transform.position = new Vector3(-7, -3.5f, -10);
        m_camera.orthographicSize = 2;
        currentCanvas.enabled = false;
        currentCanvas = farming;
    }
    public void HousingView() {
        housing.enabled = true;
        m_camera.transform.position = new Vector3(-1.2f, -2f, -10);
        m_camera.orthographicSize = 2.3f;
        currentCanvas.enabled = false;
        currentCanvas = housing;
    }
    public void EnterpriseView() {
        enterprise.enabled = true;
        m_camera.transform.position = new Vector3(4.5f, -2.9f, -10);
        m_camera.orthographicSize = 2f;
        currentCanvas.enabled = false;
        currentCanvas = enterprise;
    }
    public void IndustryView() {
        industry.enabled = true;
        m_camera.transform.position = new Vector3(8f, -2.4f, -10);
        m_camera.orthographicSize = 2f;
        currentCanvas.enabled = false;
        currentCanvas = industry;
    }
}
