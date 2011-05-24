module P30
open System.Diagnostics
open System

//indexing this array is slower than doing math.pow
let pows = [| 
                0;
                1; 
                int <| Math.Pow(2.0, 5.0);
                int <| Math.Pow(3.0, 5.0);
                int <| Math.Pow(4.0, 5.0);
                int <| Math.Pow(5.0, 5.0);
                int <| Math.Pow(6.0, 5.0);
                int <| Math.Pow(7.0, 5.0);
                int <| Math.Pow(8.0, 5.0);
                int <| Math.Pow(9.0, 5.0)
                |] 

let fifthPowSum n = 
    let mutable sum = 0
    for i in digits.ofN n do
        sum <- sum + (int <| (Math.Pow((float i), 5.0)))
    sum

let run = 
  
  //it is possible to work out that no seven digit number can have the property,
  //since the largest seven digit sumOfPows would be 7 * (9**5), which is < 1000000, i.e.
  //not seven digits
  let s = Stopwatch.StartNew()
  let sumOfPows = Seq.filter (fun x -> fifthPowSum x = x) [ 2 .. 1000000 ] |> Seq.sum
  s.Stop()
  printfn "Got sum %d in %d" sumOfPows s.ElapsedMilliseconds

  let mutable curr = 2
  let s2 = Stopwatch.StartNew()
  let mutable sumOfPowsMut = 0
  while curr < 1000000 do
    if fifthPowSum curr = curr then
//        printfn "Found fifth pow sum at %d" curr
        sumOfPowsMut <- sumOfPowsMut + curr
//        printfn "Sum is now %d" sumOfPows
    curr <- curr + 1

  s2.Stop()
  printfn "Got sum %d in %d" sumOfPowsMut s2.ElapsedMilliseconds
  sumOfPows