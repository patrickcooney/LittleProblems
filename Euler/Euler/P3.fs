module P3

open System
open System.Collections.Generic

let num = 600851475143UL

let mutable primes = []

let prime n = 
    let prime = not (List.exists (fun elem -> n % elem = 0UL) primes)

    if prime then primes <- n :: primes

    prime

let primefacs (n : uint64) = 
    seq {
    for i in 2UL .. n - 1UL do
        if n % i = 0UL then
            if prime i then yield i
    }

let run = 
    printfn "Looking for prime factors of %d" num
    for p in primefacs num do
        printfn ": %d" p
    ()

