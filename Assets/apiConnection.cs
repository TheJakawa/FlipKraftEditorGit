﻿using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;


public class apiConnection : MonoBehaviour
{
    // Start is called before the first frame update
    SessionData scrData;
    void Start()
    {
        scrData = GameObject.Find("Session").GetComponent<SessionData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

    public string request(Dictionary<string, string> toAdd, string route, string method)
    {
      /*  int headerSize = 32;
        int inputSize = 64;
        byte[] buffer = new byte[(inputSize * 1000) + headerSize];
        print(scrData.access("api_address") + route);*/
        var request = (HttpWebRequest)WebRequest.Create(scrData.access("api_address") + route);
        string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(scrData.access("email") + ":" + scrData.access("pwd")));
        request.Headers.Add("Authorization", "Basic " + svcCredentials);
        request.ContentType = "application/json";
        request.Method = method;
        print(scrData.access("api_address") + route + " "+method);
        if (method.Equals("POST") || method.Equals("PUT"))
        {
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(toAdd);
                streamWriter.Write(json);
                print(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
        }
        string result;
        var httpResponse = (HttpWebResponse)request.GetResponse();
         using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
         {
             result = streamReader.ReadToEnd();
            print(result);

        }
        httpResponse.Close();
       
        return (result);
        /*  using (Stream stream = httpResponse.GetResponseStream())
          {
              if (stream != null)
              {
                  MemoryStream memStream = new MemoryStream();

                  int count;
                  while ((count = stream.Read(buffer, 0, buffer.Length)) != 0)
                  {
                      memStream.Write(buffer, 0, count);
                  }

                  memStream.Flush();
                  stream.Close();

                  memStream.Seek(0, SeekOrigin.Begin);
                  memStream.Read(buffer, 0, buffer.Length);

                  memStream.Close();
              }
          }


          string st = "";
          foreach (byte b in buffer)
              st += b;
          return Encoding.Default.GetString(buffer); ;*/
    }
   
}
