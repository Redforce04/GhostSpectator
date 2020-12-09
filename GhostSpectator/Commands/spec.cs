﻿using System;

using Exiled.API.Features;
using CommandSystem;

namespace GhostSpectator.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    class spec : ICommand
    {
        public string Command => "spec";

        public string[] Aliases => new string[] { };

        public string Description => "Switches from ghost to normal spectator mode and vice versa.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (GhostSpectator.Singleton.Config.GhostSpecSwap == false)
            {
                response = "This command is disabled.";
                return false;
            }
            Player Ply = Player.Get(((CommandSender)sender).Nickname);
            if (Ply == null)
            {
                response = "The command speaker is not a player.";
                return false;
            }
            if (API.IsGhost(Ply))
            {
                Ply.SetRole(RoleType.Spectator);
            }
            else
            {
                if (Ply.IsAlive)
                {
                    response = "This command cannot be used to change from an alive player to a ghost.";
                    return false;
                }
                API.GhostPlayer(Ply);
            }
            response = "Success";
            return true;
        }
    }
}
