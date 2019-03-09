using Darkages.Network.Game;
using Darkages.Network.ServerFormats;
using Darkages.Scripting;
using Darkages.Types;
using System.Collections.Generic;

namespace Darkages.Storage.locales.Scripts.Mundanes

{
    [Script("GliocaNew")]
    public class StateExample : MundaneScript
    {
        Quest quest;
        public StateExample(GameServer server, Mundane mundane)

            : base(server, mundane)
        {
            //this is called on script compile, build a quest.
            quest = new Quest();
            quest.Name = "God Stats";
            quest.StatRewards.Add(new AttrReward()
            {
                Attribute = PlayerAttr.WIS,
                Operator = new StatusOperator(StatusOperator.Operator.Add, 5),
            });
            quest.ItemRewards = new List<string>()
                        {
                             "Hy-Brasyl Boots"
                        };
            quest.LegendRewards = new List<Legend.LegendItem>()
            {
                new Legend.LegendItem()
                {
                     Category = "Class",
                     Color = (byte)LegendColor.Blue,
                      Icon = (byte)LegendIcon.Heart,
                       Value = "Glioca Worshipper"
                },
            };
        }
        public override void OnClick(GameServer server, GameClient client)
        {
            //display some options to the user
            var opts = new List<OptionsDataItem>();
            opts.Add(new OptionsDataItem(0x0001, "Tell Me About Glioca"));
            opts.Add(new OptionsDataItem(0x0002, "Who Are Your Allies and Foes?"));
            opts.Add(new OptionsDataItem(0x0004, "What Does Glioca Offer?"));
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
                    client.SendOptionsDialog(Mundane, "Glioca is the goddess of compassion. She loves all that is. Glioca is the daughter of Danaan. She sprang from Danaan. As Danaan is the sun, Glioca is the moon. She is gentle, like a swan gliding upon water. She sees the reflection of all of the tuatha in everything. Her love is complete and eternal. She does not know hate.");
                    break;
             case 0x0004:
                client.SendOptionsDialog(Mundane, "Joining out fellowship will grant you additional Wisdom for your journey.");
                break;
            case 0x0003:
                    {
                        //this would return false if they were already on the quest.
                        //or failed to accept it for any reason

                        if (client.Aisling.AcceptQuest(quest))
                        {
                            //completed it.
                            quest.OnCompleted(client.Aisling, false);

                            client.SendOptionsDialog(Mundane, "Welcome to the faith, please take this as your gift from us.");
                        }
                        else

                        {
                            //they already did it
                            client.SendOptionsDialog(Mundane, "You Already Worship A God of Temuair");
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