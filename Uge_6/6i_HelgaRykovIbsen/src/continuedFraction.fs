module continuedFraction

//let z = 2 + 4

//let (+) a b = 


let tolerance = 0.001
let (=~) g h =
    g < h + tolerance && g > h - tolerance


let rec cfrac2float (lst: list<int>) : float =
    match lst with
    | [] -> float 0                                         // Branch 1: returnerer 0 hvis listen er tom ved startkaldet.
    | head :: [] -> float head                              // Branch 2: returnerer head, når head er det eneste element.
    | head :: tail -> (float head) + 1.0/(cfrac2float tail) // Branch 3: laver det rekrusive kald på udvejen og laver division på tilbagevejen.


let rec float2cfrac (x: float) : list<int> =
   let q = floor x
   let r = x - q
   let x1 = 1.0/r

   match r with
   | _ when r =~ 0.0 -> [(int q)]                            // Branch 1: returnerer en liste af heltal, hvis x er et heltal.
   | _ -> [(int q)] @ (float2cfrac x1)                       // Branch 2: laver en liste af heltal via rekrution og ender med at tilføje q som head.
 


(* Parkovs version af float2cfrac:

let rec float2cfrac (x: float) : list<int> =    //kan også laves halerekrusivt
    let floored = floor x
                                                 // let remainder = x - floor x
    match (x - (floored)) with 
    | value when (abs value) < 0.001 -> [(int floored)]     /// value (kan være hvad som helst, fx i)= match expression, som her er den evaluerede værdi af (x-floored)
    | value -> (int floored) :: (float2cfrac (1.0 / (x - floored)))*)





let rec float2cfrac (x: float) : list<int> =
   let q = floor x
   let r = x - q
   let x1 = 1.0/r

   match r with
   | i when (abs) < 0.001 -> [(int q)]
   | i -> (int q) :: (float2cfrac x1)
     