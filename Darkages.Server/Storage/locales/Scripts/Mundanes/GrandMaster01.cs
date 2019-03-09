﻿///************************************************************************
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
using System.Linq;

namespace Darkages.Storage.locales.Scripts.Mundanes
{
    [Script("The Grand Master")]
    public class GrandMaster01 : MundaneScript
    {
        public GrandMaster01(GameServer server, Mundane mundane)
            : base(server, mundane)
        {
        }

        public override void TargetAcquired(Sprite Target)
        {
        }

        public override void OnGossip(GameServer server, GameClient client, string message)
        {
        }
        public override void OnClick(GameServer server, GameClient client)
        {
            client.SendOptionsDialog(Mundane, "\nHello, How may i help you?",
                new OptionsDataItem(0x0001, "Learn Secret"),
                new OptionsDataItem(0x0002, "Forget Secret")
                );
        }

        public override void OnResponse(GameServer server, GameClient client, ushort responseID, string args)
        {
            switch (responseID)
            {
                // Skill Learn
                case 0x0001:
                    var skills = ServerContext.GlobalSpellTemplateCache.Select(i => i.Value)
                        .Where(i => i.Prerequisites != null && i.NpcKey != null && i.NpcKey.ToLower().Equals(this.Mundane.Template.Name.ToLower())
                        && i.Prerequisites.Class_Required == client.Aisling.Path).ToList();
                    var learned_skills = client.Aisling.SpellBook.Spells.Where(i => i.Value != null).Select(i => i.Value.Template)
                        .ToList();

                    var new_skills = skills.Except(learned_skills).ToList();

                    new_skills = new_skills.OrderBy(i =>
                        Math.Abs(i.Prerequisites.ExpLevel_Required - client.Aisling.ExpLevel)).ToList();


                    if (new_skills.Count > 0)
                    {
                        client.SendSpellLearnDialog(Mundane, "Only the dedicated can unlock the power of magic.", 0x0003,
                            new_skills.Where(i => i.Prerequisites.Class_Required == client.Aisling.Path));
                    }
                    else
                    {
                        client.CloseDialog();
                        client.SendMessage(0x02, "I have nothing to teach for now.");
                        return;
                    }
                    break;
                case 0x0002:
                    {
                        client.SendSpellForgetDialog(Mundane, "What must you vanish from your mind?\nMake sure you understand, This cannot be un-done.", 0x9000);
                    }
                    break;
                case 0x9000:
                    {
                        var idx = -1;
                        int.TryParse(args, out idx);

                        if (idx < 0 || idx > byte.MaxValue)
                        {
                            client.SendMessage(0x02, "Go away.");
                            client.CloseDialog();
                        }

                        client.Aisling.SpellBook.Remove((byte)idx);
                        client.Send(new ServerFormat18((byte)idx));

                        client.SendSpellForgetDialog(Mundane, "It is gone, Shall we cleanse more?\nRemember, This cannot be un-done.", 0x9000);

                    }
                    break;
                // Skill Confirmation
                case 0x0003:
                    client.SendOptionsDialog(Mundane, "Are you sure you want to learn " + args + "?\nDo you wish to expand your mind? Let me check if you're ready.", args,
                        new OptionsDataItem(0x0006, string.Format("What does {0} do?", args)),
                        new OptionsDataItem(0x0004, "Yes"),
                        new OptionsDataItem(0x0001, "No"));
                    break;
                case 0x0004:
                    {

                        var subject = ServerContext.GlobalSpellTemplateCache[args];
                        if (subject == null)
                            return;

                        if (subject.Prerequisites == null)
                        {
                            client.CloseDialog();
                            client.SendMessage(0x02, ServerContext.Config.CantDoThat);
                            return;
                        }
                        else
                        {
                            var conditions = subject.Prerequisites.IsMet(client.Aisling, (msg, result) =>
                            {
                                if (!result)
                                {
                                    client.SendOptionsDialog(Mundane, msg, subject.Name);
                                }
                                else
                                {
                                    client.SendOptionsDialog(Mundane, "You have satisfied my requirements, Ready for such magic?",
                                        subject.Name,
                                        new OptionsDataItem(0x0005, "Yes, I'm Ready."),
                                        new OptionsDataItem(0x0001, "No, I'm not ready."));
                                }
                            });
                        }
                    }
                    break;
                case 0x0006:
                    {
                        var subject = ServerContext.GlobalSpellTemplateCache[args];
                        if (subject == null)
                            return;

                        client.SendOptionsDialog(Mundane, string.Format("{0} - {1}", args, string.IsNullOrEmpty(subject.Description) ? "No more information is available." : subject.Description) + "\n" + subject.Prerequisites.ToString(),
                            subject.Name,
                            new OptionsDataItem(0x0006, string.Format("What does {0} do?", subject.Name)),
                            new OptionsDataItem(0x0004, "Yes"),
                            new OptionsDataItem(0x0001, "No"));
                    }
                    break;
                // Skill Acquire
                case 0x0005:
                    {
                        var subject = ServerContext.GlobalSpellTemplateCache[args];
                        if (subject == null)
                            return;

                        client.SendAnimation(109, client.Aisling, Mundane);
                        client.LearnSpell(Mundane, subject, "Go Spark, Aisling.");

                    }
                    break;
            }
        }
    }
}
