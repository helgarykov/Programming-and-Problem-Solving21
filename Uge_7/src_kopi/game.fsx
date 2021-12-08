open Rotate

let inputSize = System.Console.ReadLine()
let toUint = uint inputSize
let gameboard = scramble (create toUint) 100u
printfn "%s" (board2Str gameboard)

let rec gameloop (gameboard) = 
    if solved (gameboard) then 
        printfn "Hooray"
        exit
    else
        let input = System.Console.ReadLine()
        let asUint = uint input 
        let asPosition = Position asUint
        let gameboard = rotate gameboard asPosition
        printfn "%s" (board2Str gameboard)
        gameloop (gameboard)
        
gameloop (gameboard)


