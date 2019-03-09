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
using Darkages.Network.ServerFormats;
using Darkages.Scripting;
using Darkages.Types;

namespace Darkages.Storage.locales.Scripts.Spells
{
    [Script("beag ioc fein", "Dean")]
    public class beagiocfein : SpellScript
    {
        public beagiocfein(Spell spell) : base(spell)
        {
        }

        public override void OnFailed(Sprite sprite, Sprite target)
        {
        }

        public override void OnSuccess(Sprite sprite, Sprite target)
        {
        }

        public override void OnUse(Sprite sprite, Sprite target)
        {
            if (sprite is Aisling)
            {
                var client = (sprite as Aisling).Client;
                if (client.Aisling.CurrentMp >= Spell.Template.ManaCost)
                {
                    client.TrainSpell(Spell);

                    var action = new ServerFormat1A
                    {
                        Serial = sprite.Serial,
                        Number = (byte)(client.Aisling.Path == Class.Priest ? 0x80 : client.Aisling.Path == Class.Wizard ? 0x88 : 0x06),
                        Speed = 30
                    };

                    client.Aisling.CurrentMp -= Spell.Template.ManaCost;


                    foreach (var obj in client.Aisling.PartyMembers)
                    {
                        if (obj == null || obj.Dead)
                            continue;

                        obj.CurrentHp += obj.MaximumHp / 10;

                        if (obj.CurrentHp > obj.MaximumHp)
                            obj.CurrentHp = obj.MaximumHp;

                        if (client.Aisling.CurrentMp < 0)
                            client.Aisling.CurrentMp = 0;

                        if (obj.CurrentHp > 0)
                        {
                            var hpbar = new ServerFormat13
                            {
                                Serial = obj.Serial,
                                Health = (ushort)(100 * obj.CurrentHp / obj.MaximumHp),
                                Sound = 8
                            };
                            obj.Show(Scope.NearbyAislings, hpbar);
                        }

                        obj.Client.SendStats(StatusFlags.StructB);
                        client.SendAnimation(0x04, obj, client.Aisling);
                    }

                    client.Aisling.Show(Scope.NearbyAislings, action);
                    client.SendMessage(0x02, "you cast " + Spell.Template.Name + ".");
                    client.SendStats(StatusFlags.All);
                }
                else
                {
                    if (sprite is Aisling)
                    {
                        (sprite as Aisling).Client.SendMessage(0x02, ServerContext.Config.NoManaMessage);
                    }
                    return;

                }
            }
            else
            {
                sprite.CurrentHp = sprite.MaximumHp;

                var hpbar = new ServerFormat13
                {
                    Serial = sprite.Serial,
                    Health = (ushort)(100 * sprite.CurrentHp / sprite.MaximumHp),
                    Sound = 8
                };

                sprite.SendAnimation(0x04, sprite, sprite);
                sprite.Show(Scope.NearbyAislings, hpbar);
            }
        }
    }
}
