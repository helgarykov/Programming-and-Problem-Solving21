open vec2d

let vector = len (-4.0, 2.5)
let angle = ang (2.0, 3.0)

// This will round it down to three decimals.
let roundedLength = round 3 vector
let roundedAngle = round 3 angle

printfn " Black-box test for len x, y"
printfn " %5b: (-4.0, 2.5) length vector (rounded) = 4.717" (round 3 (len (-4.0, 2.5)) = 4.717)
printfn "%5b: (0.0, 0.0) length vector (rounded) = 0" (round 3 (len (0.0, 0.0)) = 0.0)
printfn "%5b: (24764.165, 4384.7485739) length vector (rounded) = 50476.941" (round 3 (len (24764.156, 43984.7485739)) = 50476.941)


printfn " Black-box test for ang x, y"
printfn " %5b: (2.0, 3.0) angle (rounded) = 0.983" (round 3 (ang (2.0, 3.0)) = 0.983)
printfn " %5b: (-2.0, -3.0) angle (rounded) = -2.159" (round 3 (ang (-2.0, -3.0)) = -2.159)
printfn " %5b: (0.0, 0.0) angle (rounded) = 0.0" (round 3 (ang (0.0, 0.0)) = 0.0)
printfn " %5b: (50879.2, -30400.0) angle (rounded) = -0.539" (round 3 (ang (50879.2, -30400.0)) = -0.539)


printfn " Black-box for vectorSum x1, y1, x2, y2"
printfn " %5b: (x1 3.0, y1 4.0), (x2 2.0, y2 5.0)  vectorSum = (5, 9)" (add (3.0, 4.0) (2.0, 5.0) = (5.0, 9.0))
printfn " %5b: (x1 -30.0, y1 -100.0)(x2 -1000.0,y2 -20.0) vectorSum = (-1030.0, -120.0)" (add  (-30.0, -100.0)(-1000.0, -20.0) = (-1030.0, -120.0))
printfn " %5b: (x1 0.0, y1 0.0), (x2 0.0, y2 0.0) vectorSum = (0.0, 0.0)" (add (0.0, 0.0) (0.0, 0.0) = (0.0, 0.0))
