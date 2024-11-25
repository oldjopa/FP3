module points_calculator
open lagrange_interpolate
open linear_interpolate
open config

let maxWindowLength = 4

let takeLast n sequence =
    sequence
    |> Seq.skip (max 0 (Seq.length sequence - n))
    |> Seq.toList

let processData (config: Config) (points: seq<float * float>) =
    seq {
        let rec processCurrent currentPoints =
            seq {
                for alg in config.Algorithms do
                    match alg with
                    | "linear" when Seq.length currentPoints >= 2 ->
                        let linearResults =
                            takeLast 2 currentPoints
                            |> Seq.pairwise
                            |> Seq.collect (fun (p1, p2) -> linearInterpolate p1 p2 config.SamplingRate)
                        yield ("linear", linearResults)
                    | "lagrange" when Seq.length currentPoints >= 4 ->
                        let lagrangeResults =
                            lagrangeInterpolate (Array.ofSeq (takeLast 4 currentPoints)) config.SamplingRate
                        yield ("lagrange", lagrangeResults)
                    | _ -> ()
            }

        // let accumulatedPoints = Seq.scan (fun acc point -> acc @ [point]) [] points
        let accumulatedPoints = 
            Seq.scan (fun acc point -> 
                match acc with
                | _ when List.length acc >= maxWindowLength -> acc.Tail @ [point]
                | _ -> acc @ [point]
            ) [] points

        for pointsSet in accumulatedPoints do
            yield! processCurrent pointsSet
    }
