module Tests

open System
open Xunit
open lagrange_interpolate
open linear_interpolate


[<Fact>]
let ``Linear interpolation between two points works correctly`` () =
    let p1 = (0.0, 0.0)
    let p2 = (10.0, 10.0)
    let samplingRate = 5.0

    let result = linearInterpolate p1 p2 samplingRate |> Seq.toList

    Assert.True(([(0.0, 0.0); (5.0, 5.0); (10.0, 10.0)] = result))

[<Fact>]
let ``Linear interpolation produces empty sequence for invalid range`` () =
    let p1 = (10.0, 10.0)
    let p2 = (5.0, 5.0) // x2 < x1
    let samplingRate = 1.0

    let result = linearInterpolate p1 p2 samplingRate |> Seq.toList
    Assert.Empty(result)