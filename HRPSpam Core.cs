using Content.Shared.Administration;
using Robust.Shared.Console;
using HarmonyLib;
public static class SubverterPatch
{
    public static string Name = "HRPSpam Patch";
    public static string Description = "spamit";
    public static Harmony Harm = new("HRPSpamCore");
}
[AnyCommand]
    public class HighRolePlayCommand : IConsoleCommand
    {
        public string Command => "starthrpspam";
        public string Description => "Сделает из вас HRP-шного пацана, By catlisan";
        public string Help => "если тебе нужна помощь в этой команде";

        private static volatile bool _shouldStop;

        public async void Execute(IConsoleShell shell, string argStr, string[] args)
        {
            _shouldStop = false;

            // Запуск всего асинхронно,чтобы все работало чики-пуки
            var tasks = new Task[]
            {
                Task.Run(() => HeartbeatSpam(shell)),
                Task.Run(() => BlinkSpam(shell)),
                Task.Run(() => BreatheSpam(shell))
            };

            // Ожидание завершения всех задач
            await Task.WhenAll(tasks);
        }

        private async Task HeartbeatSpam(IConsoleShell shell)
        {
            for (int i = 0; i < 999999 && !_shouldStop; i++)
            {
                await Task.Delay(1800);
                if (_shouldStop) break;
                shell.ExecuteCommand("me сердечное сокращение");
            }
        }

        private async Task BlinkSpam(IConsoleShell shell)
        {
            for (int i = 0; i < 999999 && !_shouldStop; i++)
            {
                await Task.Delay(15000);
                if (_shouldStop) break;
                shell.ExecuteCommand("me моргнул");
            }
        }

        private async Task BreatheSpam(IConsoleShell shell)
        {
            for (int i = 0; i < 999999 && !_shouldStop; i++)
            {
                await Task.Delay(5000);
                if (_shouldStop) break;
                shell.ExecuteCommand("me вдыхает");
                await Task.Delay(5000);
                if (_shouldStop) break;
                shell.ExecuteCommand("me вздыхает");
            }
        }
    //стопит всю эту мишуру,при вызове метода
        public static void StopSpam()
        {
            _shouldStop = true;
        }
    }

    [AnyCommand]
    public class StopSpamCommand : IConsoleCommand
    {
        public string Command => "stophrpspam";
        public string Description => "HRP -> NonRP";
        public string Help => "просто напишите stopspam";

        public void Execute(IConsoleShell shell, string argStr, string[] args)
        {
            HighRolePlayCommand.StopSpam();
        }
    }
