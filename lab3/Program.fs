open System
open System.IO
open console_input
open console_output
open points_calculator
open config


let parseArgs (args: string array) =
    let defaultConfig = { Algorithms = ["linear"]; SamplingRate = 1.0 }
    args 
    |> Array.fold (fun config arg ->
        match arg.Split('=') with
        | [| "--algorithms"; algs |] -> { config with Algorithms = algs.Split(',') |> Array.toList }
        | [| "--rate"; rate |] -> { config with SamplingRate = float rate }
        | _ -> config) defaultConfig


[<EntryPoint>]
let main (args: string array) =
    let config = parseArgs args
    let points = readInput Console.In // |> Seq.cache
    processData config points |> printResults
    0