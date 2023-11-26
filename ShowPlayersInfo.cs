using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Utils;
using System.Text.Json;

namespace ShowPlayersInfo;
public class Config
{
    public List<ulong> admins { get; set; } = new List<ulong>();
}

[MinimumApiVersion(65)]
public class ShowPlayersInfo : BasePlugin
{
    public override string ModuleName => "ShowPlayersInfo";

    public override string ModuleVersion => "v1.0.0";

    public override string ModuleAuthor => "jackson tougher";
    public Config config = new Config();
    public override void Load(bool hotReload)
    {
        var configPath = Path.Join(ModuleDirectory, "Config.json");
        if (!File.Exists(configPath))
        {
            config.admins.Add(76561199414091272);
            File.WriteAllText(configPath, JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping }));
        }
        else config = JsonSerializer.Deserialize<Config>(File.ReadAllText(configPath));
    }
    public void OnConfigReload()
    {
        var configPath = Path.Join(ModuleDirectory, "Config.json");
        config = JsonSerializer.Deserialize<Config>(File.ReadAllText(configPath));
    }
    public void SeparatePlayersInfo(List<CCSPlayerController> playersList)
    {
        int? UserID;
        string? UserName;
        ulong? UserSteamID;

        playersList.ForEach(player =>
        {
            if (player.IsValid && !player.IsHLTV)
            {
                UserID = player.UserId;
                UserName = player.PlayerName;
                UserSteamID = player.SteamID;

                player.PrintToChat($" {ChatColors.Grey}============= Players Info =============");
                player.PrintToChat($" {ChatColors.Grey} < {UserID} > || {UserName} || {UserSteamID} ||");
                player.PrintToChat($" {ChatColors.Grey}====================================");
            }

        });
    }
    [ConsoleCommand("css_players")]
    public void OnCommandPlayers(CCSPlayerController? controller, CommandInfo command)
    {
        if (controller == null) return;
        if (config.admins.Exists(adminID => adminID == controller.SteamID))
            SeparatePlayersInfo(Utilities.GetPlayers());
        else
            controller.PrintToChat($" {ChatColors.Red}You are not Admin!!!");
    }
    [ConsoleCommand("css_showplayersinfo_reload")]
    public void OnBotikiConfigReload(CCSPlayerController? controller, CommandInfo command)
    {
        if (controller == null) return;
        if (config.admins.Exists(adminID => adminID == controller.SteamID))
        {
            OnConfigReload();
            controller.PrintToChat($" {ChatColors.Olive}Configuration was reloaded. {ChatColors.Green}OK!");
        }
        else
            controller.PrintToChat($" {ChatColors.Red}You are not Admin!!!");
    }
}
