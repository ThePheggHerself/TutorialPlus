﻿using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerRoles;
using PluginAPI.Core;

namespace TutorialPlus
{
	public class Events
	{
		[PluginEvent(ServerEventType.Scp096AddingTarget)]
		public bool New096Target(Scp096AddingTargetEvent args)
		{
			if (!Plugin.Config.TutorialTrigger096 && args.Target.Role == RoleTypeId.Tutorial)
			{
				if (Plugin.Config.Debug)
					Log.Debug($"Plugin blocked player {args.Target.Nickname} from becoming target of SCP096 {args.Player.Nickname}");
				return false;
			}
			else return true;
		}

		[PluginEvent(ServerEventType.Scp173NewObserver)]
		public bool New173Target(Scp173NewObserverEvent args)
		{
			if (!Plugin.Config.TutorialObserve173 && args.Target.Role == RoleTypeId.Tutorial)
			{
				if (Plugin.Config.Debug)
					Log.Debug($"Plugin blocked player {args.Target.Nickname} from becoming observer of SCP173 {args.Player.Nickname}");
				return false;
			}
			else return true;
		}

		[PluginEvent(ServerEventType.PlayerDamage)]
		public bool PlayerDamaged(PlayerDamageEvent args)
		{
			if (Plugin.Config.TutorialGodmode && args.Target.Role == RoleTypeId.Tutorial && !args.Target.TemporaryData.Contains("tfix_disablegodmode"))
			{
				if (Plugin.Config.Debug)
					Log.Debug($"Plugin blocked player {args.Target.Nickname} from taking damage from {args.Player.Nickname}");
				return false;
			}
			else return true;
		}

		[PluginEvent(ServerEventType.PlayerChangeRole)]
		public void PlayerChangeRole(PlayerChangeRoleEvent args)
		{
			if(args.NewRole == RoleTypeId.Tutorial && args.ChangeReason == RoleChangeReason.RemoteAdmin)
			{
				if(Plugin.Config.TutorialBypass)
				{
					if (Plugin.Config.Debug)
						Log.Debug($"Plugin enabled bypass mode for {args.Player.Nickname}");
					args.Player.ReferenceHub.serverRoles.BypassMode = true;
				}

				if (Plugin.Config.TutorialGodmode)
				{
					if (Plugin.Config.Debug)
						Log.Debug($"Plugin enabled godmode for {args.Player.Nickname}");
					args.Player.ReferenceHub.characterClassManager.GodMode = true;
				}
			}
			else if(args.OldRole.RoleTypeId == RoleTypeId.Tutorial)
			{
				if (Plugin.Config.TutorialBypass)
				{
					if (Plugin.Config.Debug)
						Log.Debug($"Plugin disabled bypass mode for {args.Player.Nickname}");
					args.Player.ReferenceHub.serverRoles.BypassMode = false;
				}

				if (Plugin.Config.TutorialGodmode)
				{
					if (Plugin.Config.Debug)
						Log.Debug($"Plugin disabled godmode for {args.Player.Nickname}");
					args.Player.ReferenceHub.characterClassManager.GodMode = false;
				}
			}
		}
	}
}
