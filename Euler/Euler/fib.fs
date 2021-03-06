﻿module fib

let first = 
    1I

let second = 
    1I

let next prev prevprev = 
    prev + prevprev


let fibs = 
    seq {
        yield first
        yield second

        let prev = ref second
        let prevprev = ref first
        while true do
            let next = !prev + !prevprev
            yield next
            prevprev := !prev
            prev := next
    }
