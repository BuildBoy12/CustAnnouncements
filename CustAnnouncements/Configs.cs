using EXILED;

namespace CustomAnnouncements
{
    public class Configs
    {
        internal static bool enableCustomChaos;
        internal static string chaosMessage;

        internal static bool enableOnRoundEnd;
        internal static string oreMessage;

        internal static bool enableOnRoundStart;
        internal static string orsMessage;

        internal static bool enableOnDEscape;
        internal static string odeMessage;

        internal static bool enableOnSEscape;
        internal static string oseMessage;

        internal static bool enableCustJoinMsg;
        
        internal static void ReloadConfigs()
        {
            enableCustomChaos = Plugin.Config.GetBool("ca_EnableCHS", false);
            chaosMessage = Plugin.Config.GetString("ca_CHSMessage", "");

            enableOnRoundEnd = Plugin.Config.GetBool("ca_EnableRE", false);
            oreMessage = Plugin.Config.GetString("ca_REMessage", "");

            enableOnRoundStart = Plugin.Config.GetBool("ca_EnableRS", false);
            orsMessage = Plugin.Config.GetString("ca_RSMessage", "");

            enableOnDEscape = Plugin.Config.GetBool("ca_EnableDE", false);
            odeMessage = Plugin.Config.GetString("ca_DEMessage", "");

            enableOnSEscape = Plugin.Config.GetBool("ca_EnableSE", false);
            oseMessage = Plugin.Config.GetString("ca_SEMessage", "");

            enableCustJoinMsg = Plugin.Config.GetBool("ca_EnableJM", false);           
        }   
    }
}
