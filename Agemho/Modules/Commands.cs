using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agemho.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        
        Random rnd = new Random();
        [Command("hey")]
        public async Task HiAsync()
        {
            await ReplyAsync("Yo!");
        }
        [Command("ping")]
        public async Task PingAsync()
        {
            await ReplyAsync("Yo!");
        }
        [Command("flipacoin")]
        public async Task FlipAsync()
        {
            int result = rnd.Next(1, 2); //1 is heads 2 is tails
            if (result == 1)
            {
                await ReplyAsync("Its Heads!");
            }
            if (result == 2)
            {
                await ReplyAsync("Its Tails!");
            }            
        }
        [Command("rolladice")]
        public async Task RollAsync()
        {
            int result = rnd.Next(1, 6); //1 is heads 2 is tails
            
                await ReplyAsync($"Its a {result}!");            
        }
        [Command("kick"), RequireOwner]
        public async Task KickAsync([Remainder]string randomShit)
        {
            foreach (var userToBeBanned in Context.Message.MentionedUsers)
            {
                var user = userToBeBanned as SocketGuildUser;
                await ReplyAsync($"{Context.User.Mention} banned {userToBeBanned}!");
                await user.KickAsync();
            }
        }
        [Command("help")]
        public async Task HelpAsync()
        {
            await ReplyAsync("Here is a list of all the commands\n\n1. !coinflip : Flips a coin and tells you the result.\n2. !rolladice : Rolls a 6 sided die and tells you the result.\n3. !kick @mention (only for admins) : Kicks a given user.\n4. !games : Lists all games by Ohmegasoft.");
        }
        [Command("games")]
        public async Task GamesAsync()
        {
            int randNo = rnd.Next(1);
            if (randNo == 0)
            {
                await ReplyAsync("These are the games by OHMEGASOFT<:ohmegasoftB:420204753029431296> :\n 1) DAPLANET(Android)\n 2) Detention(Android)\n 3) Twins(Android)\n 4) A Symphony Of Death(PC)");
            }
            else
            {
                await ReplyAsync("These are the games by OHMEGASOFT<:ohmegasoftW:420204499836338176> :\n 1) DAPLANET(Android)\n 2) Detention(Android)\n 3) Twins(Android)\n 4) A Symphony Of Death(PC)");
            }
        }

    }
}
