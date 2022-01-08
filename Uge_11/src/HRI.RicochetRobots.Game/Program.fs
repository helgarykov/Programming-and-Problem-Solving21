
// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"

open HRI.RicochetRobots.Game.BoardDisplay

let board = BoardDisplay (7, 8)
board.SetRightWall (1, 3)
board.SetBottomWall (1, 3)
board.SetBottomWall (1, 2)
board.SetRightWall (2, 1)
board.SetBottomWall(0, 4)
board.Set (1, 2) (Some "AA")
board.Set (1, 3) (Some "BB")
board.Set (2, 1) (Some "ZZ")
board.Set (2, 1) None
board.Show()