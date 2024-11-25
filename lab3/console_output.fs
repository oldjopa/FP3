module console_output

open linear_interpolate
open lagrange_interpolate

// let printResults (results: list<string * seq<float * float>>) =
//     results |> List.iter (fun (description, points) ->
//         printfn "%s" description
//         points |> Seq.iter (fun (x, y) -> printfn "%.3f\t%.3f" x y)
//         printfn "" // Пустая строка для разделения блоков
//     )

let printResults (results: seq<string * seq<float * float>>) =
    results
    |> Seq.iter (fun (description, points) ->
        printfn "%s" description
        points |> Seq.iter (fun (x, y) -> printfn "%.3f\t%.3f" x y)
        printfn "" // Пустая строка для разделения блоков
    )
