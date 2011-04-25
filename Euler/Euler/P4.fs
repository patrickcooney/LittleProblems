module P4

let todigits n = 
    let mutable digits = []
    let mutable c = n

    while c > 0 do
        let digit = (c % 10)
        digits <- digit :: digits
        c <- int ((c - digit) / 10)

    digits


let palindrome n = 
    let digits = todigits n
    
    let mutable h = 0
    let mutable t = digits.Length - 1

    while h <= t && digits.[h] = digits.[t] do
        h <- h + 1
        t <- t - 1

    h > t

let run = 
    let mutable maxpal = 0
    for i in 111 ..  999 do
        for j in 111 .. 999 do
            let pal = i * j
            if pal |> palindrome && pal > maxpal then 
                maxpal <- i * j
    printf "%d" maxpal
