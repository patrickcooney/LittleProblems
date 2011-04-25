module P6

let sqOfSums n = 
    let sum = Seq.sum [1UL .. n] 
    sum * sum

let sumOfSqs n = 
    Seq.reduce (fun a b -> a + (b * b)) [1UL .. n]

let run = 
    printfn "%d" ((sqOfSums 100UL) - (sumOfSqs 100UL))

