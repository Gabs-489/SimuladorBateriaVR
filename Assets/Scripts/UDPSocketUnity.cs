using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class UDPSocketUnity : UDPSocket
{
    private BalanceBoardSensor unityScript;

    public void Server(string address, int port, BalanceBoardSensor script)
    {
        unityScript = script;
        base.Server(address, port);
    }

    public override void Receive()
    {
        _socket.BeginReceiveFrom(state.buffer, 0, bufSize, SocketFlags.None, ref epFrom, recv = (ar) =>
        {
            State so = (State)ar.AsyncState;
            SocketError errorCode;
            int bytes = _socket.EndReceive(ar, out errorCode);
            
            if (errorCode != SocketError.Success)
            {
                bytes = 0;
            }
            else
            {
                // Procesar datos recibidos
                string msg = Encoding.ASCII.GetString(so.buffer, 0, bytes);
                string[] msg_parts = msg.Split(':');
                
                if (msg_parts.Length == 2)
                {
                    switch (msg_parts[0])
                    {
                        case "rTL":
                            unityScript.rwTopLeft = double.Parse(msg_parts[1]);
                            break;
                        case "rTR":
                            unityScript.rwTopRight = double.Parse(msg_parts[1]);
                            break;
                        case "rBL":
                            unityScript.rwBottomLeft = double.Parse(msg_parts[1]);
                            break;
                        case "rBR":
                            unityScript.rwBottomRight = double.Parse(msg_parts[1]);
                            break;
                        case "timestamp":
                            monitor_latency(msg_parts);
                            break;
                    }
                }
            }
            
            _socket.BeginReceiveFrom(so.buffer, 0, bufSize, SocketFlags.None, ref epFrom, recv, so);
        }, state);
    }
    
    void monitor_latency(string[] msg_parts)
    {
        double curr_ms = DateTime.Now.TimeOfDay.TotalMilliseconds;
        double received_ms = Double.Parse(msg_parts[1]);
        double latency = curr_ms - received_ms;
        
        unityScript.latency = latency;
        
        if (latency < unityScript.minlatency) unityScript.minlatency = latency;
        if (latency > unityScript.maxlatency) unityScript.maxlatency = latency;
        
        if (latency < 0)
        {
            unityScript.display($"Latencia imposible: {latency}");
        }
        
        if (unityScript.latency_count < 2)
        {
            unityScript.avglatency = latency;
            if (unityScript.latency_count == 1)
            {
                unityScript.avg_ms_per_reading = curr_ms - unityScript.prev_ms;
            }
        }
        else
        {
            unityScript.avglatency = ((unityScript.avglatency * (unityScript.latency_count - 1)) + latency) / unityScript.latency_count;
            unityScript.avg_ms_per_reading = ((unityScript.avg_ms_per_reading * (unityScript.latency_count - 1)) + (curr_ms - unityScript.prev_ms)) / unityScript.latency_count;
        }
        
        unityScript.prev_ms = curr_ms;
        unityScript.latency_count += 1;
    }
}