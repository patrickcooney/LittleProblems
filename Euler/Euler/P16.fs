module P16
open System.Numerics

let run = 
    let sq = BigInteger.Pow (2I, 1000)
    let sum = digitsum.sum sq
    printfn "%A %A"  sq sum