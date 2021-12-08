// association lists mapping strings to values of type ’a

type 'a alist = (string * 'a) list

let add (m:'a alist) (s:string) (v:'a) : 'a alist =
    (s,v)::m

let rec look (m:'a alist) (s:string) : 'a option =
    match m with
        [] -> None
       | h::t -> if fst h = s then Some(snd h)
                    else look t s
 
let empty () : 'a alist = []

                                                                        // m = [A -> 3; B -> 8]

                                                                        //let m0 = add (empty ()) "A" 3
                                                                        //let m = add m0 "b" 8

                                                                        //printfn "%A:" (look m "C")
                                                                        //printfn "%A:" (look m "A")
                                                                        //look m "A"

                                                                        //let v = look m "A"

let alisteEksempel : alist<float>= [("hej",3.0) ; ("hygge", 2000.0) ; ("brun",-  20.0)]

                                                                        //let v = look alisteEksempel "hygge" 2000.0

printfn "Black-box test for look-function"
printfn "%A:" (look alisteEksempel "hygge")




type person = {first:string; last:string; age:int}

let xs = [{first="Lene"; last="Andersen"; age=56};
    {last="Hansen"; first="Jens"; age=39}]

let name (p:person) : string = p.first + " " + p.last

let incr_age (p:person) : person = {p with age=p.age+1}

let ys = List.map incr_age xs

printfn "Black-box test for increase-age-function på liste xs"
printfn "%A" (ys)


///<summary>7ø1.Given a day of the week, returns a corresponding integer.</summary>

type weekday = Monday | Tuesday | Wednesday | Thursday | Friday | Saturday | Sunday

let dayToNumber (day: weekday) : int =
    match day with 
    | Monday -> 1
    | Tuesday -> 2
    | Wednesday -> 3
    | Thursday -> 4
    | Friday -> 5
    | Saturday -> 6
    | Sunday -> 7

printfn "Black-box test for dayToNumber"
printfn "%A" (dayToNumber Thursday)


///<summary>7Ø2.Given a week day, return the next day.</summary>

let nextDay (dayNext: weekday) : weekday =
    match dayNext with
    | Monday -> Tuesday
    | Tuesday -> Wednesday
    | Wednesday -> Thursday
    | Thursday -> Friday
    | Friday -> Saturday
    | Saturday -> Sunday
    | Sunday -> Monday

printfn "Black-box test for nextDay"
printfn "%A" (nextDay Sunday)


///<summary>7ø3.Given a number of a week day [1; 7], returns None if n is outside the interval or the number of the week day.</summary>

let numberToDay (n: int): option<weekday> =
    match n with
    | 1 -> Some Monday
    | 2 -> Some Tuesday
    | 3 -> Some Wednesday
    | 4 -> Some Thursday
    | 5 -> Some Friday
    | 6 -> Some Saturday
    | 7 -> Some Sunday
    | _ -> None

  
printfn "Test for numberToDay"
printfn "%A" (numberToDay (dayToNumber Thursday))



type suit = Hearts | Diamonds | Clubs | Spades    // The suit of a card

type rank = Two | Three | Four | Five | Six | Seven | Eight | Nine | Ten | Jack | Queen | King | Ace  // the rank of a card

type card = rank * suit              // Combinaiton of a rank and a suit


//let x = u + r


//let add x y = x + y

let mycard = (Six, Hearts)

printfn "%A" mycard


///<summary>Given a suit of a card, return the suit of the next card.</summary>
//type suit option = None | Some of suit

let succSuit (nextSuit: suit): option<suit> =
    match nextSuit with
    | Hearts -> Some Diamonds
    | Diamonds -> Some Clubs
    | Clubs -> Some Spades
    | _ -> None

printfn "Test for succSuit"
printfn "%A" (succSuit Spades)


///<summary> Given a rank, return the next rank as an optional value.</summary>

let succRank (nextRank: rank): option<rank> =
    match nextRank with
    | Two -> Some Three
    | Three -> Some Four
    | Four -> Some Five
    | Five -> Some Six
    | Six -> Some Seven
    | Seven -> Some Eight
    | Eight -> Some Nine
    | Nine -> Some Ten
    | Ten -> Some Jack
    | Jack -> Some Queen
    | Queen -> Some King
    | King -> Some Ace
    | _ -> None

printfn "Test for succRank"
printfn "%A" (succRank Ace)

///<summary> Given a card, return the next card as an optional value.</summary>

let succCard nextCard =
    match nextCard with
    | (None, Some s) -> Some (Two, s) 
    | (Some Ace, Some Spades) -> None
    | (Some Ace, Some s) -> Some (Two, (succSuit s))                // Some s = "when Some s = Hearts"
    | (Some r, Some s) -> Some ((succRank r), s)


let x = Some 5
let y = None


type Temperature = Celsius of float
                 | Fahrenheit of float
                 | Kelvin of float 

let seaTemperature: Temperature = Celsius 28.5




