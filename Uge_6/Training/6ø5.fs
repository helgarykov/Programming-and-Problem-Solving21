///<summary>Given a list of 'a elements, find the length using recursion.</summary>

let rec length (lst: list<'a>) : int =
    match lst with
    | [] -> 0
    | head :: tail -> 1 + length tail 

let emptyList = []
let oneElement = [20]
let floatList = [1.44; 3.44; 5.89; 8.0; 9.4; 90.3]
let charList = ['d'; 'r'; 'f']
let intList = [1; 40; 30000; 45; 2; 5; 56666; 28]


printfn "%A" (length emptyList)
printfn "%A" (length oneElement)
printfn "%A" (length floatList)
printfn "%A" (length charList)
printfn "%A" (length intList)