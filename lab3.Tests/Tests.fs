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
let ``Test Lagrange Interpolation 4 Points`` () =
    let points = [ (0.0, 0.0); (1.0, 1.0); (2.0, 4.0); (3.0, 9.0) ]
    let result = lagrangeInterpolate points 0.5

    let expected = [(0.0, 0.0); (0.5, 0.25); (1.0, 1.0); (1.5, 2.25); (2.0, 4.0); (2.5, 6.25); (3.0, 9.0)]

    Assert.True((expected = (result |> Seq.toList)))


[<Fact>]
let ``Test Lagrange Interpolation Negative Values`` () =
    let points = [ (-2.0, -8.0); (-1.0, -1.0); (1.0, 1.0); (2.0, 8.0) ]
    let result = lagrangeInterpolate points 1

    let expected = [(-2.0, -8.0); (-1.0, -1.0); (0.0, 0.0); (1.0, 1.0); (2.0, 8.0)]

    Assert.True((expected = (result |> Seq.toList)))


[<Fact>]
let ``Linear interpolation should cover the last point`` () =
    let p1 = (0.0, 0.0)
    let p2 = (10.1, 10.0)
    let samplingRate = 0.5

    let result = linearInterpolate p1 p2 samplingRate |> Seq.toList

    Assert.True(fst p2 <= fst (List.last result))


[<Fact>]
let ``Lagrange interpolation should cover the last point`` () =
    let points = [ (1.0, 2.0); (3.0, 3.0); (4.0, 5.0); (6.1, 7.0) ]
    let result = lagrangeInterpolate points 0.5 |> Seq.toList

    Assert.True(fst (List.last points) <= fst (List.last result))
