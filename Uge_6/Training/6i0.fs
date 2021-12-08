///<summary>Given a continued fraction as a list of integers, find the corresponding float.</summary>
///<param name="lst">The list of integers.</param>
///<returns> The corresponding decimal number.</returns>

(*let rec cfrac2float (lst: list<int>) : float =
    match lst with
    | [] -> float 0                                         // Branch 1: returnerer 0 hvis listen er tom ved startkaldet.
    | head :: [] -> float head                              // Branch 2: returnerer head, når head er det eneste element.
    | head :: tail -> (float head) + 1.0/(cfrac2float tail) // Branch 3: laver det rekrusive kald på udvejen og laver division på tilbagevejen.

printfn "%A" (cfrac2float [3; 4; 12; 4])
printfn "%A" (cfrac2float [3; 0])*)



///<summary>The imperative version of the function above.</summary>

let cfrac2floatImperative (lst: list<int>) : float =
    if lst.IsEmpty then
        0.0
    elif lst.Tail.IsEmpty then
        float lst.Head
    elif lst.Tail.Head = 0 then
        0.0
    else
        let mutable decimalNumber = 0.0
            for counter = 0 to lst.Length do
                decimalNumber <- decimalNumber + (1.0/ lst.Item counter)
                counter <- counter + 1
        decimalNumber 
         
           
let lst1 = [3; 4; 12; 4]

printfn "%A" (cfrac2floatImperative lst1)


(*///<summary>Given a continued fraction as a list of integers, find the corresponding float, imperatively.</summary>
let cfrac2floatImperative (lst: list<int>) : float =
    if lst = [] then   // med imperative stil kan man ikke bruge patterns som []
        0.0
    elif lst.Tail = [] then
        float lst.Head
    elif lst.Tail = [0] then
        0.0
    else
        let mutable decimalNumber = 0.0
        let mutable counter = 0   // behøves ikke da counter bliver introduceret direkte i for-løkken
            for counter = 0 to lst.Length do
                decimalNumber <- (float lst.Head) + (1.0/ lst.Tail) //ikke hele tail, men det element i halen som svarer til counter(index'et)
                counter <- counter +1
        decimalNumber 
         
           

let lst1 = [3; 4; 12; 4]

printfn "%A" (cfrac2floatImperative lst1)*)