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
namespace Darkages.Types
{
    public class ClientGameSettings
    {
        public string EnabledSettingStr, DisabledSettingStr;
        public bool Enabled { get; set; }

        public ClientGameSettings(string lpEnabledStr, string lpDisabledStr, bool state = false)
        {
            EnabledSettingStr = lpEnabledStr;
            DisabledSettingStr = lpDisabledStr;
            Enabled = state;
        }

        public void Toggle()
        {
            Enabled = !Enabled;
        }
    }
}
