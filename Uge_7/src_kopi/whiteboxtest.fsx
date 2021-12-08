open Rotate

printfn "White-box Test: Create returns as expected"
printfn "Branch: 1 - %b" (create 1u = Board [])  
printfn "Branch: 2 - %b" (create 6u = Board []) 
printfn "Branch: 3 - %b" (create 2u = Board ['a'; 'b'; 'c'; 'd'])
printfn "%5b: (Branch: 3 %A) = %b" (not (create 2u = Board ['a'; 'b'; 'c'])) 2u false



printfn "White-box Test: Board2Str returns as extected"

let legalString1 = "ab" + System.Environment.NewLine + "cd" 
let legalString2 = "abcd" + System.Environment.NewLine + "efgh" + System.Environment.NewLine + "ijkl" + System.Environment.NewLine + "mnop"
let legalString3 = "abc" + System.Environment.NewLine + "def" + System.Environment.NewLine + "ghi"

let illegalString1 = "ab" + System.Environment.NewLine + "c"
let illegalString2 = "bc" + System.Environment.NewLine + "def" + System.Environment.NewLine + "ghi"
let illegalString3 = "a" + System.Environment.NewLine + "bcd"
let illegalString4 = "a" + System.Environment.NewLine + "b" + System.Environment.NewLine + "c" + System.Environment.NewLine + "d"

printfn "Branch: 1 - %b" (board2Str (create 2u) = legalString1)

printfn "%5b: (Branch 1: %A) = %b" (not (board2Str (create 2u) = illegalString1)) 2u false

printfn "Branch: 2 - %b" (board2Str (create 2u) = legalString1)

printfn "%5b: (Branch: 2 %A) = %b" (not (board2Str (create 3u) = illegalString2)) 3u false

printfn "Branch: 3 - %b" (board2Str (create 4u) = legalString2)

printfn "%5b: (Branch: 3 %A) = %b" (not (board2Str (create 2u) = illegalString3)) 2u false

printfn "Branch: 4 - %b" (board2Str (create 3u) = legalString3) 

printfn "%5b: (Branch: 4 %A) = %b" (not (board2Str (create 2u) = illegalString4)) 2u false



printfn "White-box Test: ValidRotaion returns as expected"

let board = create 4u
let invalidRotated1 = validRotation board (Position 4u)
let invalidRotated2 = validRotation board (Position 12u)
let invalidRotated3 = validRotation board (Position 13u)
let invalidRotated4 = validRotation board (Position 16u)

printfn "Branch: 1 - %b" (invalidRotated1 = false)
printfn "Branch: 1 - %b" (invalidRotated2 = false)
printfn "Branch: 1 - %b" (invalidRotated3 = false)
printfn "Branch: 1 - %b" (invalidRotated4 = false)



printfn "White-box Test: Rotate returns as expected"

let finalBoard = Board ['a'; 'b'; 'c'; 'd'; 'e'; 'f'; 'g'; 'h'; 'i']
let rotated3uPos1u = Board ['d'; 'a'; 'c'; 'e'; 'b'; 'f'; 'g'; 'h'; 'i']
let rotated4uPos2u = Board ['a'; 'f'; 'b'; 'd'; 'e'; 'g'; 'c'; 'h'; 'i'; 'j'; 'k'; 'l'; 'm'; 'n'; 'o';'p']
let rotated5uPos1u = Board ['f'; 'a'; 'c'; 'd'; 'e'; 'g'; 'b'; 'h'; 'i'; 'j'; 'k'; 'l'; 'm'; 'n'; 'o';'p'; 'q'; 'r'; 's'; 't'; 'u'; 'v'; 'w'; 'x'; 'y']
let rotated2uPos1u = Board ['c'; 'a'; 'd'; 'b']
let noRotated = Board ['a'; 'b'; 'c'; 'd']

printfn "Branch: 1 - %b" (rotate (create 3u) (Position 9u) = finalBoard)
printfn "Branch: 2 - %b" (rotate (create 3u) (Position 1u) = rotated3uPos1u)
printfn "Branch: 3 - %b" (rotate (create 4u) (Position 2u) = rotated4uPos2u) 
printfn "Branch: 4 - %b" (rotate (create 5u) (Position 1u) = rotated5uPos1u) 
printfn "Branch: 5 - %b" (rotate (create 2u) (Position 1u) = rotated2uPos1u) 
printfn "Branch: 6 - %b" (rotate (create 2u) (Position 4u) = noRotated) 



printfn "White-box test: Solved returns as expected"

let s = solved board 
let rotated = rotate board (Position 2u)
let sr = solved rotated

printfn "%5b: (Branch: 0 %A) = %b" (s = true) board true
printfn "%5b: (Branch: 0 %A) = %b" (not (sr = true)) rotated false



printfn "White-box Test: Scramble returns as expected"

let scrambled = scramble board 2u
let rotatedInvalidPosition = rotate board (Position 4u)
let scrambledInvalidPosition = scramble rotatedInvalidPosition 2u
let rotatedValidPosition = rotate board (Position 11u)
let scrambledValidPosition = scramble rotatedValidPosition 2u

printfn "%5b: (Branch: 1 %A) = %b" (board2Str scrambled = board2Str scrambled) scrambled true
printfn "%5b: (Branch: 2 %A) = %b" (board2Str scrambledInvalidPosition = board2Str scrambledInvalidPosition) rotatedInvalidPosition true
printfn "%5b: (Branch: 2 %A) = %b" (not (board2Str scrambledInvalidPosition = board2Str scrambledValidPosition)) rotatedValidPosition false
printfn "%5b: (Branch: 3 %A) = %b" (board2Str scrambledValidPosition = board2Str scrambledValidPosition) rotatedValidPosition true
printfn "%5b: (Branch: 3 %A) = %b" (not (board2Str scrambledValidPosition = board2Str scrambledInvalidPosition)) rotatedInvalidPosition false