using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Admin;
using CounterStrikeSharp.API.Modules.Utils;

namespace ShowPlayersInfo;

[MinimumApiVersion(65)]
public class ShowPlayersInfo : BasePlugin
{
    public override string ModuleName => "ShowPlayersInfo";

    public override string ModuleVersion => "v1.1.0";

    public override string ModuleAuthor => "jockii (ch1nazes)";
    public override void Load(bool hotReload)
    {
        
    }
    public void SeparatePlayersInfo(List<CCSPlayerController> playersList)
    {
        int? UserID;
        string? UserName;
        ulong? UserSteamID;
        string? Role;

        playersList.ForEach(player =>
        {
            if (player.IsValid && !player.IsHLTV && !player.IsBot)
            {
                UserID = player.UserId;
                UserName = player.PlayerName;
                UserSteamID = player.SteamID;
                if (AdminManager.PlayerHasPermissions(player, "@css/generic"))
                    Role = $" {ChatColors.Red}Admin{ChatColors.Grey}";
                else
                    Role = $" {ChatColors.Olive}Player{ChatColors.Grey}";

                player.PrintToChat($" {ChatColors.Grey}============= Players Info =============");
                player.PrintToChat($" {ChatColors.Grey}{Role} || < {UserID} > || {UserName} || {UserSteamID}");
                player.PrintToChat($" {ChatColors.Grey}====================================");
            }
        });
    }
    [ConsoleCommand("css_players")]
    public void OnCommandPlayers(CCSPlayerController? controller, CommandInfo command)
    {
        if (controller == null) return;
        if (AdminManager.PlayerHasPermissions(controller, "@css/generic"))
            SeparatePlayersInfo(Utilities.GetPlayers());
        else
            controller.PrintToChat($" {ChatColors.Red}You do not have permission for this command");
    }
}