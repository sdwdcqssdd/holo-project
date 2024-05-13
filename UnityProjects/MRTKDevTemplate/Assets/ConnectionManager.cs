using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;

public class ConnectionManager : MonoBehaviour
{
    private Socket clientSocket;
    private string serverIP;
    private int serverPort;
    public bool isLock = false;
    public OutputManager outputManager;
    // Start is called before the first frame update
    void Start()
    {
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            clientSocket.Connect(serverIP, serverPort);
        }
        catch (Exception e)
        {
            Debug.LogError("An error occurred: " + e.Message);
        }
    }

    // Update is called once per frame
    public void sendData(string data)
    {
        byte[] bytes = Encoding.ASCII.GetBytes(data);
        clientSocket.Send(bytes);
        isLock = true;
    }

    public void recieveData()
    {
        byte[] bytes = new byte[1024];
        int bytesRead = clientSocket.Receive(bytes);
        outputManager.Display(Encoding.ASCII.GetString(bytes));
        isLock = false;
    }
}
