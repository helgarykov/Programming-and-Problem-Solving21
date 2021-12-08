//3ga

/// <summary> Find a when a = 1 + 2 ... n-1 + n, where n is some integer.</summary>
/// <remarks> If n < 1, the function will return 0.</remarks>
/// <parameter name = "n" > Any integer.</param>
/// <returns> The final sum of the first n numbers.</returns>

let sum (n:int) : int =
    let mutable s = 0
    let mutable counter = n
    if n <= 0 then 0
    else
        while counter > 0 do
            s <- s + counter
            counter <- counter - 1
        s


//3gb
(*In order to explain how the formula is related to the sum, consider the sum A = 1 + 2 + ... + n-1 + n. We can now write the same sum, but in reverse order, such that A = n + (n-1) + ... + 2 + 1. Adding these two sums together, we get 2A = (n+1)+(n+1)+(n+1)... n times. As such, we have that 2A = n(n+1), however, since this was 2 times the sum of A, we can now divide by 2 on both sides, thus giving us the formula A = (n(n+1))/2*)
/// <summary> Find the sum of any finite sequence of numbers (n) where sum = 1 + 2 +...+(n-1)+n.<\summary>
/// <param nam = "n"> Any integer.</param>
///<returns> The sum of the finite sequence.</returns>

let simpleSum (n : int) : int =
   n * (n+1)/ 2

//3gc
/// The user inputs an n into the console and is given the output of both function.

printfn "%A" "Find the sum of both functions"
System.Console.Write "Enter n: "
let input = int (System.Console.ReadLine ())
let call1 = sum input
let call2 = simpleSum input
do printfn "%A %A" call1 call2

//3gd

do printfn "   n  sum  simple"

for n = 1 to 10 do
    do printfn "%4d%4d%4d" n (simpleSum n) (sum n)

//3ge
(* The largest value n that the sum-function can calculate is n = 65535, while the largest value that the simpleSum function can calculate is n = 46340. To correctly calculate the sum for larger values of n, the type of integer (which in this case is int) should be changed to int64.*)

///<summary> Find the point at which simpleSum(n) > simpleSum(n-1), which means that we have overflow at that point.</summary>

///<returns> The maximum value of n that the two sum-functions can calculate the value of.</returns>

let mutable n = 1
while simpleSum(n) > simpleSum(n-1) do
    n <- n+1
do printfn "%A" n

let mutable s = 1
while sum(s) > sum(s-1) do
    s <- s+1
do printfn "%A" s
