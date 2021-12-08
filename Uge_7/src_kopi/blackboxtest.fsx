open Rotate 


printfn "Black-box Test: Create returns a board of correct length"
printfn "%5b: Legal Board 5x5" (boardlength(create 5u) = 25u)
printfn "%5b: Legal Board 4x4" ((boardlength (create 4u) = 16u))
printfn "%5b: Illegal Board 1x1" ((boardlength (create 1u) = 0u))
printfn "%5b: Illegal Board 6x6" (boardlength (create 6u) = 0u)



printfn "Black-box Test: Board2Str returns the board characters as a string"

let stringsOf3 = "abc" + System.Environment.NewLine + "def" + System.Environment.NewLine + "ghi"

let stringsOf5 = "abcde" + System.Environment.NewLine + "fghij" + System.Environment.NewLine + "klmno" + System.Environment.NewLine + "pqrst" + System.Environment.NewLine + "uvwxy"

let emptyString = ""

printfn "%5b: abc / def / ghi" (board2Str (create 3u) = stringsOf3)
printfn "%5b: abcde / fghij / klmno / pqrst / uvwxy" (board2Str (create 5u) = stringsOf5)
printfn "%5b: Empty string 6x6" (board2Str (create 6u) = emptyString)
printfn "%5b: Empty string 1x1" (board2Str (create 1u) = emptyString)



printfn "Black-box Test: ValidRotation returns TRUE when position is valid"
printfn "%5b: 4x4, p = 3" (validRotation (create 4u) (Position 3u) = true)
printfn "%5b: 3x3, p = 1" (validRotation (create 3u) (Position 1u) = true)
printfn "%5b: 5x5, p = 10" (validRotation (create 5u) (Position 10u) = false)
printfn "%5b: 5x5, p = 25" (validRotation (create 5u) (Position 25u) = false)
printfn "%5b: 3x3, p = 7" (validRotation (create 3u) (Position 7u) = false)



printfn "Black-box Test: Rotate correctly moves the characters as expected"

let board = create 4u
let expectedOne = ['a'; 'f'; 'b'; 'd'; 'e'; 'g'; 'c'; 'h'; 'i'; 'j'; 'k';  'l'; 'm'; 'n'; 'o'; 'p']
let rotatedOne = rotate board (Position 2u)
let result (Board x) = x

let expectedTwo = ['a'; 'b'; 'c'; 'd'; 'e'; 'f'; 'g'; 'h'; 'i'; 'j'; 'o'; 'k'; 'm'; 'n'; 'p'; 'l']
let rotatedTwo = rotate board (Position 11u)

let noRotation = ['a'; 'b'; 'c'; 'd'; 'e'; 'f'; 'g'; 'h'; 'i'; 'j'; 'k'; 'l'; 'm'; 'n'; 'o'; 'p']
let rotatedThree = rotate board (Position 4u)

let rotatedFour = rotate board (Position 13u)

let rotatedFive = rotate board (Position 16u)

printfn "%5b: Expected after rotating 4x4, p = 2" (result rotatedOne = expectedOne)
printfn "%5b: Expected after rotating 4x4, p = 11" (result rotatedTwo = expectedTwo)
printfn "%5b: Expected no rotation 4x4, p = 4" (result rotatedThree = noRotation)
printfn "%5b: Expected no rotation 4x4, p = 13" (result rotatedFour = noRotation)
printfn "%5b: Expected no rotation 4x4, p = 16" (result rotatedFive = noRotation)



printfn "Black-box Test: Solved should return TRUE when in order"

let originalBoard1 = create 4u
let s = solved originalBoard1

printfn "%5b: Original board 4x4 is solved" (s = true)

printfn "Black-box Test: Solved should return FALSE when not in order"

let originalBoard2 = create 4u
let rotated2 = rotate originalBoard2 (Position 2u)
let rotated7 = rotate originalBoard2 (Position 7u)
let rotated11 = rotate originalBoard2 (Position 11u)
let sr2 = solved rotated2
let sr7 = solved rotated7
let sr11 = solved rotated11

printfn "%5b: Rotated board 4x4, p = 2 is not solved" (sr2 = false)
printfn "%5b: Rotated board 4x4, p = 7 is not solved" (sr7 = false)
printfn "%5b: Rotated board 4x4, p = 11 is not solved" (sr11 = false)



printfn "Black-box Test: Scramble randomly rotates the board as expected"

let board1 = create 4u
let scrambled1 = scramble board1 10u
let scrambled2 = scramble board1 20u
let scrambled3 = scramble board1 100u

printfn "%5b: Scramble Board 4x4 10 times" (scrambled1 = scrambled1)
printfn "%5b: Scramble Board 4x4 20 times" (scrambled2 = scrambled2)
printfn "%5b: Scramble Board 4x4 100 times" (scrambled3 = scrambled3)