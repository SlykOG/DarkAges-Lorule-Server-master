///************************************************************************
//Project Lorule: A Dark Ages Server (http://darkages.creatorlink.net/index/)
//Copyright(C) 2018 TrippyInc Pty Ltd
//
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License
//along with this program.If not, see<http://www.gnu.org/licenses/>.
//*************************************************************************/
using Darkages.Network.Game;
using Darkages.Network.ServerFormats;
using Darkages.Scripting;
using Darkages.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Darkages.Storage.locales.Scripts.Mundanes
{
    [Script("Friend", "Dean")]
    public class friend : MundaneScript
    {
        public friend(GameServer server, Mundane mundane) : base(server, mundane)
        {
        }

        public override void OnGossip(GameServer server, GameClient client, string message)
        {
            {
                if (message.Contains("Help"))
                {
                    Mundane.Show(Scope.NearbyAislings,
                        new ServerFormat0D { Text = "What do you need? Talk to me closer boy I am getting old", Serial = Mundane.Serial });
                }
            }
        }

        public override void TargetAcquired(Sprite Target)
        {
        }

        public override void OnClick(GameServer server, GameClient client)
        {
            var opts = new List<OptionsDataItem>();
            opts.Add(new OptionsDataItem(0x0001, "Tell me a bit about who you are"));
            opts.Add(new OptionsDataItem(0x0002, "Where do you recommend to hunt?"));
            opts.Add(new OptionsDataItem(0x0003, "Any tips on survival?"));

            client.SendOptionsDialog(Mundane, "You seem lost, can I help young Aisling?", opts.ToArray());
        }

        public override void OnResponse(GameServer server, GameClient client, ushort responseID, string args)
        {
            switch (responseID)
            {
                case 0x0001:
                    client.SendOptionsDialog(Mundane, "Me, I watch over the young ones...always have. The name is Aldair, I can't teach you anything myself, but I know a thing or two about magic so let me know if you need help suriving out here young Aisling.");
                    break;

                case 0x0002:
                    client.SendOptionsDialog(Mundane, "Head southwest and see if you can clean up the rat problem there. Don't be intimidated when challenges face you, be brave young Aisling.");
                    break;

                case 0x0003:
                    if (client.Aisling.CurrentHp > 0)
                    {
                        client.Aisling.ApplyBuff("buff_aite");
                        client.SendOptionsDialog(Mundane, "Take this but move quickly! It won't keep you safe for long.");
                    }
                    break;
            }
        }

    }
}
