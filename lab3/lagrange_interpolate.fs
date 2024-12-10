module lagrange_interpolate

let lagrangeInterpolate (points: (float * float) list) (samplingRate: float) =
    let n = points.Length
    let xs, ys = points |> List.unzip
    let step = samplingRate
    let minX = xs[0]
    let maxX = xs[n - 1]+step

    seq {
        for x in
            Seq.initInfinite (fun i -> minX + float i * step)
            |> Seq.takeWhile (fun x -> x < maxX) do
            let y =
                [ for i in 0 .. n - 1 do
                       let xi, yi = xs.[i], ys.[i]

                       let li =
                           [ for j in 0 .. n - 1 do
                                  if j <> i then (x - xs.[j]) / (xi - xs.[j]) else 1.0 ]
                           |> List.fold (*) 1.0

                       yield yi * li ]
                |> List.sum

            yield (x, y)
    }
