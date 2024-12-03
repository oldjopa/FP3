module lagrange_interpolate

let lagrangeInterpolate (points: (float * float) array) (samplingRate: float) =
    let n = points.Length
    let xs, ys = points |> Array.unzip
    let step = samplingRate
    let minX = xs.[0]
    let maxX = xs.[3]

    seq {
        for x in
            Seq.initInfinite (fun i -> minX + float i * step)
            |> Seq.takeWhile (fun x -> x <= maxX) do
            let y =
                [| for i in 0 .. n - 1 do
                       let xi, yi = xs.[i], ys.[i]

                       let li =
                           [| for j in 0 .. n - 1 do
                                  if j <> i then (x - xs.[j]) / (xi - xs.[j]) else 1.0 |]
                           |> Array.fold (*) 1.0

                       yield yi * li |]
                |> Array.sum

            yield (x, y)
    }
