///<sumamry>Imperativ solution: Given a function that transposes 'a to 'b, returns a list of 'b.</summary>

///'a -> 'b
///let f1 (x: int) = float x
///let f2 x = x + 2
///let f3 x = sprintf "Hej %A" x

let map (f: 'tstart -> 'tslut) (x: list<'tstart>): list <'tslut> =
    let mutable newX = []

    for startItem in x do              //startItem er 1.element i x-listen
        let slutItem = f startItem     //slutItem er 1.element i newX (slutItem kommer til at stå for 1., 2., osv alle elementer i listen)
        newX <- slutItem :: newX     // lav en ny liste, hvor slutItem er sat som head. Assign til newX.

        /// newX <- newX @ [slutItem] / sæt tomme newX sammen med en ny liste (the transformed x-liste, som indeholder slutitems), der indeholder slutItem. Mindre effektiv løsning end linje 17, da T(n)=n^2

    List.rev newX  // reverser alle elementer på en gang til sidst; tager n tid, som er mindre emd i linje 15.


///<sumamry>Functional solution: Given a function that takes 'a and outputs 'b, returns a list of 'b from a list of 'a.</summary>

let rec map (f: 'a -> 'b) (x: list<'a>) : list<'b> =
    match x with
    | [] -> []
    | head :: tail -> (f head) :: (map f tail)    //() betyder at det der er inde i bliver lavet af f til en liste); f head skal ikke være rekrusiv da det kun er et element som vi skal køre funktion på.


let halemap (f: 'a -> 'b) (x: list<'a>) : list<'b> =
    let rec map (remaininglist: list<'a>) (acc: list<'b>) =   //akkumulatoren er altid af den samme type som outputtet.
        match remaininglist with
        | [] -> List.rev acc                                  //List.rev er en indbygget funktion i F#, som sætter elementer i listen i omvendt rækkefølge.
        | head :: tail -> map tail ((f head) :: acc)          //1) map indeholder 2 argumenter: a list (her skal vi køre vores f-funktion på dens 1.element) and acc; map er halerekrusiv fordi det er rekrusiv f der bliver kaldt sidst.

    map x []                               // x er vores liste af 'a og [] er akkumulatoren, som er sæt i linje 31 til en [].

/// let x1 = [1; 2; 3; 4]
/// let f1 = (fun x -> x + 2)
/// map initial call: remaininglist = [1; 2; 3; 4], acc = []
/// map 2nd call:     remaininglist = [2; 3; 4], acc = [3]
/// map 3rd call:     remaininglist = [3; 4], acc = [4; 3]
/// map 4th call:     remaininglist = [4], acc = [5; 4; 3]
/// map 5th call:     remaininglist = [], acc = [6; 5; 4; 3]








(*let originalList = [2; 3; 4]
let finalList =
    rec map (fun x -> x * 2) (originalList)
    match x with
    | [] -> 0
    | _ -> map x originalList*)



/// printfn "%A" (finalList 2 originalList)



(*let rec map (lst: list<'a>) : list<'b> =
    match x with
    | lst -> map x lst

let kat = rec map 3 [2; 3; 1]

printfn "%A" (kat)*)
