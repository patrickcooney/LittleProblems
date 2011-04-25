module P10



let run = 
    let sum = Seq.sum <| primes.primesUpTo 2000000UL
    printfn "%d" sum