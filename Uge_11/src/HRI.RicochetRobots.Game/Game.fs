namespace HRI.RicochetRobots.Game

module Game =
    
    open Board
    open BoardDisplay
    open BoardElements
    open Movement

    type GameCondition = Undecided | Win | Defeat

    let directionInput () =

        let rec r () = 
            System.Console.WriteLine("Press a direction key.")
            let k = System.Console.ReadKey(true)

            match k.Key with
            | System.ConsoleKey.UpArrow -> North
            | System.ConsoleKey.RightArrow -> East
            | System.ConsoleKey.DownArrow -> South
            | System.ConsoleKey.LeftArrow -> West
            | _ -> r()

        r()

    let rec fetchFrame (elements: BoardElement list) =
        match elements with
        | [] -> BoardFrame (4, 7)
        | head :: tail -> 
            match head with
            | :? BoardFrame as frame -> frame
            | _ -> fetchFrame tail

    type Game(board: Board) =
        
        let mutable numsteps = 0
        member val Board = board with get


        member this.IsGameOver() =
            let rec ingoal (elements: BoardElement list) =
                match elements with
                | [] -> false
                | head :: tail ->
                    match head with
                    | :? Goal as goal -> goal.GameOver this.Board.Elements
                    | _ -> ingoal tail

            ingoal this.Board.Elements

        member this.ShowBoard() =
            System.Console.Clear()

            let frame = fetchFrame this.Board.Elements
            let display = BoardDisplay (frame.Rows, frame.Columns)
            let elements = this.Board.Elements

            for element in elements do
                element.RenderOn display

            display.Show()

        member this.PlayerAction() =
            System.Console.WriteLine("Please select a robot: ")

            let i = System.Console.ReadLine()           
            let s = (if i.Length = 1 then (String.replicate 2 i) else i).Substring(0, 2).ToLowerInvariant()
            let r = 
                robotsOf this.Board.Elements
                |> List.filter (fun x -> x.Name = s)
                |> List.tryExactlyOne

            match r with
            | Some robot -> 
                let direction = directionInput()
                this.Board.Move robot direction
                numsteps <- numsteps + 1

                match this.IsGameOver() with
                | true -> Win
                | false -> Undecided

            | None -> Undecided

        member this.GameLoop() =
            let mutable condition = Undecided
            
            while condition = Undecided do
                this.ShowBoard()
                condition <- this.PlayerAction()

            condition

        member this.Play() =
            let result = this.GameLoop()
            
            this.ShowBoard()

            let message = 
                match result with
                | Win -> sprintf "YOU WON in %i steps!" numsteps
                | Defeat -> "Game over. You lost."
                | _ -> "An error occured."

            System.Console.WriteLine(message)
            System.Console.ReadKey() |> ignore
            0

