using System;
using System.Collections.Generic;
using System.Linq;
using AQ3D_Commander.API;

using Newtonsoft.Json;
using UnityEngine;

namespace AQ3D_Commander
{
    public class Commander : MonoBehaviour
    {
        public static Commander Instance;
        public static List<JoinCommand> JoinCommands;

        private UIInputChat chatInput;

        public static void Load()
        {
            new GameObject().AddComponent<Commander>().name = "Commander";
            CacheCommands();
        }

        public static async void CacheCommands()
        {
            var data = await SaryionAPI.SendReq(APITypes.JOINCOMMANDS);
            if (data == null) return;
            
            JoinCommands = JsonConvert.DeserializeObject<List<JoinCommand>>(data);
        }

        private void Awake()
        {
            Instance = this;
        }

        private void LateUpdate()
        {
            if (Chat.Instance != null && chatInput == null)
            {
                chatInput = Chat.Instance.chatInput;
                chatInput.onChatSubmit += CommandHandler;
            }
        }

        public static void CommandHandler(string msg)
        {
            if (!msg.StartsWith("/")) return;
            
            string[] array = msg.Split(new [] {' '}, StringSplitOptions.RemoveEmptyEntries);
            string cmd = array[0].Remove(0, 1);
            string[] args = array.Skip(1).ToArray();
            
            switch (cmd)
            {
                case "join":
                    
                    JoinCommandHandler(string.Join(" ", args).ToLower());
                    break;
            }
        }
        
        public static void JoinCommandHandler(string cmd)
        {
            if (!JoinCommandExists(cmd)) return;

            JoinCommand command = GetJoinCommand(cmd);
            Game.Instance.SendAreaJoinRequest(command.JoinID);
        }

        public static bool JoinCommandExists(string cmd)
        {
            return JoinCommands.Exists(c => c.Command == cmd);
        }

        public static JoinCommand GetJoinCommand(string cmd)
        {
            return JoinCommands.Find(c => c.Command == cmd);
        }
    }
}