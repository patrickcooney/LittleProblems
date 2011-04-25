module P9
open System

let squaresUpTo n = 
    seq { 
    if n > 0 then
        for i in 1 .. int <| Math.Sqrt (float n) do
          yield i
    }  



let run = 
    let maxC = 998
    let sum = 1000
    for c in [1 .. maxC] do        
        let cSq = c * c
        for b in squaresUpTo (cSq - 1) do
            let bSq = b * b
            if c + b < sum then
                for a in squaresUpTo (bSq - 1) do
                    let aSq = a * a
                    if aSq + bSq = cSq  && a + b + c = sum then
                        printfn "%d %d %d %d" a b c (a + b + c)
                    

