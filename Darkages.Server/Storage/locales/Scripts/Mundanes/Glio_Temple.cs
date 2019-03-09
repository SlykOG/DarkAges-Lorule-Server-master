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
    [Script("OldGlioca", "Dean")]
    public class glioca : MundaneScript
    {
        public glioca(GameServer server, Mundane mundane) : base(server, mundane)
        {
        }

        public override void OnGossip(GameServer server, GameClient client, string message)
        {
            {
                if (message.Contains("Praise Glioca"))
                {
                    {
                        var script = ScriptManager.Load<SpellScript>("armachd", Spell.Create(3, ServerContext.GlobalSpellTemplateCache["armachd"]));
                        {
                            script.OnUse(Mundane, client.Aisling);
                        }
                        Mundane.Show(Scope.NearbyAislings,
                        new ServerFormat0D { Text = "Thou Art Our Savior!", Serial = Mundane.Serial });
                    }
                }
            }
        }

        public override void TargetAcquired(Sprite Target)
        {
        }

        public override void OnClick(GameServer server, GameClient client)
        {
            var opts = new List<OptionsDataItem>();
            opts.Add(new OptionsDataItem(0x0001, "Why Do You Worship Glioca?"));
            opts.Add(new OptionsDataItem(0x0002, "Who Are You Allies and Foes?"));
            opts.Add(new OptionsDataItem(0x0003, "I Swear My Faith to Glioca."));

            client.SendOptionsDialog(Mundane, "Welcome to the Glioca Temple, would you worship with us?", opts.ToArray());
        }

        public override void OnResponse(GameServer server, GameClient client, ushort responseID, string args)
        {
            switch (responseID)
            {
                case 0x0001:
                    client.SendOptionsDialog(Mundane, "Glioca is the goddess of compassion. She loves all that is. Glioca is the daughter of Danaan. She sprang from Danaan. As Danaan is the sun, Glioca is the moon. She is gentle, like a swan gliding upon water. She sees the reflection of all of the tuatha in everything. Her love is complete and eternal. She does not know hate.");
                    break;

                case 0x0002:
                    client.SendOptionsDialog(Mundane, "Glioca's allies are Deoch and Cail. Glioca's enemies are Ceannlaidir and Gramail.");
                    break;
                case 0x0003:
                    if (client.Aisling.LegendBook.LegendMarks.Any(i => i.Value == "Glioca Worshipper"))
                    {
                        //user has legend
                    }
                    {
                            client.Aisling.LegendBook.AddLegend(new Legend.LegendItem
                        {
                            Category = "Class",
                            Color = (byte)LegendColor.Blue,
                            Icon = (byte)LegendIcon.Victory,
                            Value = string.Format("Glioca Worshipper")
                        });
                        client.SendOptionsDialog(Mundane, "Welcome to the faith. Please don't click on this again this shit is broken atm you'll just keep fuckin getting legend marks. In fact, how the fuck do I stop that shit, can someone please figure that out ask me for the code on discord we gotta somehow reference the aislings legend and look for any type of god mark and prevent it");
                        break;
                    }
            }
        }

    }
}
