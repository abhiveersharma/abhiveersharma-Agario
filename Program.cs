using System.Drawing;
using System.Text;
using System.Text.Json;
using AgarioModels;
using Communications;
using Microsoft.Extensions.Logging.Abstractions;

/// <summary>
/// Simple console application that allows us to deserialize food from the Agario server
/// </summary>
Networking? channel = new Networking(NullLogger.Instance, onConnect,onDisconnect,onMessage,'\n');
channel.Connect("localhost", 11000);
channel.ClientAwaitMessagesAsync();
Console.ReadLine();

void onMessage(Networking channel, string message)
{
    if (message.Substring(0,Protocols.CMD_Food.Length) == Protocols.CMD_Food)
    {
        string food_items = message[Protocols.CMD_Food.Length..]!; //Get the rest of the message without the command prefix
        string[] splitFood = food_items.Split(',');
        //List<Food> list = JsonSerializer.Deserialize<List<Food>>(message[Protocols.CMD_Food.Length..]);

        Food[]? result = JsonSerializer.Deserialize<Food[]>(food_items);

        int counter = 0;
        while (counter < 10)
        {
            Console.WriteLine($"X:{result[counter].X } Y:{result[counter].Y } Color:{result[counter].ARGBcolor } ");
            counter++;

        }

        
    }
  
}
channel.Disconnect();

 void onDisconnect(Networking channel)
{
    return;
}

 void onConnect(Networking channel)
{
    // INFO: Handshake 1 - Send "Phase1" to finalize connection
    channel.Send("Phase1");
}

class Food
{
    private int x;
    public int X { get; set; }
    private int y;
    public int Y { get; set; }
    private Color _ARGBcolor;
    public Color ARGBcolor { get; set; }
    private float mass;
    public float Mass { get; set; }
}




