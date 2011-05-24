module P32
open System.Collections.Generic

let numericchars = ["1"; "2"; "3"; "4"; "5"; "6"; "7"; "8"; "9"]

let pandigitalstring (str : string) = 
    str.Length = 9 && (Seq.forall (fun x -> str.Contains(x)) numericchars)

let pandigitalproduct a b = 
    let ab = a * b
    if ab > 987654321 || ab % 10 = 0 then
        false
    else
        let stringified = a.ToString() + b.ToString() + ab.ToString()
        pandigitalstring stringified

let run() = 
    let hs = new HashSet<int>()

    let max = 9999;

    for i in 1 .. 9999 do
        for j in 1 .. 9999 do
            if not (i % 10 = 0) && not (j % 10 = 0) && pandigitalproduct i j then
                printfn "found pd %d %d" i j
                ignore <| hs.Add(i * j)

    printfn "%d pandigitals sum %d" hs.Count (Seq.sum hs)

    1