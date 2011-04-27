module P22

open System.IO

let names = File.ReadAllText("e:\\code\LittleProblems\\Euler\\Euler\\names.txt").Replace("\"", "").Split([|','|])

let namescore (x : string) = 
    Seq.map (fun x -> int x - int 'A' + 1) (x.ToCharArray()) |> Seq.sum

let run = 
    let sorted = Seq.sort names
    let score = Seq.mapi (fun i x -> (i + 1) * (namescore x)) sorted |> Seq.sum
    printfn "%d" score
    ()