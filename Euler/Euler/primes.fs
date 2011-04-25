module primes
open System
open System.Collections.Generic

let prime n (primesBelowN : List<uint64>)= 
    let root = uint64 (Math.Sqrt (float n))
    let possibleDivisors = Seq.takeWhile (fun x -> x <= root) primesBelowN
    let prime = not (Seq.exists (fun elem -> n % elem = 0UL) possibleDivisors)
    if prime then ignore <| primesBelowN.Add n
    prime

let primesUpTo2 n primesBelowN = 
    seq {
        if n > 1UL then yield 2UL
        for i in 3UL .. 2UL .. n do
            if prime i primesBelowN then yield i
    }
  
let primesUpTo (n : uint64) =
    let primesBelowN = new List<uint64>()
    primesUpTo2 n primesBelowN


let nthPrime (n : uint64) = 
    let ps = new List<uint64>()
    ignore <| ps.Add(2UL)

    let mutable curr = 3UL
    let mutable lastPrime = 2UL
    while uint64 ps.Count < n do
        if prime curr ps then
            lastPrime <- curr
        
        curr <- curr + 2UL

    lastPrime