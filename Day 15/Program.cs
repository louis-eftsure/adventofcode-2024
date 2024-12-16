using Day_15;
using Spectre.Console;

var input = (await File.ReadAllTextAsync("Inputs/input.txt")).Split("\r\n\r\n");
var map = input[0];
var moves = input[1];

await WarehouseRunner.RunSimulation(map, moves, useWideMode: false, visualize: false);
await WarehouseRunner.RunSimulation(map, moves, useWideMode: true, visualize: false);