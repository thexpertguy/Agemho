using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agemho.Modules
{
    class Hi : ModuleBase<SocketCommandContext>
    {
        [Command("hi")]
        public async Task HiAsync()
        {
            await ReplyAsync("Hey!");
        }
    }
}
