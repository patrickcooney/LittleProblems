module P2

open System.Numerics

let run = 
 
    let c = Seq.fold (fun (x : BigInteger) (c : BigInteger) -> x + c) 0I <| (Seq.filter (fun e -> e % 2I = 0I) <| Seq.takeWhile (fun e -> e <= 4000000I) fib.fibs)

    printfn "x %A" c
    
