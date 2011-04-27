module P25

let run = 
    let mutable onethousanddigits = 1I
    for i in 1 .. 999 do
        onethousanddigits <- onethousanddigits * 10I

    let min = onethousanddigits
    let firstBig = Seq.tryFindIndex (fun x -> x >= min) fib.fibs


    printfn "%A" <| firstBig.Value + 1
    0