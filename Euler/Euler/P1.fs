module P1
open System

let run = 
    let mutable x = 0
    for i in 3 .. 999 do
        if (i % 5) = 0 then x <- x + i
        elif (i % 3) = 0 then x <- x + i
    printfn "%d" x
    ignore System.Console.ReadLine
    ()

