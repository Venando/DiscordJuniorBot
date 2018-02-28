using Discord;
using Discord.WebSocket;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DiscordJuniorBot {
    class Program {

        Discord​Socket​Client client;
        String TOKEN_FILE_NAME = "token.token";

        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync() {
            client = new DiscordSocketClient();
            client.Log += Log;
            client.MessageReceived += MessageReceived;
            String token = ReadToken();
            await client.LoginAsync(TokenType.Bot, ReadToken());
            await client.StartAsync();
            await Task.Delay(-1);
        }

        private async Task MessageReceived(SocketMessage msg) {
            if (msg.Content == "!ping") {
                await msg.Channel.SendMessageAsync("pong!");
            }
        }

        private Task Log(LogMessage msg) {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        public String ReadToken() {
            String directory = Directory.GetCurrentDirectory() + "\\" + TOKEN_FILE_NAME;
            if (!File.Exists(directory)) {
                Console.WriteLine("{0} does not exist.", directory);
                throw new Exception(String.Format("{0} does not exist.", directory));
            }
            using (StreamReader sr = File.OpenText(TOKEN_FILE_NAME)) {
                String input;
                while ((input = sr.ReadLine()) != null) {
                    Console.WriteLine(input);
                    return input;
                }
                throw new Exception("The file is empty.");
            }
        }

    }
}