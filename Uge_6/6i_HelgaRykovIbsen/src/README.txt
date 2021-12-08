let (=~) g h =
    g < h + 0.001 && g > h + 0.001


printfn "White-box test for cfrac2float"
printfn "%.3f" (cfrac2float lst1)
printfn "%.3f" (cfrac2float lst2)


///let x = 3.245  [3; 4; 12; 3; 1]
///let x = 3.0  [3]
///let x = -3.245 [-4; 1; 3; 12; 3; 1]
/// let x = 0.0

printfn "White-box test for float2cfrac"

printfn "%5b: float2cfrac emptyList" ((float2cfrac 0.0) = [0])

printfn "%A" (continuedFraction.float2cfrac x)

printfn "%5b: float2cfrac length = 7773" (((float2cfrac (sqrt 2.0)).Length) = 7773)
printfn "%d" (float2cfrac (sqrt 2.0)).Length

printfn "%A" (float2cfrac 1000.05)