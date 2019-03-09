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
using Darkages.Storage;
using Darkages.Types;
using Newtonsoft.Json;

namespace Darkages
{
    public class ServerConstants
    {
        [JsonProperty] public bool AssailsCancelSpells = true;

        [JsonProperty] public string BadRequestMessage = "(Invalid Request)";

        [JsonProperty] public string SomethingWentWrong = "Something went wrong.";

        [JsonProperty]
        /// <summary>
        /// WHat is the starting base armor class?
        /// </summary>
        public byte BaseAC = 0;


        [JsonProperty]
        public byte MaxAC  = 99;


        [JsonProperty] public byte BaseMR = 70;

        [JsonProperty] public byte BaseStatAttribute = 3;

        [JsonProperty] public bool CancelCastingWhenWalking = true;

        [JsonProperty] public string CantCarryMoreMsg = "You can't carry more.";

        [JsonProperty] public string CantDoThat = "You can't do that.";

        [JsonProperty] public string CantDropItemMsg = "You can't drop that.";

        [JsonProperty] public string CantEquipThatMessage = "You can't wear that.";

        [JsonProperty] public string CantUseThat = "You can't use that.";

        [JsonProperty]
        /// <summary>
        /// Prefix, Suffix for Spell chants.
        /// by default, using *
        /// </summary>
        public string ChantPrefix = "*";

        [JsonProperty] public string ChantSuffix = "*";

        [JsonProperty] public int ClickLootDistance = 3;

        [JsonProperty] public string ConAddedMessage = "You become more fit.";

        [JsonProperty] public int ConnectionCapacity = 300;

        [JsonProperty]
        /// <summary>
        /// In seconds, what is the daytime interval
        /// </summary>
        public double DayTimeInterval = 30.0;

        [JsonProperty]
        /// <summary>
        /// What is the Health Lost when restoring Life?
        /// This is a penalty value. and should reflect the type of game play difficulty.
        /// USDA uses 50. so this is the default value.
        /// </summary>
        public int DeathHPPenalty = 50;

        [JsonProperty] public int DeathMap = 88888;

        [JsonProperty] public bool DebugMode;

        [JsonProperty] public ItemColor DefaultItemColor = ItemColor.blue;

        [JsonProperty] public uint DefaultItemDurability = 1000;

        [JsonProperty] public uint DefaultItemValue = 500;

        [JsonProperty]
        /// <summary>
        /// Default Security Encryption Key.
        /// This is currently the MD5 Hash of our client.
        /// we will use it later to determine if the client has been modified
        /// </summary>
        public string DefaultKey = "73F2BE80DDEB1BFDC887AB1C3CA18365";

        [JsonProperty] public string DexAddedMessage = "You feel more flexible.";

        [JsonProperty] public string DoesNotFitMessage = "That does not fit you.";

        [JsonProperty] public bool DontTurnDuringRefresh;

        [JsonProperty] public int ERRORCAP = 5;

        [JsonProperty]
        /// <summary>
        /// Ms between 0 cooldown based skills.
        /// </summary>
        public double GlobalBaseSkillDelay = 450;

        [JsonProperty]
        /// <summary>
        /// What is the rate we invoke the Monolith?
        /// Monster Templates in the end control the spawn rates,
        /// But this controls how often we spin up the templates.
        /// By default,and to keep things lowcpu usage, i would not go below 1000ms.
        /// </summary>
        public double GlobalSpawnTimer = 2000.0f;

        [JsonProperty] public string GroupRequestDeclinedMsg = "noname does not wish to join your group.";

        [JsonProperty]
        /// <summary>
        /// Client Handshake message.
        /// This can be anything stating with C, and ending with \n.
        /// </summary>
        public string HandShakeMessage = "CAN WE ALL GET ALONG\n";

        [JsonProperty] public int HelperMenuId = -1;

        [JsonProperty] public string HelperMenuTemplateKey = "Lorule Helper";

        [JsonProperty] public string IntAddedMessage = "Your mind expands.";

        [JsonProperty] public string LevelUpMessage = "You have reached level {0}";

        [JsonProperty]
        /// <summary>
        /// How long should an aisling linger around, before we declare them as logged-in?
        /// </summary>
        public double LingerState = 1000;

        [JsonProperty] public int MaxCarryGold = 100000000;

        [JsonProperty]
        /// <summary>
        /// How many seconds should we clear message bar for aislings?
        /// This timer will commence 5 seconds after the last message was sent.
        /// By default, 10 mimics the USDA rate.
        /// </summary>
        public double MessageClearInterval = 12;

        [JsonProperty]
        /// <summary>
        /// What is the Lowest HP an aisling can reach under any circumstances?
        /// </summary>
        public int MinimumHp = 50;

        [JsonProperty] public int MonsterSpellSuccessRate = 30;

        [JsonProperty]
        /// <summary>
        /// This controls how often we check for dead mundanes,
        /// and controls when to spin up there templates for respawning.
        /// Mundanes are not supposed to die much, so i would keep it at 3.0
        /// to keep server usage down. but you can probably go as high as 60.0.
        /// this value is in seconds.
        /// </summary>
        public double MundaneRespawnInterval = 10.0;

        [JsonProperty] public string NotEnoughGoldToDropMsg = "You wish you had that much.";

        [JsonProperty]
        /// <summary>
        /// In Seconds, How often should we ping the client?
        /// this by default is every 35 seconds.
        /// this also controls the auto-save function.
        /// and auto-saves will also occur on this interval.
        /// </summary>
        public double PingInterval = 10.0;

        [JsonProperty] public bool RefreshOnWalkCollision = true;

        [JsonProperty]
        /// <summary>
        /// What is the time between aisling f5ing?
        /// </summary>
        public int RefreshRate = 300;

        [JsonProperty]
        /// <summary>
        /// What is the Regen Rate Expononent Modifier?
        /// 0.15 is about the same rate as USDA.
        /// </summary>
        public int RegenRate = 15000;

        [JsonProperty]
        /// <summary>
        /// In Seconds, How often should we have active aislings?
        /// </summary>
        public double SaveRate = 25;

        [JsonProperty] public int SERVER_PORT = 2615;

        [JsonProperty] public string SERVER_TITLE = "Slyk";

        [JsonProperty]
        public string ServerWelcomeMessage = "Danaan calls your name, and you open your eyes....";

        [JsonProperty] public int StartingMap = 888;

        [JsonProperty] public Position StartingPosition = new Position(26, 48);

        [JsonProperty]
        /// <summary>
        /// Maximum Server Capacity for stat attributes
        /// Recommended to keep it below 255.
        /// </summary>
        public byte StatCap = 255;

        [JsonProperty] public string StrAddedMessage = "You become stronger.";

        [JsonProperty] public string ToWeakToLift = "You are to weak to even lift it.";

        [JsonProperty] public short TransitionPointX = 3;

        [JsonProperty] public short TransitionPointY = 3;

        [JsonProperty] public int TransitionZone = 9999;

        [JsonProperty] public string UserDroppedGoldMsg = "noname has dropped some money nearby.";

        [JsonProperty] public int VeryNearByProximity = 6;

        [JsonProperty] public double WeightIncreaseModifer = 3.5;

        [JsonProperty] public string WisAddedMessage = "Your will increases.";

        [JsonProperty] public int WithinRangeProximity = 12;

        [JsonProperty] public string YouDroppedGoldMsg = "you dropped some gold.";

        [JsonProperty] public int LootTableStackSize = 3;

        [JsonProperty] public int HpGainFactor = 5;

        [JsonProperty] public int MpGainFactor = 5;

        [JsonProperty] public int StatsPerLevel = 2;

        [JsonProperty] public string NoManaMessage = "Your will is too weak.";

        [JsonProperty] public double FasNadurStrength = 3.00;

        [JsonProperty] public double MorFasNadurStrength = 4.50;

        [JsonProperty] public string CantAttack = "Can't Attack.";

        [JsonProperty] public int SkullLength = 10;

        [JsonProperty] public double GroupExpBonus = 5.0;

        [JsonProperty] public int PlayerLevelCap = 99;

        [JsonProperty]
        public Position[] Alters = new Position[]
        {
            new Position(31, 52, 1001),
            new Position(31, 53, 1001),
        };

        [JsonProperty]
        public string[] GlobalScripts = new string[]
        {
            "Tut",
            "Reactors",
            "WishingWell"
        };

        [JsonProperty]
        public string CursedItemMessage = "You reach for it, But something holds you back.";

        [JsonProperty]
        public GameSetting[] Settings = new GameSetting[]
        {
            new GameSetting("Loot Mode  :Single", "Loot Mode  :Multi", true),
            new GameSetting("PVP  :ON", "PVP  :OFF", true),
            new GameSetting("AUTO LOOT GOLD  :Toggle", "AUTO LOOT GOLD  :Toggled", false)
        };

        public struct GameSetting
        {
            public string SettingOn, SettingOff;
            public bool Enabled;

            public GameSetting(string _SettingOn, string _SettingOff, bool _Enabled = false)
            {
                SettingOn = _SettingOn;
                SettingOff = _SettingOff;
                Enabled = _Enabled;
            }
        }

        public override string ToString()
        {
            return StorageManager.Save(this)
                   ?? string.Empty;
        }
    }
}
