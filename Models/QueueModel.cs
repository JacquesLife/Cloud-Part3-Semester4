using System;
using Azure.Storage.Queues.Models;
using Azure;
using Azure.Storage.Queues;

namespace WebApplication1.Models;

public class QueueModel
{
    public string Message { get; set; }
    public List<string> Messages { get; set; }
}
