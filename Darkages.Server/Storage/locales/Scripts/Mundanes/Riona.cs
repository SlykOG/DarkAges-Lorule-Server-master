using Darkages.Network.Game;
using Darkages.Network.ServerFormats;
using Darkages.Scripting;
using Darkages.Types;
using System.Collections.Generic;

namespace Darkages.Storage.locales.Scripts.Mundanes

{
    [Script("Slyk")]
    public class Slyk : MundaneScript
    {
        Quest quest;
        public Slyk (GameServer server, Mundane mundane)

            : base(server, mundane)
        {
            //this is called on script compile, build a quest.
            quest = new Quest();
            quest.Name = "Stick n' Swing";
            quest.ItemRewards = new List<string>()
                        {
                             "Stick"
                        };
            quest.SkillRewards = new List<string>()
                        {
                             "Assail"
                        };
        }
        public override void OnClick(GameServer server, GameClient client)
        {
            //display some options to the user
            var opts = new List<OptionsDataItem>();
            opts.Add(new OptionsDataItem(0x0001, "Riona's Greeting"));
            opts.Add(new OptionsDataItem(0x0002, "How Can I Survive Here?"));

            client.SendOptionsDialog(Mundane, "Welcome to Mileth Inn!", opts.ToArray());

        }
        public override void OnResponse(GameServer server, GameClient client, ushort responseID, string args)
        {
            switch (responseID)
            {
                case 0x0001:
                    client.SendOptionsDialog(Mundane, "I hope you had a good rest young Aisling! Last I heard you were having dreams of outlandish creatures and magic unheardof. Fear not, you're home in Temuair and adventure awaits! What year is it? Why, you don't remeber? time stopped in Deoch 32 when Law destroyed the Aislings fighting to break his Seals.");
                    break;
                case 0x0002:
                    {
                        //this would return false if they were already on the quest.
                        //or failed to accept it for any reason

                        if (client.Aisling.AcceptQuest(quest))
                        {
                            //completed it.
                            quest.OnCompleted(client.Aisling, false);

                            client.SendOptionsDialog(Mundane, "I can only teach you how to Assail, this skill should fend off some of your enemies until you can choose a path and become stronger! Oh, and take this old stick, you'll need it more than me.");
                        }
                        else

                        {
                            //they already did it
                            client.SendOptionsDialog(Mundane, "I've tought you everything I know. Trust in yourself now.");
                        }
                    }
                    break;
                default: break;
            }
        }
        public override void OnGossip(GameServer server, GameClient client, string message)
        {
        }
        public override void TargetAcquired(Sprite Target)
        {
        }
    }
}