///<summary> Given a list of integers, find their sum.</summary>

let rec sum (lst: list<int>) : int =
    match lst with
    | [] -> 0
    | head :: tail -> head + (sum tail)

let zeroLst = []
let oneElementLst = [5]
let lst1 = [3; 5; 7; 10]
let largeNumbers = [50; 900; 50000; 700; 1000]
let negativeNumbers = [-30; -100; -500; -4]


printfn "%A" (sum zeroLst)
printfn "%A" (sum oneElementLst)
printfn "%A" (sum lst1)
printfn "%A" (sum largeNumbers)
printfn "%A" (sum negativeNumbers)






   