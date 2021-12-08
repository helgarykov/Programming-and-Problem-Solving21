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
        | _ when acc.Length = n -> acc  // The acc length has the right length, so we should return it.      
        | [] -> acc  // The original list has no more items, so we should return what we have.
        | head :: tail -> r tail (head::acc)  // Add the next item.
    
    List.rev (r list []) // The 'r' function will reverse it, so we reverse it again before returning.


let create (n:uint) =
    match n with
    | _ when n < 2u -> Board [] // A 1x1 board is not acceptable.
    | _ when n > 5u -> Board [] // 5x5 is max, only 26 letters in alphabet.
    | _ -> Board (slice alphabet (int (n * n))) // Return the board, based on a slice of the alphabet, n times n in size.


let board2Str (b: Board) : string =

    let rec build (alphabet: char list) (acc: string) =
        match alphabet with
        | [] -> acc  // alphabet list is empty, we are done
        | head :: [] -> acc + head.ToString()  // head is last, add it and we are done
        | head :: tail when tail.Length % (board2n b) = 0 -> build tail (acc + head.ToString() + System.Environment.NewLine)  // the current is divisible by n, so insert newline after it
        | head :: tail -> build tail (acc + head.ToString())  // the current is not special, just add it to the string

    let (Board bl) = b  // deconstruct the Board, so bl = char list

    build bl ""  // we start with the board char list and an empty string.


let validRotation (b: Board) (p: Position) : bool =
    let n = uint (board2n b)
    let (Position uPosition) = p // get the position as an uint

    match p with
    | Position v -> (v % n) <> 0u && v <= ((boardlength b) - n) // position is illegal in last column (every nth) and last row (length - n)



let rotate (b: Board) (p: Position) =
    let n = board2n b // get n as an int
    let (Position uPosition) = p // get the position as an uint
    let posAsIndex = (int uPosition) - 1 // convert the 1-based position uint to a 0-based int index

    let (Board chars) = b  // deconstuct Board as a list of characters

    let rec r (index: int) (acc: char list) =  // Function to iterate through all indeces of the list.
        match index with
        | i when i >= chars.Length -> acc  // When index has reached the list length, we are done.
        | i when i = posAsIndex -> r (index + 1) ((chars.Item (index + n)) :: acc)  // If index = p, add item at index + n instead.
        | i when i = posAsIndex + 1 -> r (index + 1) ((chars.Item (index - 1)) :: acc)  // If index = p + 1, add item at index - 1 instead.
        | i when i = posAsIndex + n -> r (index + 1) ((chars.Item (index + 1)) :: acc)  // If index = p + n, add item at index + 1 instead.
        | i when i = posAsIndex + n + 1 -> r (index + 1) ((chars.Item (index - n)) :: acc) // If index = p + n + 1, add item at index - n instead.
        | _ -> r (index + 1) ((chars.Item index) :: acc)  // We are outside rotation area, add the item as is at index.

    let valid = validRotation b p
    let result = 
        match valid with
        | true -> Board (List.rev (r 0 []))  // The result was reversed during add, reverse it again.
        | false -> b

    result // Return true for a board with valid rotatin and false for the board with invalid rotation.



let solved (b: Board) : bool =
    let (Board current) = b  // Extract the current list of chars
    let target = slice alphabet current.Length  // Take a slice of the list alphabet of the same length as that of the Board (current list of chars).
    current = target  // If current equals the slice, that means it is in alphabetical order.



let scramble (b: Board) (m: uint) : Board =
    let (Board l) = b  // Deconstruct Board, so that l is a list of chars

    let rec r (count: int) (acc: Board) =  // Rotate a random area count times.
        let next = random l.Length    // Get a random number between 0 and the length of the board.

        match next with
        | _ when count = (int m) -> acc  // If we have rotated m times already, return the result.
        | next when not (validRotation b (Position (uint next))) -> r count acc  // If the planned rotation is not valid, ignore the result and go again.
        | next -> r (count + 1) (rotate acc (Position (uint next)))  // Call again with incremented counter and performed rotation.
    
    r 0 b     // Call the function with an initial count of 0 and the board.




    


