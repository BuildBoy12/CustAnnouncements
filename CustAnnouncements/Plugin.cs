using System;
using EXILED;

namespace CustomAnnouncements
{
	public class Log : Plugin
	{
		public EventHandlers EventHandlers;
		public MTFRespawn Respawn;

		public const string Version = "1.0";

		public override void OnEnable()
		{
			try
			{
				Info("Loading Custom Announcements");
				Debug("Initializing event handlers..");

				EventHandlers = new EventHandlers(this);

				Events.TeamRespawnEvent += EventHandlers.OnRespawn;
				Events.PlayerJoinEvent += EventHandlers.PlayerJoin;
				Events.RoundEndEvent += EventHandlers.OnRoundEnd;
				Events.RoundStartEvent += EventHandlers.OnRoundStart;
				Events.RemoteAdminCommandEvent += EventHandlers.OnCommand;
				Events.CheckEscapeEvent += EventHandlers.CheckEscape;

				EXILED.Log.Info("Loading configs");


				Configs.ReloadConfigs();

				EXILED.Log.Info($"Custom Announcements loaded");
			}
			catch (Exception e)
			{
				Error($"There was an error loading the plugin: {e}");
			}
		}

		public override void OnDisable()
		{
			Events.TeamRespawnEvent -= EventHandlers.OnRespawn;
			Events.PlayerJoinEvent -= EventHandlers.PlayerJoin;
			Events.RoundEndEvent -= EventHandlers.OnRoundEnd;
			Events.RoundStartEvent -= EventHandlers.OnRoundStart;
			Events.RemoteAdminCommandEvent -= EventHandlers.OnCommand;
			Events.CheckEscapeEvent -= EventHandlers.CheckEscape;

			EventHandlers = null;
		}

		public override void OnReload()
		{
			
		}

		public override string getName { get; } = "Custom Announcements";
	}
}