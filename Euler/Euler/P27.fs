module P27

open System.Collections.Generic
open System.Diagnostics

type solution = { A : int; B : int; NPrimes: int;}

let ps = new List<uint64>()
let nPrimes = 1000000UL 

let sw = Stopwatch.StartNew()
for p in  primes.primesUpTo nPrimes do
    ps.Add(p)

sw.Stop()
printfn "generated %d primes in %d, enter to continue" nPrimes sw.ElapsedMilliseconds

ignore <| System.Console.ReadLine()

let eq a b n =
    (n*n) + (a*n) + b

let isPrimeNonZero (x : uint64) = 
    primes.prime x ps

let isPrime x = 
    //printfn "testing if %d is  prime" x
    x > 0 && isPrimeNonZero (uint64 x)

let primeSeq a b = 
    let mutable n = 0

    while isPrime <| eq a b n do
        n <- n + 1

    n

let findMaxPrimeSeq n = 
    let mutable best = { A = 0; B = 0; NPrimes = 0;}
    for a in -n .. n do
        for b in -n .. n do
            let score = primeSeq a b
            
            if score > best.NPrimes then
                printfn "score for a %d b %d is %d" a b score
                best <- { A = a; B = b; NPrimes = score; }
    best.NPrimes

let run = 
    findMaxPrimeSeq 999