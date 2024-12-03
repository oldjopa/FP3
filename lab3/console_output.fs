module console_output

open linear_interpolate
open lagrange_interpolate

let printResults (results: seq<string * seq<float * float>>) =
    results
    |> Seq.iter (fun (description, points) ->
        printfn "%s" description
        
        let xs, ys = 
            points
            |> Seq.fold (fun (xAcc, yAcc) (x, y) -> (xAcc @ [x], yAcc @ [y])) ([], [])
        
        printfn "%s" (System.String.Join("\t", xs |> List.map (fun x -> sprintf "%.3f" x)))
        printfn "%s" (System.String.Join("\t", ys |> List.map (fun y -> sprintf "%.3f" y)))
        printfn "" 
    )
