using System;
using System.IO;
using EXILED;
using System.Collections.Generic;
using MEC;

namespace CustomAnnouncements
{
	public class EventHandlers
	{
		public Log plugin;
		public EventHandlers(Log plugin) => this.plugin = plugin;

		public string list;
		public string ebic;
		public string deathCauseInput;
		public string deathCause;

		public string joinMsg = "Build hasentered";
		public string joinPlayer = "76561198801824014@steam";

		public string chsUsage = "<br>" + "USAGE: chs (view/play)";
		public string reUsage = "<br>" + "USAGE: (view/play)";
		public string rsUsage = "<br>" + "USAGE: (view/play)";
		public string deUsage = "<br>" + "USAGE: (view/play)";
		public string seUsage = "<br>" + "USAGE: (view/play)";
		public string mtfaUsage = "<br>" + "USAGE: mtfa (scps left) (mtf number) (mtf letter)";
		public string scpaUsage = "<br>" + "USAGE: scpa (scp number) (death type)";

		public string success = "Announcement made successfully.";
		public string denied = "Permission ca_main required.";

		public string jannouncement;
		public string jplayer;

		public void OnRespawn(ref TeamRespawnEvent ev)
		{
			if (ev.IsChaos && Configs.enableCustomChaos == true && Configs.chaosMessage != "") 
			{
				PlayerManager.localPlayer.GetComponent<MTFRespawn>().RpcPlayCustomAnnouncement(Configs.chaosMessage, false, true);
				Plugin.Info("Running Chaos Insurgency announcement.");
			}
			else if(ev.IsChaos && Configs.enableCustomChaos == true && Configs.chaosMessage == "")
			{
				Plugin.Info("A message must be set for ca_CHSMessage for it to function");
			}
			else if (ev.IsChaos && Configs.enableCustomChaos != true && Configs.chaosMessage != "")
			{
				Plugin.Info("ca_EnableCHS must be set to true for the message to occur");
			}
		}

		public void PlayerJoin(PlayerJoinEvent ev)
		{
			Dictionary<string, string> playerAnnoun = CustAnnouncements.PreConf.GetDictonaryValue(Plugin.Config.GetString("ca_JoinMsgs", ""));
			
			if (string.IsNullOrEmpty(ev.Player.characterClassManager.UserId) || ev.Player.characterClassManager.IsHost || ev.Player.nicknameSync.MyNick == "Dedicated Server")
				return;
			if (Configs.enableCustJoinMsg == true && joinMsg != "")
			{
				string e = playerAnnoun[ev.Player.characterClassManager.UserId];
				if (e != null)
				{
					PlayerManager.localPlayer.GetComponent<MTFRespawn>().RpcPlayCustomAnnouncement(e, false, false);
					Plugin.Info("Player Join announcement played successfully");
					return;
				}
			}

			else if(ev.Player.characterClassManager.UserId == joinPlayer && Configs.enableCustJoinMsg != true && joinMsg != "")
			{
				Plugin.Info("ca_EnableJM must be set to true for the message to occur");
			}
		}

		public void OnRoundEnd()
		{
			if (Configs.enableOnRoundEnd == true && Configs.oreMessage != "")
			{
				PlayerManager.localPlayer.GetComponent<MTFRespawn>().RpcPlayCustomAnnouncement(Configs.oreMessage, false, false);
			}
			else if (Configs.enableOnRoundEnd == true && Configs.oreMessage == "")
			{
				Plugin.Info("A message must be set for ca_REMessage for it to function");
			}
			else if (Configs.enableOnRoundEnd != true && Configs.oreMessage != "")
			{
				Plugin.Info("ca_EnableRE must be set to true for the message to occur");
			}
		}

		public void OnRoundStart()
		{
			if (Configs.enableOnRoundStart == true && Configs.orsMessage != "")
			{
				PlayerManager.localPlayer.GetComponent<MTFRespawn>().RpcPlayCustomAnnouncement(Configs.orsMessage, false, false);
			}
			else if (Configs.enableOnRoundStart == true && Configs.orsMessage == "")
			{
				Plugin.Info("A message must be set for ca_RSMessage for it to function");
			}
			else if (Configs.enableOnRoundStart != true && Configs.orsMessage != "")
			{
				Plugin.Info("ca_EnableRS must be set to true for the message to occur");
			}
		}
		
		public void CheckEscape(ref CheckEscapeEvent ev)
		{
			RoleType role = ev.Player.characterClassManager.CurClass;
			string srole = role.ToString();
			if (srole == "ClassD")
			{
				if (Configs.enableOnDEscape == true && Configs.odeMessage != "")
				{
					PlayerManager.localPlayer.GetComponent<MTFRespawn>().RpcPlayCustomAnnouncement(Configs.odeMessage, false, true);
				}
				else if (Configs.enableOnDEscape == true && Configs.odeMessage == "")
				{
					Plugin.Info("A message must be set for ca_DEMessage for it to function");
				}
				else if (Configs.enableOnDEscape != true && Configs.odeMessage != "")
				{
					Plugin.Info("ca_EnableDE must be set to true for the message to occur");
				}
			}
			else if (srole == "Scientist")
			{
				if (Configs.enableOnSEscape == true && Configs.oseMessage != "")
				{
					PlayerManager.localPlayer.GetComponent<MTFRespawn>().RpcPlayCustomAnnouncement(Configs.oseMessage, false, true);
				}
				else if (Configs.enableOnSEscape == true && Configs.oseMessage == "")
				{
					Plugin.Info("A message must be set for ca_SEMessage for it to function");
				}
				else if (Configs.enableOnSEscape != true && Configs.oseMessage != "")
				{
					Plugin.Info("ca_EnableSE must be set to true for the message to occur");
				}
			}
		}

		public void OnCommand(ref RACommandEvent ev)
		{
			try
			{
				if (ev.Command.Contains("REQUEST_DATA PLAYER_LIST SILENT"))
					return;

				string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
				string scpFolder = Path.Combine(appData, "SCP Secret Laboratory");
				string logs = Path.Combine(scpFolder, "AdminLogs");
				string fileName = Path.Combine(logs, $"command_log-{ServerConsole.Port}.txt");
				if (!Directory.Exists(logs))
					Directory.CreateDirectory(logs);
				if (!File.Exists(fileName))
					File.Create(fileName).Close();
				string data =
					$"{DateTime.Now}: {ev.Sender.Nickname} ({ev.Sender.SenderId}) executed: {ev.Command} {Environment.NewLine}";
				File.AppendAllText(fileName, data);

				string[] args = ev.Command.Split(' ');
				ReferenceHub sender = ev.Sender.SenderId == "SERVER CONSOLE" ? Plugin.GetPlayer(PlayerManager.localPlayer) : Plugin.GetPlayer(ev.Sender.SenderId);

				switch (args[0].ToLower())
				{
					case "ca":
						ev.Allow = false;
						if(!sender.CheckPermission("ca.main"))
						{
							ev.Sender.RAMessage(denied);
							return;
						}
						ev.Sender.RAMessage("<br>" + 
							"ca: Displays commands and info" + "<br>" + "<br>" +
							"chs (view/play): Plays configured chaos announcement" + "<br>" + "<br>" +
							"re (view/play): Plays configured round end announcement" + "<br>" + "<br>" +
							"rs (view/play): Plays configured round start announcement" + "<br>" + "<br>" +
							"de (view/play): Plays configured DClass escape announcement" + "<br>" + "<br>" +
							"se (view/play): Plays configured Scientist escape announcement" + "<br>" + "<br>" +
							"mtfa (scps left) (mtf number) (mtf letter): Plays mtf annoucement" + "<br>" + "<br>" +
							"scpa (scp number) (death type): Plays scp death announcement;" + "<br>" + "Use tesla, dclass, science, mtf, or chaos for death type");
						break;

					case "chs":
						ev.Allow = false;
						if (!sender.CheckPermission("ca.main"))
						{
							ev.Sender.RAMessage(denied);
							return;
						}

						if (args.Length != 2)
						{
							ev.Sender.RAMessage(chsUsage);
							return;
						}

						if (args[1] == "play" || args[1] == "p")
						{
							if (Configs.chaosMessage != "")
							{
								PlayerManager.localPlayer.GetComponent<MTFRespawn>().RpcPlayCustomAnnouncement(Configs.chaosMessage, false, true);
								ev.Sender.RAMessage(success);
								return;
							}
							else
							{
								ev.Sender.RAMessage("Chaos Spawn announcement is not set");
							}
						}
						else if (args[1] == "view" || args[1] == "v")
						{
							if (Configs.chaosMessage != "")
							{
								ev.Sender.RAMessage(Configs.chaosMessage);
								return;
							}
							else
							{
								ev.Sender.RAMessage("Chaos Spawn announcement is not set");
							}
						}
						else
						{
							ev.Sender.RAMessage(chsUsage);
						}
						break;

					case "re":
						ev.Allow = false;
						if (!sender.CheckPermission("ca.main"))
						{
							ev.Sender.RAMessage(denied);
							return;
						}

						if (args.Length != 2)
						{
							ev.Sender.RAMessage(reUsage);
							return;
						}

						if (args[1] == "play" || args[1] == "p")
						{
							if (Configs.oreMessage != "")
							{
								PlayerManager.localPlayer.GetComponent<MTFRespawn>().RpcPlayCustomAnnouncement(Configs.oreMessage, false, true);
								ev.Sender.RAMessage(success);
								return;
							}
							else
							{
								ev.Sender.RAMessage("Round End announcement is not set");
							}
						}
						else if (args[1] == "view" || args[1] == "v")
						{
							if (Configs.oreMessage != "")
							{
								ev.Sender.RAMessage(Configs.oreMessage);
								return;
							}
							else
							{
								ev.Sender.RAMessage("Round End announcement is not set");
							}
						}
						else
						{
							ev.Sender.RAMessage(reUsage);
						}
						break;

					case "rs":
						ev.Allow = false;
						if (!sender.CheckPermission("ca.main"))
						{
							ev.Sender.RAMessage(denied);
							return;
						}

						if (args.Length != 2)
						{
							ev.Sender.RAMessage(rsUsage);
							return;
						}

						if (args[1] == "play" || args[1] == "p")
						{
							if (Configs.orsMessage != "")
							{
								PlayerManager.localPlayer.GetComponent<MTFRespawn>().RpcPlayCustomAnnouncement(Configs.orsMessage, false, true);
								ev.Sender.RAMessage(success);
								return;
							}
							else
							{
								ev.Sender.RAMessage("Round Start announcement is not set");
							}
						}
						else if (args[1] == "view" || args[1] == "v")
						{
							if (Configs.oseMessage != "")
							{
								ev.Sender.RAMessage(Configs.orsMessage);
								return;
							}
							else
							{
								ev.Sender.RAMessage("Round Start announcement is not set");
							}
						}
						else
						{
							ev.Sender.RAMessage(rsUsage);
						}
						break;

					case "de":
						ev.Allow = false;
						if (!sender.CheckPermission("ca.main"))
						{
							ev.Sender.RAMessage(denied);
							return;
						}

						if (args.Length != 2)
						{
							ev.Sender.RAMessage(deUsage);
							return;
						}

						if (args[1] == "play" || args[1] == "p")
						{
							if (Configs.odeMessage != "")
							{
								PlayerManager.localPlayer.GetComponent<MTFRespawn>().RpcPlayCustomAnnouncement(Configs.odeMessage, false, true);
								ev.Sender.RAMessage(success);
								return;
							}
							else
							{
								ev.Sender.RAMessage("DClass Escape announcement is not set");
							}
						}
						else if (args[1] == "view" || args[1] == "v")
						{
							if (Configs.odeMessage != "")
							{
								ev.Sender.RAMessage(Configs.odeMessage);
								return;
							}
							else
							{
								ev.Sender.RAMessage("DClass Escape announcement is not set");
							}
						}
						else
						{
							ev.Sender.RAMessage(deUsage);
						}
						break;

					case "se":
						ev.Allow = false;
						if (!sender.CheckPermission("ca.main"))
						{
							ev.Sender.RAMessage(denied);
							return;
						}

						if (args.Length != 2)
						{
							ev.Sender.RAMessage(seUsage);
							return;
						}

						if (args[1] == "play" || args[1] == "p")
						{
							if (Configs.oseMessage != "")
							{
								PlayerManager.localPlayer.GetComponent<MTFRespawn>().RpcPlayCustomAnnouncement(Configs.oseMessage, false, true);
								ev.Sender.RAMessage(success);
								return;
							}
							else
							{
								ev.Sender.RAMessage("Scientist Escape announcement is not set");
							}
						}
						else if (args[1] == "view" || args[1] == "v")
						{
							if (Configs.oseMessage != "")
							{
								ev.Sender.RAMessage(Configs.oseMessage);
								return;
							}
							else
							{
								ev.Sender.RAMessage("Scientist Escape announcement is not set");
							}
						}
						else
						{
							ev.Sender.RAMessage(seUsage);
						}
						break;

					case "mtfa":
						ev.Allow = false;
						if (!sender.CheckPermission("ca.main"))
						{
							ev.Sender.RAMessage(denied);
							return;
						}

						if (args.Length != 4)
						{
							ev.Sender.RAMessage(mtfaUsage);
							return;
						}

						if (args.Length == 4)
						{
							if (Int32.TryParse(args[1], out int a))
							{
								int scpsLeft = a;
							}
							else
							{
								ev.Sender.RAMessage(mtfaUsage);
								return;
							}

							if (Int32.TryParse(args[2], out int b))
							{
								int mtfNumber = b;
							}
							else
							{
								ev.Sender.RAMessage(mtfaUsage);
								return;
							}

							if (char.TryParse(args[3], out char c))
							{
								char mtfLetter = c;
							}
							else
							{
								ev.Sender.RAMessage(mtfaUsage);
								return;
							}

							PlayerManager.localPlayer.GetComponent<MTFRespawn>().RpcPlayCustomAnnouncement("MtfUnit epsilon 11 designated nato_" + c + " " + b + " " + "HasEntered AllRemaining AwaitingRecontainment" + " " + a + " " + "scpsubjects", false, true);
							ev.Sender.RAMessage(success);
						}
						break;

					case "scpa":
						ev.Allow = false;
						if (!sender.CheckPermission("ca.main"))
						{
							ev.Sender.RAMessage(denied);
							return;
						}

						if (args.Length != 3)
						{
							ev.Sender.RAMessage(scpaUsage);
							return;
						}

						if (args.Length == 3)
						{
							int e = 0;
							bool resultNan = int.TryParse(args[1], out e);

							if(e == 0)
							{
								ev.Sender.RAMessage(scpaUsage);
								return;
							}
							else
							{
								ebic = args[1];

								for (int i = 1; i <= ebic.Length; i += 1)
								{
									ebic = ebic.Insert(i, " ");
									i++;
								}
							}

							if (args[2] == "t" || args[2] == "tesla")
							{
								deathCause = "Successfully Terminated by Automatic Security System";
							}
							else if (args[2] == "d" || args[2] == "dclass")
							{
								deathCause = "terminated by ClassD personnel";
							}
							else if (args[2] == "s" || args[2] == "science")
							{
								deathCause = "terminated by science personnel";
							}
							else if (args[2] == "c" || args[2] == "chaos")
							{
								deathCause = "terminated by ChaosInsurgency";
							}
							else if (args[2] == "m" || args[2] == "mtf")
							{
								var chars1 = "abcdefghijklmnopqrstuvwxyz";
								var stringChars1 = new char[1];
								var random1 = new Random();

								for (int i = 0; i < stringChars1.Length; i++)
								{
									stringChars1[i] = chars1[random1.Next(chars1.Length)];
								}

								var randLetter = new String(stringChars1);

								var chars2 = "123456789";
								var stringChars2 = new char[1];
								var random2 = new Random();

								for (int i = 0; i < stringChars2.Length; i++)
								{
									stringChars2[i] = chars2[random2.Next(chars2.Length)];
								}

								var randNumber = new String(stringChars2);

								deathCause = "ContainedSuccessfully  ContainmentUnit nato_" + randLetter + " " + randNumber;
							}

							if(deathCause == null)
							{
								ev.Sender.RAMessage(scpaUsage);
								return;
							}
							PlayerManager.localPlayer.GetComponent<MTFRespawn>().RpcPlayCustomAnnouncement("scp" + " " + ebic + " " + deathCause, false, true);
							ev.Sender.RAMessage(success);
							deathCause = null;
						}
						break;
				}
			}
			catch (Exception e)
			{
				EXILED.Log.Error($"Handling command error: {e}");
			}
		}
	}
}