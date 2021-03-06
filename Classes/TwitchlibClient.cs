using System;
using System.Collections.Generic;

using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;


namespace TTSBot
{
    
    class Bot
    {

        public TwitchClient client = null;
        public TwitchTTSBotSettingsManager botSettingManager = new TwitchTTSBotSettingsManager();
        // Buffer containing all messages to be processed externally.
        public Queue<OnMessageReceivedArgs> messageBuffer = new Queue<OnMessageReceivedArgs>();

        private ConnectionCredentials credentials;

        public Bot()
        {

            credentials = new ConnectionCredentials(botSettingManager.settings.botName, botSettingManager.settings.botOAuthKey);
            var clientOptions = new ClientOptions
            {
                MessagesAllowedInPeriod = 750,
                ThrottlingPeriod = TimeSpan.FromSeconds(30)
            };
            WebSocketClient customClient = new WebSocketClient(clientOptions);
            client = new TwitchClient(customClient);

        }

        public bool Connect()
        {

            var clientOptions = new ClientOptions
            {
                MessagesAllowedInPeriod = 750,
                ThrottlingPeriod = TimeSpan.FromSeconds(30)
            };
            WebSocketClient customClient = new WebSocketClient(clientOptions);
            client = new TwitchClient(customClient);


            client.Initialize(credentials, botSettingManager.settings.defaultJoinChannel);

            client.OnLog += Client_OnLog;
            client.OnJoinedChannel += Client_OnJoinedChannel;
            client.OnMessageReceived += Client_OnMessageReceived;
            client.OnWhisperReceived += Client_OnWhisperReceived;
            client.OnNewSubscriber += Client_OnNewSubscriber;
            client.OnConnected += Client_OnConnected;

            return client.Connect();

        }

        private void Client_OnLog(object sender, OnLogArgs e)
        {
            Console.WriteLine($"{e.DateTime.ToString()}: {e.BotUsername} - {e.Data}");
        }

        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            Console.WriteLine($"Connected to {e.AutoJoinChannel}");
        }

        private void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            try
            {
                if (botSettingManager.settings.settingDictionary["displayConnectionMessage"])
                {
                    client.SendMessage(e.Channel, String.Format("{0}'s TTSBot Has Connected to This Channel!", (char.ToUpper(botSettingManager.settings.botAdminUserName[0]) + botSettingManager.settings.botAdminUserName.Substring(1))));
                }
            }
            catch { }
        }

        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {

            messageBuffer.Enqueue(e);

        }

        private void Client_OnWhisperReceived(object sender, OnWhisperReceivedArgs e)
        {
            if (e.WhisperMessage.Username == "my_friend")
                client.SendWhisper(e.WhisperMessage.Username, "Hey! Whispers are so cool!!");
        }

        private void Client_OnNewSubscriber(object sender, OnNewSubscriberArgs e)
        {
            if (e.Subscriber.SubscriptionPlan == SubscriptionPlan.Prime)
                client.SendMessage(e.Channel, $"Welcome {e.Subscriber.DisplayName} to the substers! You just earned 500 points! So kind of you to use your Twitch Prime on this channel!");
            else
                client.SendMessage(e.Channel, $"Welcome {e.Subscriber.DisplayName} to the substers! You just earned 500 points!");
        }


    }

}

