module P21
open System
open System.Collections.Generic


let getNumToDivisors max = 
    let ntd = new Dictionary<int, int>()

    for i in 2 .. max do
        ntd.Add(i, (divisors.sumDivisors i))

    ntd

let amicablePairs max = 
    seq {
    let numToDivisors = getNumToDivisors max
    
    for i in 2 .. max do
        let idc = numToDivisors.[i]
        if idc <> i && idc > 1 && idc <= max && numToDivisors.[idc] = i then
            yield i
            //idc gets yielded later
            printfn "amicable %d %d" i idc
    }            

let run = 
    amicablePairs 9999 |> Seq.sum |> printfn "%d" 
