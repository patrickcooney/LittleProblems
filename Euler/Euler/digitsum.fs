module digitsum

open System.Numerics

let rec sum (n : BigInteger) = 
    if n = 0I then 0I
    else
        n % 10I + (sum <| n / 10I)


