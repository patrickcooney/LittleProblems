module primes

let mutable primes = []

let prime n = 
    let prime = not (List.exists (fun elem -> n % elem = 0UL) primes)

    if prime then primes <- n :: primes

    prime

  
let primesUnder n =


