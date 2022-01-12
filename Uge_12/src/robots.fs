namespace HRI.RicochetRobots.Game

open System

module Printers =

    let repeat (s: string) (n: int) = String.replicate n s
    let printr (s: string) (i: int option) = printf "%s" (repeat s (defaultArg i 1))
    let prcorner () = printf "+"
    let prhline (i: int option) = printr "-" i
    let prvline (i: int option) = printr "|" i
    let prnewline () = printf "%s" Environment.NewLine
    let prrowsep (cols: int) (cellwidth: int) =
        prcorner ()
        for i = 1 to cols do
            prhline (Some cellwidth)
            prcorner()
        prnewline()


module BoardDisplay =

    open Printers
    
    let strfix (chars: int) (s: string option) =
        match s with
        | None -> "".PadRight(chars)
        | Some s -> s.PadRight(chars).Substring(0, chars)

    let coordsToIndex (cols: int) (row: int) (col: int): int =
        cols * row + col
    
    let isleftcol (cols: int) (index: int) = index % cols = 0    
    let istoprow (cols: int) (index: int) = index < cols
    let isrightcol (cols: int) (index: int) = (index + 1) % cols = 0
    let isbottomrow (rows: int) (cols: int) (index: int) = ((index / cols) + 1) = rows
    
    type BoardCell = {
        Index: int
        Content: string
        RightWall: bool
        BottomWall: bool
    }
    
    let createBoardCell (rows: int) (cols: int) (index: int) (rwall: bool) (bwall: bool) (content: string option) =
        {
            Index = index
            Content = (strfix 2 content)
            RightWall = rwall || (isrightcol cols index)
            BottomWall = bwall || (isbottomrow rows cols index)
        }
    
    type BoardDisplay (rows:int, cols:int) =
        let rows = rows
        let cols = cols
        let c2i = coordsToIndex cols
        let mutable fields: BoardCell list = List.init (rows * cols) (fun i -> createBoardCell rows cols i false false None)
        
        member this.SetCell (cell: BoardCell) =
            let i = cell.Index
            fields <- (fields.GetSlice (None, Some (i - 1))) @ [cell] @ (fields.GetSlice (Some (i + 1), None))
        
        member this.Set (row:int, col:int) (cont:string option) =
            let cell = fields.Item (c2i row col)
            this.SetCell { cell with Content = (defaultArg cont "  ") }
        
        member this.SetBottomWall (row: int, col: int) =
            let cell = fields.Item (c2i row col)
            this.SetCell { cell with BottomWall = true }
            
        member this.SetRightWall (row: int, col: int) =
            let cell = fields.Item (c2i row col)
            this.SetCell { cell with RightWall = true }
        
        member this.ShowRow (row: int) =
            let imin = (cols * row)
            let imax = imin + cols - 1
            
            let cells = fields.GetSlice (Some imin, Some imax)
            
            for cell in cells do
                printf "%s" cell.Content
                if cell.RightWall then
                    prvline None
                else
                    printf " "
            
            prnewline()
            
            for cell in cells do
                prcorner()
                if cell.BottomWall then
                    prhline (Some 2)
                else
                    printf "  "
                    
            prcorner()
            prnewline()
   
        member this.Show () =
            prrowsep cols 2

            for r = 0 to (rows - 1) do
                prvline None
                this.ShowRow r


module Movement =
    
    type Direction = North | East | South | West

    type Position = Position of int * int
    with
        member this.Value =
            let (Position (r, c)) = this 
            (r, c)
        
        member this.Row =
            let (r, _) = this.Value
            r
        
        member this.Column =
            let (_, c) = this.Value
            c
    
    let opposite (direction: Direction) =
        match direction with
        | North -> South
        | East -> West
        | South -> North
        | West -> East

    let move (position: Position) direction =
        match direction with
        | North -> Position (position.Row - 1, position.Column)
        | East -> Position (position.Row, position.Column + 1)
        | South -> Position (position.Row + 1, position.Column)
        | West -> Position (position.Row, position.Column - 1)

    let moveSteps (position: Position) direction steps = 
        let rec r (n: int) (acc: Position) =
            match n with
            | 0 -> acc
            | i -> r (n - 1) (move acc direction)
        
        r steps position

    type Position with
            
        member this.WithMove (direction: Direction) = move this direction
        member this.WithMoveSteps (direction: Direction) (steps: int) = moveSteps this direction steps


module BoardElements =

    open BoardDisplay
    open Movement
    
    type Action =
        | Stop of Position
        | Continue of Direction * Position
        | Ignore
        | Explode
        | AddGoal of Position

    [<AbstractClass>]
    type BoardElement() =
        abstract member RenderOn : BoardDisplay -> unit
        abstract member Interact : BoardElement -> Direction -> Action
        default _.Interact _ _ = Ignore
        abstract member GameOver : BoardElement list -> bool
        default _.GameOver _ = false
    
    type Robot(row: int, col: int, name: string) =
        inherit BoardElement()
        member val Name = name with get, set
        member val Position = Position (row, col) with get, set

        override this.Interact other dir =
            match other with
            | :? Robot as r -> 
                match r.Position.WithMove dir with
                | x when x = this.Position -> Stop r.Position
                | _ -> Ignore
            | _ -> Ignore

        override this.RenderOn display =
            display.Set this.Position.Value (Some this.Name)

        member this.Step (direction: Direction) =
            this.Position <- this.Position.WithMove direction
            
    let robotsOf (boardElements: BoardElement list): Robot list =
        boardElements
        |> List.filter (fun x -> x :? Robot)
        |> List.map (fun x -> downcast x)

    type Goal(row: int, col: int) =
        inherit BoardElement()
        member val Position = Position (row, col) with get, set
        override this.GameOver elements = List.exists (fun (r: Robot) -> r.Position = this.Position) (robotsOf elements)
        override this.RenderOn display = display.Set this.Position.Value (Some "GG")

    type BoardFrame(rows: int, columns: int) =
        inherit BoardElement()
        member val Rows = rows with get, set
        member val Columns = columns with get, set
        override this.Interact element direction =
            let outside (p: Position) =
                p.Row < 0 || p.Column < 0 || p.Row >= this.Rows || p.Column >= this.Columns

            match element with
            | :? Robot as r ->
                match (outside (r.Position.WithMove direction)) with
                | true -> Stop r.Position
                | false -> Ignore
            | _ -> Ignore

        override this.RenderOn _ = ()

    [<AbstractClass>]
    type Wall(row: int, col: int, length: int) =
        inherit BoardElement()
        member val Position = Position(row, col) with get, set
        member val Length = length with get, set
        abstract member Direction: unit -> Direction
        abstract member Blocks: unit -> Direction

        member this.AllPositions () =
            let direction = this.Direction()
            
            List.init (abs length) id 
            |> List.map (fun o -> this.Position.WithMoveSteps direction o)

        override this.RenderOn display =
            let f = match (this.Direction()) with
                    | North | South -> display.SetRightWall
                    | West | East -> display.SetBottomWall

            for cell in (this.AllPositions()) do
                f cell.Value

        override this.Interact element direction =
            let block = this.Blocks()

            let isWallOccupyingSameSpace (w: Wall) (r: Robot) =
                w.AllPositions()
                |> List.exists (fun p -> r.Position = p)

            let isWallAdjacent (w: Wall) (r: Robot) = 
                w.AllPositions()
                |> List.map (fun p -> p.WithMove block)
                |> List.exists (fun p -> r.Position = p)

            match element with
            | :? Robot as r -> 
                let movingOppositeBlocking = direction = opposite block
                let isAdjacent = isWallAdjacent this r
                let isOutsideMovingIn = movingOppositeBlocking && isAdjacent

                let movingAlongBlocking = direction = block
                let isOccupyingSameSpace = isWallOccupyingSameSpace this r
                let isInsideMovingOut = movingAlongBlocking && isOccupyingSameSpace

                let isCollision = isOutsideMovingIn || isInsideMovingOut

                match isCollision with
                | true -> Stop r.Position
                | false -> Ignore

            | _ -> Ignore


    type VerticalWall(row: int, col: int, length: int) =
        inherit Wall(row, col, length)

        override this.Direction() = match this.Length with
                                    | x when x > 0 -> South
                                    | _ -> North

        override this.Blocks() = East
    
    type HorizontalWall(row: int, col: int, length: int) =
        inherit Wall(row, col, length)
        member val Position = Position (row, col) with get, set
        member val Length = length with get, set

        override this.Direction() = match this.Length with
                                    | x when x > 0 -> East
                                    | _ -> West

        override this.Blocks() = South


module Board =
    
    open Movement
    open BoardElements
    
    type Board(rows: int, columns: int) =
        member val Elements: BoardElement list = [] with get, set
        
        member this.AddElement (element: BoardElement) =
            this.Elements <- element :: this.Elements

        member this.RemoveElement (element: BoardElement) =
            let rec r (rem: BoardElement) (elements: BoardElement list) (acc: BoardElement list) =
                match elements with
                | [] -> acc
                | head :: tail -> 
                    match head with
                    | h when h = rem -> r rem tail acc
                    | _ -> r rem tail (head :: acc)

            this.Elements <- (r element this.Elements [])
        
        member this.Move (robot: Robot) (direction: Direction) : unit =
            let rec interact (r: Robot) (d: Direction) (e: BoardElement list) =
                match e with
                | [] -> Ignore
                | head :: tail ->
                    let action = head.Interact r d

                    match action with
                    | Ignore -> interact r d tail
                    | Explode ->
                        this.RemoveElement head
                        this.RemoveElement r
                        action
                    | AddGoal p ->
                        this.AddElement (Goal (p.Row, p.Column))
                        action
                    | _ -> action

            let rec movewhileignored (e: BoardElement list) =
                let a = interact robot direction e

                match a with
                | Ignore ->
                    robot.Step direction
                    movewhileignored e
                | _ -> a

            let others = 
                this.Elements
                |> List.filter (fun x -> not (x = robot))

            movewhileignored others |> ignore

            ()


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


module Extensions =

    type Trap(r: int, c: int) =
        inherit BoardElements.BoardElement()

        member val Position = Movement.Position (r, c)

        override this.RenderOn display =
            display.Set (this.Position.Row, this.Position.Column) (Some "XX")

        override this.Interact element direction =
            match element with
            | :? BoardElements.Robot as r ->
                let collision = (r.Position.WithMove direction) = this.Position
                match collision with
                | true -> BoardElements.Action.Explode
                | false -> BoardElements.Action.Ignore
            | _ -> BoardElements.Action.Ignore

    type Helper(frame: BoardElements.BoardFrame) =
        inherit BoardElements.BoardElement()

        let mutable steps = 0

        let random = System.Random()
        let rpos() =
            Movement.Position (random.Next(0, frame.Rows), random.Next(0, frame.Columns))

        override this.RenderOn display = ()

        override this.Interact element direction =
            steps <- steps + 1

            if (steps % 100 = 0) then
                BoardElements.Action.AddGoal (rpos())
            else
                BoardElements.Action.Ignore

    




        