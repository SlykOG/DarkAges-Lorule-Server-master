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
using Darkages.Storage.locales.debuffs;
using Darkages.Types;
using System;

namespace Darkages.Storage.locales.Scripts.Skills
{
    [Script("Wolf Fang Fist", "Dean")]
    public class Wff : SkillScript
    {
        private readonly Random rand = new Random();
        public Skill _skill;

        public Wff(Skill skill) : base(skill)
        {
            _skill = skill;
        }

        public override void OnFailed(Sprite sprite)
        {
            if (sprite is Aisling)
            {
                var client = (sprite as Aisling).Client;

                client.SendMessage(0x02,
                    !string.IsNullOrEmpty(Skill.Template.FailMessage) ? Skill.Template.FailMessage : "failed.");
            }
        }

        public override void OnSuccess(Sprite sprite)
        {
            if (sprite is Aisling)
            {
                var client = (sprite as Aisling).Client;

                var debuff = new debuff_frozen();

                var i = sprite.GetInfront(sprite);

                if (i == null || i.Count == 0)
                {
                    client.SendMessage(0x02, "you have embarrassed yourself.");
                    return;
                }


                foreach (var target in i)
                {
                    if (target is Money)
                        continue;
                    if (target is Item)
                        continue;

                    target.RemoveDebuff("sleep");
                    target.RemoveDebuff("frozen");

                    if (!target.HasDebuff(debuff.Name))
                    {
                        Apply(client, debuff, target);
                        return;
                    }
                }
            }
        }

        private void Apply(GameClient client, Debuff debuff, Sprite target)
        {

            var action = new ServerFormat1A
            {
                Serial = client.Aisling.Serial,
                Number = 0x02,
                Speed = 40
            };
            client.Aisling.Show(Scope.NearbyAislings, action);

            target.ApplyDamage(client.Aisling, 0, false, Skill.Template.Sound);
            debuff.OnApplied(target, debuff);
        }

        public override void OnUse(Sprite sprite)
        {
            if (!Skill.Ready)
                return;

            if (sprite is Aisling && Skill.Ready)
            {
                var client = (sprite as Aisling).Client;
                client.TrainSkill(Skill);

                if (client.Aisling != null && !client.Aisling.Dead)
                {
                    var success = Skill.RollDice(rand);

                    if (success)
                        OnSuccess(sprite);
                    else
                        OnFailed(sprite);
                }
            }
            else
            {
                var target = sprite.Target;
                if (target == null)
                    return;

                if (target is Aisling)
                {
                    var debuff = new debuff_frozen();
                    {
                        if (!target.HasDebuff(debuff.Name))
                            Apply((target as Aisling)?.Client, debuff, target);
                    }
                }
            }
        }
    }
}
