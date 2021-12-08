
//3ø4(a)
let f (a : int) (b : int) (x : int) : int = a * x + b
let y = f 3 0 4
let c = f 3 1 4
let d = f 3 2 4
let e = f 3 3 4
let g = f 3 4 4
let j = f 3 5 4
printfn "%A" y
printfn "%A" c
printfn "%A" d
printfn "%A" e
printfn "%A" g
printfn "%A" j


//3ø4(b)
for i = 0 to 5 do
    let f (a : int) (b : int) (x : int) : int = a * x + b
    let y = f 3 4 i
    do printfn "%A" y





//3ø4(c)
let mutable i = 0
let f (a : int) (x : int) (b : int) : int = a * x + b
while i <= 5 do
      let y = f 3 i 4
      i <- i + 1
      printfn "%A" y


      
//3ø5 (a)
let fac (n : int) : int =
    let mutable counter = 1
    let mutable local = 1
    while counter <= n do
        local <- local * counter
        counter <- counter + 1
    local
printfn "%A" (fac 5)


//4 spaces for 1. indentation, 8 spaces for 2. indentation

(* 3ø5 (b) Write a program, which asks the user to enter the number n using the keyboard and which writes the result of fac n.*)

System.Console.Write "Enter n: "
let input = int (System.Console.ReadLine () )
let fac (n: int) : int =
    let mutable local = 1
    let mutable counter = n
    while conunter > 0 do
        local <- local * counter
	counter <- counter - 1
    local
let call = fac input
do printfn "%A" call








 



    







