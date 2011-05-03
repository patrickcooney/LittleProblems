module P26

open System
open System.Collections.Generic
open System.Diagnostics
open System.Numerics
open System.Text

//let numerator = 1000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000I
let mutable numerator = 100I

let rec digits n = 
    seq {
        let s = n.ToString()
        for x in s.ToCharArray() do
            if x >= '0' && x <= '9' then
                yield Int32.Parse(x.ToString())
    }

let stringify digits = 
    let sb = new StringBuilder(Seq.length digits)
    for d in digits do
        ignore(sb.Append(d.ToString()))
    
    sb.ToString() 

let pairedInSeq (p1 : int) (p2 : int) digits = 
    p2 > p1 && (Seq.length digits) > p2 && Seq.nth p1 digits = Seq.nth p2 digits

let repeatedPatternLenAt pos digits = 
    if pos < 1 then raise (ArgumentException("pos"))
    //0 to pos - 1 = pos to (2*pos - 1)
//    let pat = Seq.forall (fun x -> x) <| (Seq.mapi (fun i elem -> pairedInSeq i (pos + i) digits) <| Seq.take pos digits)
    

    let mutable i = 0
    while i < pos && (pairedInSeq i (pos + i) digits) do 
        i <- i + 1
    
    if i = pos then 
        let part1 = Seq.take pos digits
        let part2 = Seq.skip pos digits
//        printfn "1st pat: %s" <| stringify part1 
//        printfn "2nd pat: %s" <| stringify part2
        pos 
    else 0    


let rec repeatedPatternLenAtOrPast pos digits = 
    if pos > Seq.length digits then 0
    elif pos + 1 = Seq.length digits then repeatedPatternLenAt pos digits
    else 
        let atPos = repeatedPatternLenAt pos digits
        if atPos > 0 then atPos
        elif pos > (Seq.length digits) / 2 then 0
        else repeatedPatternLenAtOrPast (pos + 1) digits

let repeatedPatternLen digits start = 
//    printfn "%d" <| Seq.length digits
//    printfn "Seq is %s" <| stringify digits
    let maxSkip = 10
    let mutable skip = maxSkip
    let len = Seq.length digits
    if maxSkip > len then skip <- len - 2
    let startpos =  seq { 0 ..  skip }

    let mx = ref 0

    for i in startpos do
        let subSeq = Seq.skip i digits |> Seq.cache
        let curr = repeatedPatternLenAtOrPast 1 subSeq 

        if curr > !mx then
            mx := curr
    !mx

let rpl n start = 
    let cachedDigits = Seq.cache <| digits n
    repeatedPatternLen cachedDigits start

let rplinverseWithStartPos (m : BigInteger) (start : int) = 
    (m, (rpl (numerator/m) start))


let run = 
//    let x = rplinverseWithStartPos 89I 1
//    printfn "%d has %d recurrence %A" (int (fst x)) (snd x) (numerator / (fst x))
//    let numSeq = Seq.cache <| Seq.m  rplinverse (seq { 1I .. 1000I })
//    let maxN = Seq.maxBy (fun x -> snd x) numSeq
//    for a in Seq.filter (fun x -> snd x > 3) numSeq do
//        printfn "%d has %d recurrence in %A" (int (fst a)) (snd a)  (numerator/(fst a))   
    let mutable maxN = (0I, 1)
    let found = new HashSet<BigInteger>()
    for k in 1 .. 50 do 
        printfn "PASS %d" k
        printfn "Max so far has: %d has %d recurrence" (int (fst maxN)) (snd maxN)    
        numerator <- numerator * 1000I

//        let nums = [821; 937; 977] 
        for i in 1I ..  999I do
            if not (found.Contains i) then

                let sw = Stopwatch.StartNew()
                let curr = rplinverseWithStartPos i (snd maxN)
            
                let score = snd curr
                if score > 0 then 
                    ignore <| found.Add i
                    printfn "Found repeated pattern for %A of len %d: %A" i score (numerator / i)

                if score > snd maxN then
                    maxN <- curr
                    printfn "%A has taken start pos to %A in %d" (fst maxN) (snd maxN) sw.ElapsedMilliseconds
                    //printfn "%A" (numerator/(fst maxN))

    printfn "Max: %d has %d recurrence" (int (fst maxN)) (snd maxN)
    printfn "%A" (numerator/(fst maxN))
    int (snd maxN)

