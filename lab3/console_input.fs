module console_input

open System.IO
open System


let readInput (input: TextReader) =
    seq {
        while true do
            let line: string = input.ReadLine()

            if line = null then
                yield! []
            else
                match line.Split(separator = [| ' '; '\t' |], options = StringSplitOptions.RemoveEmptyEntries) with
                | [| x; y |] -> yield (float x, float y)
                | _ -> printfn "Invalid input format"
    }
