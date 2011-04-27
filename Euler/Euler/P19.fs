module P19

open System

let rec daysFrom (d : DateTime) = 
    seq { 
        let next = d.AddDays 1.0
        yield d
        yield! daysFrom next
    }

let days = 
    daysFrom (new System.DateTime(1901, 1, 1))


let run = 
    let y2k = new DateTime(2001, 1, 1)
    let count = (Seq.takeWhile (fun d -> d < y2k) days) |> Seq.filter (fun d -> d.DayOfWeek = DayOfWeek.Sunday && d.Day = 1) |> Seq.length
    printfn "%d" count
