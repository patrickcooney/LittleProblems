module P24
open System
open System.Diagnostics

let rec fact n = 
    if n < 1 then raise (ArgumentException("n"))
    elif n = 1 then 1
    else 
        fact (n-1) * n
         
let run = 
    let mutable elems = seq { 0 .. 9} 

    let mutable n = 999999
    let sw = Stopwatch.StartNew()

    for i in 9 .. -1 .. 1 do
        let facted = fact i
        let numberOfPerms = n / facted
        let nextN = Seq.nth numberOfPerms elems
        printf "%d" nextN
        elems <- (Seq.filter (fun x -> x <> nextN) elems)
        n <- n - (numberOfPerms * facted)

    sw.Stop()

    printfn "%d in %dms" (Seq.head elems) sw.ElapsedMilliseconds 
    0

