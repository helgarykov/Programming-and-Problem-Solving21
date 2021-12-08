///<summary> Given an integer N and a power n >= 0, find the value N^n.</summary>

let rec powN (N: int) (n: int) : int =
    if n <= 0 then 1
    else N * powN N (n-1)
let x = 5
let a = 2

printfn "%A" (powN a x)          //a^x