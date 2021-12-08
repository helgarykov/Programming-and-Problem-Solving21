open continuedFraction


printfn "Black-box test for cfrac2float"
printfn "%5b: emptyList" (cfrac2float [] = 0.0)
printfn "%5b: OTail" (cfrac2float [10] = 10.0)
printfn "%5b: illegal list 1 divide by 0" (cfrac2float [10; 0] = infinity)
printfn "%5b: single element list" (cfrac2float [4] = 4.0)
printfn "%5b: negative integer list" (cfrac2float [-2; -5; -10; -15; -3; -5] =~ -2.196)
printfn "%5b: large integer list" (cfrac2float [100; 1000] =~ 100.00100)


printfn "White-box test for cfrac2float"
printfn " %5b: Branch 1" (cfrac2float [] = 0.0)
printfn " %5b: Branch 2" (cfrac2float [4] = 4.0)
printfn " %5b: Branch 3" (cfrac2float [3; 4; 12; 4] = 3.245)
printfn " %5b: Branch 3" ((cfrac2float [-2; -5; -10; -15; -3; -5]) =~ -2.196)
printfn " %5b: Branch 3" ((cfrac2float [100; 1000]) =~ 100.00100)
printfn " %5b: Branch 3" ((cfrac2float [10; 0]) = infinity)


printfn "Black-box test for float2cfrac"
printfn "%5b: float2cfrac x = 0" ((float2cfrac 0.0) = [0])
printfn "%5b: float2cfrac x is a float" ((float2cfrac 3.245) = [3; 4; 12; 3; 1])
printfn "%5b: float2cfrac x is a negative float" ((float2cfrac -3.245) = [-4; 1; 3; 12; 3; 1])
printfn "%5b: float2cfrac x is an integer" ((float2cfrac 3.0) = [3])
printfn "%5b: float2cfrac x is a negative integer" ((float2cfrac -3.0) = [-3])
printfn "%5b: float2cfrac x is an irrational number (sqrt 2)" ((float2cfrac (sqrt 2.0))= [int infinity])                                                                       
//Given x is an irrational number, the expected result is a non-finite list of intigers. Tolerance is set to 0.001 and this produces a finite list of integrs.
printfn "%5b: float2cfrac x irrational number list length = 31" (((float2cfrac (sqrt 2.0)).Length) = 31)
printfn "%5b: float2cfrac x is a large number" ((float2cfrac 1000.3456)= [1000; 2; 1; 8; 2; 1; 1; 4])


printfn "White-box test for float2cfrac"
printfn "%5b: Branch 1: x is an integer" (float2cfrac 3.0 = [3])
printfn "%5b: Branch 1: x is a negative integer" (float2cfrac -3.0 = [-3])
printfn "%5b: Branch 2: x is a positive float" (float2cfrac 3.245 = [3; 4; 12; 3; 1])
printfn "%5b: Branch 2: x is a negative float" (float2cfrac -3.245 = [-4; 1; 3; 12; 3; 1])
printfn "%5b: Branch 2: x is an irratioanl number (sqrt 2) list length = 31" (((float2cfrac (sqrt 2.0)).Length) = 31)

