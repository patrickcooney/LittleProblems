module fact
open System

let rec factorial n = 
    if n < 1I then raise (ArgumentException("cannot fact negative"))
    if n = 1I then n
    else n * (factorial (n - 1I))
