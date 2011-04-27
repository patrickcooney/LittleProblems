module divisors

open System

let sumDivisors n = 
    let mutable count = 1 // because the seq below starts at two, n isn't counted but 1 should be
    if n < 2 then raise (ArgumentException("cannot get number of divisors"))
    else
        let max = int <| Math.Sqrt (float n)
        if max > 1 then
            let divLessSquare = seq { 2 .. max } |> Seq.filter (fun e -> n % e = 0)
            count <- count + Seq.sum divLessSquare
            count <- count + (Seq.sum <| Seq.map (fun e -> n / e) divLessSquare)
            if max * max = n then count <- count - max
        count
