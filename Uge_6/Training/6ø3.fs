///<summary>Given a range of uint number, find the sum.</summary>

let rec sumRec (n: uint) : uint =
    if n <= 0u then 0u
    else
        n + sumRec (n - 1u)
let x = 100u

printfn "%A" (sumRec x)