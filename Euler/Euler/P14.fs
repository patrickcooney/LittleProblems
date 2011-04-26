module P14

open System.Collections.Generic

let next (n : uint64) = 
    if n % 2UL = 0UL then n/2UL
    else 3UL * n + 1UL

let rec seqLen n precedingLen = 
    if n = 1UL then precedingLen + 1UL
    else seqLen (next n) (precedingLen + 1UL)

let rec memoSeqLen (n : uint64) (lenMap : Dictionary<uint64, int>) = 
    if n = 1UL then 1
    elif lenMap.ContainsKey(n) then
        lenMap.Item(n)        
    else
        let mutable curr = n
        let mutable count = 0
        while curr <> 1UL && not (lenMap.ContainsKey curr) do
            count <- count + 1
            curr <- next curr

            if count % 100 = 0 then
                printfn "at %d for %d" count n
            
        if lenMap.ContainsKey curr then
            count <- count + lenMap.Item(curr)
        else 
            count <- count + 1
        
        lenMap.Add(n, count)    
            
        count
        
             


let run = 
    let lens = new Dictionary<uint64, int>()
    for i in 1UL .. 1000000UL do
        ignore <| memoSeqLen i lens

    let mutable k = 0UL
    let mutable v = -1

    for kvp in lens do
//        printfn "%d %d" kvp.Key kvp.Value        
        if kvp.Value > v then
            v <- kvp.Value
            k <- kvp.Key

    printfn "%d %d" k v