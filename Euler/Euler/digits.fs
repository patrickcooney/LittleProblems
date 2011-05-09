module digits

open System

let digitsInt32ToStr n = 
    seq {
        let s = n.ToString()
        for x in s.ToCharArray() do
            if x >= '0' && x <= '9' then
                yield Int32.Parse(x.ToString())
    }

let ofN n = 
    seq {
         let curr = ref n
         while !curr > 0 do
            yield !curr % 10
            curr := !curr / 10
    }


