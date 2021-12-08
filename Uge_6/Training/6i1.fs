///<summary>Given a float, find the corresponding continued fraction as a list of integers.</summary>
///<param name="x">A float.</param>
///<param name="q">A floored x.</param>
///<param name="r">The difference between x and q.</param>
///<param name="x1">The quotient of 1 and r.</param>
///<returns>The list of integers.</returns>

let tolerance = 0.00001
let inline (=~) (x : float) (y : float) = abs(x-y)<tolerance




let rec float2cfrac (x: float) : list<int> =
    let q = floor x
    let r = x - q
    let x1 = 1.0/r
    if abs(r-0.0)<0.0001 then
        [(int q)]
    else 
        let continuedFraction = [(int q)] @ (float2cfrac x1)
        continuedFraction
        
printfn "%A" (float2cfrac 3.245)



