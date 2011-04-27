module P23

open System
open System.Collections.Generic
open System.Diagnostics

let isabundant n = 
    divisors.sumDivisors n > n

let abundant max = 
    seq {
    for i in 2 .. max do
        if isabundant i then yield i
    }

let minAbound = 12

let noSumOf2 x abound (hashed : HashSet<int>) = 
    let min = x - minAbound + 1
    not (Seq.takeWhile (fun y -> y < min ) abound |> Seq.exists (fun z -> hashed.Contains(x - z)))

let run =
    let max = 28122

    let hashed = new HashSet<int>()

    let s = Stopwatch.StartNew()
    for a in abundant max do
        for b in abundant max do
            ignore <| hashed.Add(a + b)

    

    let s = Stopwatch.StartNew()
    let range = seq { 1 .. max }
    let sum = Seq.sum <| Seq.filter (fun x -> not <| hashed.Contains(x)) range

    s.Stop()

    printfn "%d in %d" sum s.ElapsedMilliseconds

    sum