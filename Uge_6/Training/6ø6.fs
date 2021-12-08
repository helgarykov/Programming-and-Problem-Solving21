///<summary>Given a list and an accumulator, find the list length tail-recursively.</summary>


(*let rec lengthAcc (acc: int) (xs: list<'a>) : int =
    match xs with
    | _ when xs = [] -> acc
    | _ when acc = 0 -> xs.Length                       // gren 2 er overflødig da der kun er 2 scenarier: enten listen er tom eller ej.
    | _ -> lengthAcc (acc) (xs.Tail)             // hele pointen med rekrusion er at man ikke bruger indekodede funktioner som "length" eller "tail".
    
let a = 0
let b = []

printfn "%A" (lengthAcc a b)
printfn "%A" (lengthAcc a lst1)
printfn "%A" (lengthAcc a lst1)*)



///<summary> Halerekursiv funktion, som finder længden på en liste uden "length" og "tail" funktioner.</summary>

let rec lengthAcc2 (xs: list<'a>) (acc: int) : int =
    match xs with
    | [] -> acc
    | head :: tail -> lengthAcc2 (tail) (acc + 1)  // (acc + head) if to find sum list



///<summary> For at undgå en fejlkide og et argument som er 0, er akkumulatoren skjult her inde i indrefunktionen "recursivelength".</summary>

let lengthAcc3 (xs: list<'a>) : int =
    
    let rec recursivelength (list: list<'a>) (acc: int) =
        match list with
        | [] -> acc
        | head :: tail -> recursivelength (tail) (acc + 1)

    recursivelength xs 0                                      //her kalder jeg funktionen "recursovelength" med 2 argumenter: xs og 0 (akkumulatoren starter altid med 0)




let lst1 = [1; 2; 3; 4; 5; 6; 7; 8; 9; 10; 11; 12; 13; 14; 15; 16; 17; 18; 19; 20]
    
printfn "%A" (lengthAcc2 lst1 0)
printfn "%A" (lengthAcc3 lst1)
    
    
    
    
    
    



    
    
   
    
    
    