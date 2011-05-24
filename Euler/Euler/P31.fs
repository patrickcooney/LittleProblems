module P31
open System.Collections.Generic
open System.Diagnostics


let rec makeChange amount maxCoin (maxCoinToAvailableCoins : Dictionary<int, int seq>) (memo : Dictionary<int, Dictionary<int, int>>) = 
    if amount = 0 then 
        1
    elif memo.[amount].ContainsKey(maxCoin) then
        memo.[amount].[maxCoin]
    else
        let candidates = Seq.filter (fun x -> x <= amount)  maxCoinToAvailableCoins.[maxCoin]
        let ways = Seq.sum <| Seq.map (fun coin -> makeChange (amount - coin) coin maxCoinToAvailableCoins memo) candidates
        memo.[amount].Add(maxCoin, ways)
        ways


let maxCoinLessThanOrEqualTo n coins : int = 
    let lessThanOrEqual = Seq.filter (fun x -> x <= n) coins
    let max = Seq.max lessThanOrEqual
    max

let run() = 
    let coins = [1; 2; 5; 10; 20; 50; 100; 200]

    let maxToAvail = new Dictionary<int, int seq>()

    let memo = new Dictionary<int, Dictionary<int, int>>()

    let max = 200

    for coin in coins do
        maxToAvail.Add(coin, (Seq.filter (fun c -> c <= coin) coins))

    for i in 1 .. max do
        memo.Add(i, new Dictionary<int, int>())

    makeChange 1 1 maxToAvail memo

    let sw = Stopwatch.StartNew()
    makeChange max max maxToAvail memo
    sw.Stop()

    printfn "got %d for %d in %fms %d tucks" max (memo.[max].[(Seq.max coins)]) (100000.0 * (float sw.ElapsedMilliseconds) / (float Stopwatch.Frequency)) sw.ElapsedTicks


    1

//let allCoinsUnderEach allCoins = 
//    let biggestToAll = new Dictionary<int, int seq>()
//    for x in allCoins do
//        let under = Seq.filter (fun z -> z <= x) allCoins
//        biggestToAll.Add(x, under)
//
//    biggestToAll
//
//let rec allWaysCount (n : int) (maxNextCoin : int) (allCoinsUnderEach : Dictionary<int, int seq>)  (memoWays : Dictionary<int, Dictionary<int, int>>) = 
//    let mutable ways = 0
//    if n = 0 then
//        ways <- 1
//    else if n > 0 then
//        let memoedN = memoWays.ContainsKey(n)
//        let memoedNC = memoedN && memoWays.[n].ContainsKey(maxNextCoin)
//        if memoedNC then
//            ways <- memoWays.[n].[maxNextCoin]
////            printfn "cache hit making %d, %d ways" n ways
//        else
//            if maxNextCoin = 1 then
//                ways <- 1
//            else   
////                printfn "making %d with max %d" n maxNextCoin
//                for c in allCoinsUnderEach.[maxNextCoin] do
//                    if c <= n then 
//                        printfn "%d" c
//                        if memoedN && memoWays.[n].ContainsKey(c) then
//                            ways <- ways + memoWays.[n].[c]
//                        else
//                            ways <- ways +  (allWaysCount (n - c) c allCoinsUnderEach memoWays)
//            if not <| memoWays.ContainsKey(n) then
//                memoWays.Add(n, new Dictionary<int, int>())
//            memoWays.[n].Add(maxNextCoin, ways)
//    ways
//
//let maxCoinLTE x coins = 
//    Seq.filter (fun z -> z <= x) coins |> Seq.max
//
//
//let run() = 
//    let allCoins = [1; 2; 5; 10; 20; 50; 100; 200]
//    let b = allCoinsUnderEach allCoins
//    let memo = new Dictionary<int, Dictionary<int, int>>()
//    for i in 1 .. 2 do
//        ignore <| allWaysCount i (maxCoinLTE i allCoins) b memo
//        
//    let sw = Stopwatch.StartNew()
//    let max = 199
//    let maxCoin = maxCoinLTE max allCoins
//    for i in 1 .. max do
//        let maxCoinForI = (maxCoinLTE i allCoins)
//        ignore <|  allWaysCount i maxCoinForI b memo
//    sw.Stop()
//    printfn "Can make %d in %d ways %d millis to calculate" max (memo.[max].[maxCoin]) sw.ElapsedMilliseconds
//
//    1
