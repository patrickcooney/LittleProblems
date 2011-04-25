module P5

let getProd nums = 
    Seq.reduce (fun a c -> a * c) nums

let allDivisible n divs = 
    Seq.forall (fun elem -> n % elem = 0UL) divs

let firstNonDivisible n divs = 
    Seq.find (fun elem -> n % elem <> 0UL) divs

let removeCovered newFact facts = 
    seq { 
        yield newFact
        for i in facts do
            if newFact % i <> 0UL then
                yield i
    }

let run = 
    let max = 20UL
    let mutable facts = primes.primesUpTo max
   
    let divs = seq {1UL..max}
    
    while not <| allDivisible (getProd facts) divs do
        //not optimal to pass divs, since this is really "divs not facts"
        let firstNewFact = firstNonDivisible (getProd facts) divs
        printfn "Adding %d" firstNewFact
        facts <- removeCovered firstNewFact facts

       
    printfn "%d" (getProd facts)