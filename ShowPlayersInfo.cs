using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Admin;
using CounterStrikeSharp.API.Modules.Utils;
using CounterStrikeSharp.API.Modules.Entities;

namespace ShowPlayersInfo;

[MinimumApiVersion(65)]
public class ShowPlayersInfo : BasePlugin
{
    public override string ModuleName => "ShowPlayersInfo";

    public override string ModuleVersion => "v1.3.0";

    public override string ModuleAuthor => "jockii (ch1nazes)";
    public override void Load(bool hotReload)
    {

    }
    [ConsoleCommand("css_players")]
    public void OnCommandPlayers(CCSPlayerController? controller, CommandInfo command)
    {
        if (controller == null) return;
        if (AdminManager.PlayerHasPermissions(controller, "@css/generic"))
        {
            controller.PrintToChat($" {ChatColors.Grey}====================== Players Info =======================");
            List<CCSPlayerController> playersList = Utilities.GetPlayers();
            playersList.ForEach(player =>
            {
                if (player.IsValid && !player.IsBot && !player.IsHLTV)
                {
                    string? UserID = "";
                    string? UserName = "";
                    string? UserRole = "";
                    string? UserID2 = "";
                    string? UserID3 = "";
                    string? UserID64 = "";
                    SteamID id = new SteamID(player.SteamID);

                    if (player.TeamNum == 2)
                    {
                        if (AdminManager.PlayerHasPermissions(player, "@css/generic"))
                            UserRole = $" {ChatColors.Purple}Admin {ChatColors.Yellow}(T)";
                        else
                            UserRole = $" {ChatColors.Olive}Player {ChatColors.Yellow}(T)";

                        UserID = $" {ChatColors.Yellow}{player.UserId}";
                        UserID2 = $" {ChatColors.Yellow}{id.SteamId2}";
                        UserID3 = $" {ChatColors.Yellow}{id.SteamId3}";
                        UserID64 = $" {ChatColors.Yellow}{id.SteamId64}";
                        UserName = $" {ChatColors.Yellow}{player.PlayerName}";
                    }
                    else if (player.TeamNum == 3)
                    {
                        if (AdminManager.PlayerHasPermissions(player, "@css/generic"))
                            UserRole = $" {ChatColors.Purple}Admin {ChatColors.Blue}(CT)";
                        else
                            UserRole = $" {ChatColors.Olive}Player {ChatColors.Blue}(CT)";

                        UserID = $" {ChatColors.Blue}{player.UserId}";
                        UserID2 = $" {ChatColors.Blue}{id.SteamId2}";
                        UserID3 = $" {ChatColors.Blue}{id.SteamId3}";
                        UserID64 = $" {ChatColors.Blue}{id.SteamId64}";
                        UserName = $" {ChatColors.Blue}{player.PlayerName}";
                    }
                    else if (player.TeamNum == 1)
                    {
                        if (AdminManager.PlayerHasPermissions(player, "@css/generic"))
                            UserRole = $" {ChatColors.Purple}Admin {ChatColors.White}(SPEC)";
                        else
                            UserRole = $" {ChatColors.White}Spectator";

                        UserID = $" {ChatColors.White}{player.UserId}";
                        UserID2 = $" {ChatColors.White}{id.SteamId2}";
                        UserID3 = $" {ChatColors.White}{id.SteamId3}";
                        UserID64 = $" {ChatColors.White}{id.SteamId64}";
                        UserName = $" {ChatColors.White}{player.PlayerName}";
                    }
                    controller.PrintToChat($" {ChatColors.Grey}Status: {UserRole} {ChatColors.Grey}|| UserID: <{ChatColors.Green}{UserID} {ChatColors.Grey}> || Nickname: {ChatColors.Lime}{UserName}");
                    controller.PrintToChat($" {ChatColors.Grey}STEAM_ID: {UserID2}");
                    controller.PrintToChat($" {ChatColors.Grey}STEAM_ID3: {UserID3}");
                    controller.PrintToChat($" {ChatColors.Grey}STEAM_ID64: {UserID64}");
                    controller.PrintToChat($" {ChatColors.Grey}=======================================================");
                }
            });
        }
        else
            controller.PrintToChat($" {ChatColors.Red}You do not have permission for this command");
    }
}