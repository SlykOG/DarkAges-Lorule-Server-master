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
using System.Collections.Generic;

namespace Darkages.Assets.locales.Scripts.Mundanes
{
    [Script("Mundane Reactor Example")]
    public class MundaneReactorExample : MundaneScript
    {
        Reactor reactor;

        public int Wis { get; private set; }

        public MundaneReactorExample(GameServer server, Mundane mundane) : base(server, mundane)
        {
        }

        public override void OnClick(GameServer server, GameClient client)
        {
            reactor = new Reactor()
            {
                CallBackScriptKey = null,
                CallerType = ReactorQualifer.Reactor,
                CallingReactor = "Example Reactor 2",
                ScriptKey = "Example Reactor 2",
                Name = "Example Reactor 2",
                Sequences = new List<DialogSequence>()
                    {
                        new DialogSequence()
                        {
                            HasOptions = false,
                            CanMoveBack = false,
                            CanMoveNext = true,
                            DisplayText = "Finally, You're awake!, What are you still doing here? Have you not heard what happened last night?",
                            DisplayImage = (ushort)Mundane.Template.Image,
                            Title = "Wren",
                        },
                        new DialogSequence()
                        {
                            HasOptions = false,
                            CanMoveBack = true,
                            CanMoveNext = true,
                            DisplayText = "All i heard was it has something to do with Crovax, The Barron Lord.",
                            DisplayImage = (ushort)Mundane.Template.Image,
                            Title = "Wren",
                        },
                        new DialogSequence()
                        {
                            HasOptions = true,
                            CanMoveBack = true,
                            CanMoveNext = true,
                            DisplayText = "You better go assemble outside with the other residents and group up.",
                            DisplayImage = (ushort)Mundane.Template.Image,
                            Title = "Wren",
                        },
                        new DialogSequence()
                        {
                            HasOptions   = true,
                            CanMoveBack  = true,
                            CanMoveNext  = true,
                            DisplayText  = "Let me know what you find out. I'll stay here and warn other who may come along...",
                            DisplayImage = (ushort)Mundane.Template.Image,
                            OnSequenceStep     = SequenceCompletedCallback,
                            Title = "Wren"
                        },
                    },
                Quest = new Quest()
                {
                    Name = "Wren's Quest",
                    ExpRewards = new List<uint>()
                        {
                            1000, 1000, 1000
                        },
                    GoldReward = 50000,
                    LegendRewards = new List<Legend.LegendItem>()
                        {
                            new Legend.LegendItem()
                            {
                               Category = "Quest",
                               Color = (byte)LegendColor.Blue,
                               Icon = (byte)LegendIcon.Community,
                               Value = "Earned Wren's Respect",
                            },
                        },
                    ItemRewards = new List<string>()
                        {
                             "Stick",
                             "Wooden Shield"
                        },
                }
            };

            reactor.Decorator = ScriptManager.Load<ReactorScript>(reactor.ScriptKey, reactor);
            if (reactor != null && reactor.Decorator != null)
                reactor.Decorator.OnTriggered(client.Aisling);
        }

        public override void OnGossip(GameServer server, GameClient client, string message)
        {

        }

        public override void OnResponse(GameServer server, GameClient client, ushort responseID, string args)
        {
            switch (responseID)
            {
                case 0x0010: 
                    {
                        client.SendOptionsDialog(Mundane, "You can attack by using space bar. or by pressing the 's' key and clicking on 'Assail', Also notice how 'Assail' is in the first slot? this means you can press the '1' to activate it. The numpad also works too.\nHave you forgotten everything i taught you? Jesus christ we got a bright one here boys.");
                    }
                    break;
                case 0x0011: 
                    {
                        client.CloseDialog();
                    }
                    break;
                case 0x0012:
                    {
                        client.Aisling.TutorialCompleted = true;
                        client.Aisling.ExpLevel = 11;
                        client.Aisling._Str = ServerContext.Config.BaseStatAttribute;
                        client.Aisling._Int = ServerContext.Config.BaseStatAttribute;
                        client.Aisling._Wis = ServerContext.Config.BaseStatAttribute;
                        client.Aisling._Con = ServerContext.Config.BaseStatAttribute;
                        client.Aisling._Dex = ServerContext.Config.BaseStatAttribute;
                        client.Aisling._MaximumHp = (ServerContext.Config.MinimumHp + 33) * 11;
                        client.Aisling._MaximumMp = (ServerContext.Config.MinimumHp + 21) * 11;

                        client.Aisling.StatPoints = 11 * ServerContext.Config.StatsPerLevel;
                        client.SendStats(StatusFlags.All);

                        client.SendMessage(0x02, "You have lost all memory...");
                        client.TransitionToMap(1006, new Position(2, 4));
                        client.Aisling.TutorialCompleted = true;
                    } break;
            }
        }

        public override void TargetAcquired(Sprite Target)
        {

        }

        public void SequenceCompletedCallback(Aisling a, DialogSequence b)
        {
            if (b.HasOptions)
            {
                var subject = a.Quests.Find(i => i.Name == "awakening");

                if (subject != null && subject.Completed)
                {
                    a.Client.SendOptionsDialog(Mundane, "You look all set, Please let me know what you find out what's happening...",
                        new OptionsDataItem(0x0010, "How do i attack?")
                    );
                }
                else
                {
                    a.Client.SendOptionsDialog(Mundane, "Don't tell me you forgot where your stuff is? try the chest. You probably locked it up.");
                    return;
                }

                subject = a.Quests.Find(i => i.Name == "practice makes perfect");

                if (subject != null && subject.Completed)
                {
                    a.Client.SendOptionsDialog(Mundane, string.Format("Thank you {0}, I have a secret way out of this tutorial if you desire!", a.Username),
                        new OptionsDataItem(0x0012, "Skip Tutorial (Advanced Users Only)")
                    );
                }
                else
                {
                    a.Client.SendOptionsDialog(Mundane, "I said go kill these rats, what are you sitting around for?");
                    return;
                }
            }
        }
    }
}
