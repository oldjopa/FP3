# FP3
## Лабораторная работа №3 

```Янковой Леонид Кириллович```

```367672```

## Язык - F#

### Описание программы

При запуске в качестве аргументов принимаются алгоритмы интерполяции по ключу ```--algorithms``` и частота дискретизации по ключу ```--rate```

Реализовано 2 алгоритма интерполяции - Лагранж и линейная отрезками.

### Внутреннее устройство кода

Программа разделена на обработчик ввода, обработчик введенных данных, алгоритмы интерполяции и вывод полученных данных

Все работает на Seq - местные ленивые вычисления


### Листинг функций интерполяции

linear_interpolate.fs
```fsharp
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
```

lagrange_interpolate.fs
```fsharp
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
```

### Пример взаимодействия
```
\FP\FP3\lab3> dotnet run --algorithms=linear,lagrange --rate=1
Enter point
1 1
Enter point
2 2
linear
1.000   2.000
1.000   2.000

Enter point
10 15
linear
2.000   3.000   4.000   5.000   6.000   7.000   8.000   9.000   10.000
2.000   3.625   5.250   6.875   8.500   10.125  11.750  13.375  15.000

Enter point
20 24
linear
10.000  11.000  12.000  13.000  14.000  15.000  16.000  17.000  18.000  19.000  20.000
15.000  15.900  16.800  17.700  18.600  19.500  20.400  21.300  22.200  23.100  24.000

lagrange
1.000   2.000   3.000   4.000   5.000   6.000   7.000   8.000   9.000   10.000  11.000  12.000  13.000  14.000  15.000  16.000  17.000  18.000  19.000  20.000
1.000   2.000   3.220   4.625   6.180   7.851   9.603   11.402  13.212  15.000  16.730  18.368  19.880  21.230  22.384  23.307  23.965  24.323  24.346  24.000
```