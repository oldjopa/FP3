module linear_interpolate

let linearInterpolate (x1, y1) (x2, y2) (samplingRate: float) =
    seq {
        let step = samplingRate
        let mutable x = x1
        while x <= x2 do
            let t = (x - x1) / (x2 - x1)
            let y = y1 + t * (y2 - y1)
            yield (x, y)
            x <- x + step
    }
