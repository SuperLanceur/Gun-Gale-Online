using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO.Ports;

public class HeartRateManager : MonoBehaviour
{
    public Text heartRate;
    SerialPort serialPort = new SerialPort("COM4", 115200);

    private void Awake()
    {
        heartRate = GetComponentInChildren<Text>();
        heartRate.text = "111";
        serialPort.Open();
        serialPort.ReadTimeout = 1;
    }

    private void Update()
    {
        if (serialPort.IsOpen)
        {
            try
            {
                heartRate.text = serialPort.ReadLine();
            }
            catch(System.Exception)
            {
                
            }
        }
    }
	
}
