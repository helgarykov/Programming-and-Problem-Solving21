///<summary> Given two integers t and n, find the greatest common divisor.</summary>

let rec gcd (t: int) (n: int) : int =
    match n with
        | _ when n = 0 -> t
        | _ -> gcd (n) (t % n)

let example1 = gcd 8 2
let example2 = gcd 2 8
let example3 = gcd 8 0
let example4 = gcd -4 -16
let example5 = gcd 140 96
let example6 = gcd 100 100

printfn "%A" (example1)
printfn "%A" (example2)
printfn "%A" (example3)
printfn "%A" (example4)
printfn "%A" (example5)
printfn "%A" (example6)