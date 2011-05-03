module P29

open System.Collections.Generic
open System.Numerics
open System

let run = 
    let nums = new HashSet<float>()
    for a in 2 .. 100 do
        for b in 2 .. 100 do
            let pow = Math.Pow((float a),  (float b))
            ignore <| nums.Add pow

    printfn "Total unique nums %d" nums.Count

    nums.Count