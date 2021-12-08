///Convert a non-negative integer into its binary form.
///E.g., dec2bin 3 = "0b11"

open transform

printfn "Black-box testing of dec2bin n"
printfn "  %5b: n < 0" (dec2bin -1 = "illegal value")
printfn "  %5b: n = 0" (dec2bin 0 = "0b0")
printfn "  %5b: n = 1" (dec2bin 1 = "0b1")
printfn "  %5b: n = 2" (dec2bin 2 = "0b10")
printfn "  %5b: n = 10" (dec2bin 10 = "0b1010")
printfn "  %5b: n = 11" (dec2bin 11 = "0b1011")