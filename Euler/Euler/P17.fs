module P17

open System

let ones n = 
    if n = 0 then ""
    elif n = 1 then "one"
    elif n = 2 then "two"
    elif n = 3 then "three"
    elif n = 4 then "four"
    elif n = 5 then "five"
    elif n = 6 then "six"
    elif n = 7 then "seven"
    elif n = 8 then "eight"
    else "nine"

let hundreds n = 
    let x = n - (n % 100)
    if x <> 0 then
        (ones (x / 100)) + "hundred"
    else ""

let tens n = 
    if n >= 90 then "ninety"
    elif n >= 80 then "eighty"
    elif n >= 70 then "seventy"
    elif n >= 60 then "sixty"
    elif n >= 50 then "fifty"
    elif n >= 40 then "forty"
    elif n >= 30 then "thirty"
    elif n >= 20 then "twenty"
    else raise (ArgumentException("cannot do teens"))

let teens n = 
    if n = 10 then "ten"
    elif n = 11 then "eleven"
    elif n = 12 then "twelve"
    elif n = 13 then "thirteen"
    elif n = 14 then "fourteen"
    elif n = 15 then "fifteen"
    elif n = 16 then "sixteen"
    elif n = 17 then "seventeen"
    elif n = 18 then "eighteen"
    elif n = 19 then "nineteen"
    else raise (ArgumentException("not a teen"))

let tensones n = 
    let mutable v = ""
    let nohundreds = n % 100

    if nohundreds > 0 && nohundreds < 10 then
        v <- (ones nohundreds)
    elif nohundreds >= 10 && nohundreds < 20 then
        v <- (teens (nohundreds))
    elif nohundreds >= 20 then
        v <- (tens (nohundreds)) + (ones (n % 10))
    
    if n > 100 && nohundreds > 0 then
        "and" + v
    else v

let asWords n = 
    if n > 1000 then raise (ArgumentException("cannot do more than 1000"))
    elif n = 1000 then "onethousand"
    else
        (hundreds n) + (tensones (n))

let run = 
    let nums = seq { 1 .. 1000 }
    let x = Seq.sum (Seq.map (fun (s : string) -> s.Length) <| Seq.map (fun i -> asWords i) nums)
    printfn "%d" x
        
