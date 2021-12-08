///<summary>Given a list of floats, return the last float recursively.</summary>

let rec lastFloat (lst: list<float>) : float =
    match lst with
    | [] -> nan
    | head :: [] -> head 
    | _ :: tail -> lastFloat tail

let lst1 = [1.0; 2.3; 3.5; 4.456]
let lstEmpty = []

printfn "%A" (lastFloat lst1)
printfn "%A" (lastFloat lstEmpty)