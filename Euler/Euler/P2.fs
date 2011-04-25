module P2

let run = 
 
    let c = Seq.fold (fun (x : uint64) (c : uint64) -> x + c) 0UL <| (Seq.filter (fun e -> e % 2UL = 0UL) <| Seq.takeWhile (fun e -> e <= 4000000UL) fib.fibs)

    printfn "x %d" c
    
