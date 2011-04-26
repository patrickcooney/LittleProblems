module P15

open System.Collections.Generic

let max = 20

let rec routesFrom i j (memoRoutes : Dictionary<(int * int), uint64>) = 
    if i = max && j = max then 1UL
    elif memoRoutes.ContainsKey (i, j) then
        memoRoutes.Item((i,j))
    elif i = max then 
        let v = 1UL //routesFrom i (j + 1) memoRoutes
        memoRoutes.Add((i, j), v)
        v
    elif j = max then   
        let v = 1UL //routesFrom (i + 1) j memoRoutes
        memoRoutes.Add((i, j), v)
        v
    else
        let v = (routesFrom i (j + 1) memoRoutes) + (routesFrom (i + 1) j memoRoutes)
        printfn "%d routes at %d/%d" v i j
        memoRoutes.Add((i, j), v)
        v

let run =
    let x = new Dictionary<(int * int), uint64>()
    let routes = routesFrom 0 0 x
    printfn "%d" routes