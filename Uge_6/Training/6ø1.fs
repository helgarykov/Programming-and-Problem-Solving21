///<summary> Given an uint number, calculate the faculty.</summary>

let rec fac (n: uint) : uint =
    if n <= 1u then 1u
    else n * fac (n-1u)
let x = 5u

printfn "%A" (fac x)



///<summary> Given an integer 2 and a power n >= 0, find the value 2^n.</summary>
let rec pow2 (n: bigint) : bigint =
    if n <= 0I then 1I
    else 2I * pow2 (n-1I)
let x1 = 200I

printfn "%A" (pow2 x1)



(*

step 5

2^0

1 = 1

--------

step 4

2^1

2 * 1= 2

----------

step 3

2^2

2*2 * 1 = 4
----------

step 2

2^3

2*2 * 2 = 8

------------

step 1

2^4

2*2*2 * 2 = 16

*)