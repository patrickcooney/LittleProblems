module P16
open System.Numerics

let rec digitSum (n : BigInteger) = 
    if n = 0I then 0I
    else
        n % 10I + (digitSum <| n / 10I)


let run = 
    let sq = BigInteger.Pow (2I, 1000)
    let sum = digitSum sq
    printfn "%A %A"  sq sum