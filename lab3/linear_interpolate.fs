module linear_interpolate

let linearInterpolate (x1, y1) (x2, y2) (samplingRate: float) =
    let rec loop x =
        if x < x2+samplingRate then
            let t = (x - x1) / (x2 - x1)
            let y = y1 + t * (y2 - y1)
            seq {
                yield (x, y)
                yield! loop (x + samplingRate)
            }
        else
            Seq.empty

    loop x1
