using System;
using System.Text;
using nMqtt.Messages;

namespace nMqtt.Test
{
    class Program
    {
        static MqttClient _client;

        static void Main(string[] args)
        {

            _client = new MqttClient("127.0.0.1", "d");
            _client.OnMessageReceived += OnMessageReceived;
            _client.ConnectAsync().Wait();


            _client.Subscribe("/World");

            while (Console.ReadLine() != "c")
            {
                _client.Publish("/World", Encoding.UTF8.GetBytes("测试发送消息"), Qos.AtLeastOnce);
            }

            Console.ReadKey();
        }

        static void OnMessageReceived(MqttMessage message)
        {
            switch (message)
            {
                case ConnAckMessage msg:
                    Console.WriteLine("---- OnConnAck");
                    _client.Subscribe("/World");
                    break;

                case SubscribeAckMessage msg:
                    Console.WriteLine("---- OnSubAck");
                    break;

                case PublishMessage msg:
                    Console.WriteLine("---- OnMessageReceived");
                    Console.WriteLine(@"topic:{0} data:{1}", msg.TopicName, Encoding.UTF8.GetString(msg.Payload));
                    break;

                default:
                    break;
            }
        }
    }
}