module P12
open System

let triangles = 
    seq {
        let sum = ref 0UL
        let curr = ref 1UL
        while !sum + !curr > 0UL do
            yield !sum + !curr
            sum := !sum + !curr
            curr := !curr + 1UL
            
    }    

let nfact n = 
    let mutable facts = 0
    let root = uint64 (Math.Sqrt <| float n)
    for i in 1UL .. root - 1UL do
        if n % i = 0UL then facts <- facts + 2

    if n % root = 0UL then facts <- facts + 1

    facts

let run =
    let x = Seq.find (fun el -> (nfact el) > 500) triangles 
    printfn "%d" x