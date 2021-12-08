//Continued fractions library
//Continued fractions are represented as lists of integers.

module continuedFraction 

///<summary>Given a continued fraction as a list of integers, find the corresponding float.</summary>
///<param name="lst">The list of integers.</param>
///<returns> The corresponding decimal number, the output is a float.</returns>
val cfrac2float : lst: int list -> float

///<summary>Given a float, find the corresponding continued fraction as a list of integers.</summary>

// Local parameters inside the function MUST not be described. Only input arguments and the output.
(*///<param name="x">A float.</param>
///<param name="q">A floored x.</param>
///<param name="r">The difference between x and q.</param>
///<param name="x1">The quotient of 1 and r.</param>*)


///<returns>The list of integers.</returns>
val float2cfrac : x: float -> list<int>

///<summary> Given a long float, rounds a decimal value up to three decimals.</summary>
///<param name="tolerance">A rounded float with three decimals.</param> 
///<returns>The float with three decimals.</returns>
val (=~) : float -> float -> bool
 
