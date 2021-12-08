open vec2d

let vector = len (-4.0, 2.5)
// This will produce a long float.
printfn " %f: length vector before round" vector

//The Black-box test displays "false" because it does not exactly match yet.
printfn " %5b: length vector before round = 4.717" (vector = 4.717)

// This will round it down to three decimals.
let roundedLength = round 3 vector

//This dispalys the new number after rounding.
printfn " %f: length vector after rounding off" roundedLength

//This dispalys "true", because it is now comparable
//to a number with just three decimals.
printfn " %5b: length vector after rounding off = 4.717" (roundedLength = 4.717)


// This will produce a long float.
let angle = ang (2.0, 3.0)

// This shows a long float.
printfn " %f: angle before round" angle

//The Black-box test displays "false" because it does not exactly match yet.
printfn " %5b: angle before round = 0.983" (angle = 0.983)

// This will round it down to three decimals.
let roundedAngle = round 3 angle

//This dispalys the new number after rounding.
printfn " %f: angle after rounding off" roundedAngle

//This dispalys "true", because it is now comparable
//to a number with just three decimals.
printfn " %5b: angle after rounding off = 0.983" (roundedAngle = 0.983)

let vector1 = (3.0, 4.0)
let vector2 = (2.0, 5.0)
let vectorSum = vec2d.add vector1 vector2

//This dispalys "true", because it produces two floats with 0 decimals.
printfn " %5b: vectorSum = (5, 9)" (vectorSum = (5.0, 9.0))
