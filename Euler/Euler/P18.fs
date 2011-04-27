module P18

open System
open System.Collections.Generic

let s = "75
95 64
17 47 82
18 35 87 10
20 04 82 47 65
19 01 23 75 03 34
88 02 77 73 07 63 67
99 65 04 28 06 16 70 92
41 41 26 56 83 40 80 70 33
41 48 72 33 47 32 37 16 94 29
53 71 44 65 25 43 91 52 97 51 14
70 11 33 28 77 73 17 78 39 68 17 57
91 71 52 38 17 14 91 43 58 50 27 29 48
63 66 04 68 89 53 67 30 73 16 69 87 40 31
04 62 98 27 23 09 70 98 73 93 38 53 60 04 23"

let maxLine = 14

type Node = { value : int; best: int; left : Node option; right: Node option}

let getLeft line nodeNum (lineToNodes : Dictionary<int, Node[]>) =
    if line = maxLine then None
    else
        Some (lineToNodes.[line + 1].[nodeNum])
        
let getRight line nodeNum (lineToNodes : Dictionary<int, Node[]>) =
    if line = maxLine then None
    else
        Some (lineToNodes.[line + 1].[nodeNum + 1])

let getBest line nodeNum nodeValue (lineToNodes : Dictionary<int, Node[]>) =
    if line = maxLine then nodeValue
    else 
        let lVal = (getLeft line nodeNum lineToNodes).Value.best
        let rVal = (getRight line nodeNum lineToNodes).Value.best
        nodeValue +  (max lVal rVal)

let getGraph =
    let lines = s.Split([|'\r'; 'n'|], StringSplitOptions.RemoveEmptyEntries)
    let lineToNodes = new Dictionary<int, Node[]>()
    for line in lines.Length - 1 .. -1 .. 0 do
        let nums = lines.[line].Split([| ' ' |], StringSplitOptions.RemoveEmptyEntries)
        printfn "%d %d" line nums.Length
        let nodes = Array.init (line + 1) (fun y -> 
            printfn "y %d line %d" y line
            
            { value = Int32.Parse(nums.[y]); best = (getBest line y (Int32.Parse(nums.[y])) lineToNodes); left = (getLeft line y lineToNodes); right = (getRight line y lineToNodes) })
        lineToNodes.Add(line, nodes)
        printfn "finished line %d" line
    
    lineToNodes

let run = 
    let g = getGraph
    let r = g.[0].[0].best

    printfn "%d" r