using Spectre.Console;

namespace Day_15;

public class WarehouseRunner
{
    public static async Task RunSimulation(string originalMap, string moves, bool useWideMode = false, bool visualize = false)
    {
        var map = useWideMode ? MapWidener.WidenMap(originalMap) : originalMap;
        var warehouse = new Warehouse(map, useWideMode);

        if (visualize)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new Rule($"[yellow]Warehouse Robot Simulation ({(useWideMode ? "Wide" : "Normal")} Mode)[/]").LeftJustified());
            await warehouse.ProcessMovesWithVisualization(moves);
            AnsiConsole.WriteLine();
            AnsiConsole.Write(new Rule("[yellow]Final GPS Coordinates[/]").LeftJustified());
            AnsiConsole.MarkupLine($"[green]Total GPS Sum: {warehouse.CalculateGPSSum()}[/]");
        }
        else
        {
            warehouse.ProcessMoves(moves);
            Console.WriteLine($"Total GPS Sum: {warehouse.CalculateGPSSum()}");
        }
    }
}