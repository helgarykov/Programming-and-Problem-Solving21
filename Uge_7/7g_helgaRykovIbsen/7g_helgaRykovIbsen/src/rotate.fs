module Rotate

type Board = Board of char list
type Position = Position of uint

let alphabet = ['a'; 'b'; 'c'; 'd'; 'e'; 'f'; 'g'; 'h'; 'i'; 'j'; 'k'; 'l'; 'm'; 'n'; 'o'; 'p'; 'q'; 'r'; 's'; 't'; 'u'; 'v'; 'w'; 'x'; 'y'; 'z']  



let boardlength (Board list) : uint =
    uint list.Length 


let board2n (b: Board) : int =
    int (System.Math.Sqrt(float(boardlength b)))


let random (max: int) : int =
    let randomizer = System.Random()
    randomizer.Next(max)


let slice (list: 'a list) (n: int) =

    let rec r (l: 'a list) (acc: 'a list) =
        match l with 
        | _ when acc.Length = n -> acc  
        | [] -> acc  
        | head :: tail -> r tail (head::acc)  
    
    List.rev (r list []) 


let create (n:uint) =
    match n with
    | _ when n < 2u -> Board [] 
    | _ when n > 5u -> Board [] 
    | _ -> Board (slice alphabet (int (n * n))) 



let board2Str (b: Board) : string =

    let rec build (alphabet: char list) (acc: string) =
        match alphabet with
        | [] -> acc  
        | head :: [] -> acc + head.ToString()  
        | head :: tail when tail.Length % (board2n b) = 0 -> build tail (acc + head.ToString() + System.Environment.NewLine)  
        | head :: tail -> build tail (acc + head.ToString())  

    let (Board bl) = b  

    build bl ""  


let validRotation (b: Board) (p: Position) : bool =
    let n = uint (board2n b)
    let (Position uPosition) = p 

    match p with
    | Position v -> (v % n) <> 0u && v <= ((boardlength b) - n) 



let rotate (b: Board) (p: Position) =
    let n = board2n b 
    let (Position uPosition) = p 
    let posAsIndex = (int uPosition) - 1 

    let (Board chars) = b  

    let rec r (index: int) (acc: char list) =  
        match index with
        | i when i >= chars.Length -> acc  
        | i when i = posAsIndex -> r (index + 1) ((chars.Item (index + n)) :: acc)  
        | i when i = posAsIndex + 1 -> r (index + 1) ((chars.Item (index - 1)) :: acc)  
        | i when i = posAsIndex + n -> r (index + 1) ((chars.Item (index + 1)) :: acc)  
        | i when i = posAsIndex + n + 1 -> r (index + 1) ((chars.Item (index - n)) :: acc) 
        | _ -> r (index + 1) ((chars.Item index) :: acc)  

    let valid = validRotation b p
    let result = 
        match valid with
        | true -> Board (List.rev (r 0 []))  
        | false -> b

    result 



let solved (b: Board) : bool =
    let (Board current) = b  
    let target = slice alphabet current.Length  
    current = target  



let scramble (b: Board) (m: uint) : Board =
    let (Board l) = b  

    let rec r (count: int) (acc: Board) =  
        let next = random l.Length    

        match next with
        | _ when count = (int m) -> acc  
        | next when not (validRotation b (Position (uint next))) -> r count acc  
        | next -> r (count + 1) (rotate acc (Position (uint next)))  
    
    r 0 b     




    


