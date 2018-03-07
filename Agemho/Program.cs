using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Agemho
{
    class Program
    {
        Random rnd = new Random();
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();
       
        public async Task RunBotAsync()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();
            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            string botToken = "NDIwNTY3MjMwMjI1MzE3OTA4.DYA99g.FlfAYhKX8cmvurtyi8g6wxZej1w";

            //event subscriptions
            _client.Log += Log;
            _client.UserJoined += AnnounceUserJoined;
            _client.MessageReceived += MessageRecieved;

            await RegisterCommandsAsync();

            await _client.LoginAsync(TokenType.Bot, botToken);

            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private async Task MessageRecieved(SocketMessage message)
        {
            var channel = message.Channel;
            if (message.Content.Contains("gay") || message.Content.Contains("bitch") || message.Content.Contains("Bitch") || message.Content.Contains("Fuck"))
            {
                await channel.SendMessageAsync($"Swearing is not allowed here, {message.Author.Mention}");
                await message.DeleteAsync();
            }
            if (message.Content.Contains("nigga") || message.Content.Contains("nigger"))
            {
                await channel.SendMessageAsync($"Racism is not allowed here, {message.Author.Mention}");
                await message.DeleteAsync();
            }
            if (message.Content.Contains("how are you all") || message.Content.Contains("How Are You All") || message.Content.Contains("How are you all") || message.Content.Contains("how are yall") || message.Content.Contains("How are yall") || message.Content.Contains("How Are Yall"))
            {
                await channel.SendMessageAsync($"We are fine, {message.Author.Mention}, thanks!");
            }
            if (message.Content=="hi" || message.Content == "hello" || message.Content == "yo" || message.Content.Contains("hi ") || message.Content.Contains("Hi") || message.Content.Contains("Hey") || message.Content.Contains("Yo") || message.Content.Contains("yo ") || message.Content.Contains("Hello") || message.Content.Contains("hello "))
            {
                if (!message.Author.IsBot)
                {
                    int rand = rnd.Next(5);
                    if (rand == 0)
                    {
                        await channel.SendMessageAsync($"Hi there, {message.Author.Mention}!");
                    }
                    if (rand == 1)
                    {
                        await channel.SendMessageAsync($"Yo, {message.Author.Mention}!");
                    }
                    if (rand == 2)
                    {
                        await channel.SendMessageAsync($"Hey, {message.Author.Mention}!");
                    }
                    if (rand == 3)
                    {
                        await channel.SendMessageAsync($"Hola, {message.Author.Mention}!");
                    }
                    if (rand == 4)
                    {
                        await channel.SendMessageAsync($"Hello, {message.Author.Mention}!");
                    }
                    if (rand == 5)
                    {
                        await channel.SendMessageAsync($"Hi, {message.Author.Mention}!");
                    }
                }
            }
        }
        

        private async Task AnnounceUserJoined(SocketGuildUser user)
        {
            var guild = user.Guild;
            var channel = guild.DefaultChannel;
            await channel.SendMessageAsync($"Welcome, {user.Mention}");
        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);

            return Task.CompletedTask;
        }

        public async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;

            if (message is null || message.Author.IsBot)
            {
                return;
            }

            int argPos=0;

            if(message.HasStringPrefix("!", ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var context = new SocketCommandContext(_client,message);

                var result = await _commands.ExecuteAsync(context, argPos, _services);

                if (!result.IsSuccess)
                {
                    Console.WriteLine(result.ErrorReason);
                }
            }
        }
    }
}
