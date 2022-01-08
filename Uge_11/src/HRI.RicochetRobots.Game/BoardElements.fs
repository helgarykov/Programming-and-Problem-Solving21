namespace HRI.RicochetRobots.Game

module BoardElements =

    open BoardDisplay
    
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
            
        member this.WithMove direction =
            match direction with
            | North -> Position (this.Row - 1, this.Column)
            | East -> Position (this.Row, this.Column + 1)
            | South -> Position (this.Row + 1, this.Column)
            | West -> Position (this.Row, this.Column - 1)
            
        member this.WithMoveSteps direction steps =
            let rec r (n: int) (acc: Position) =
                match n with
                | 0 -> acc
                | i -> r (n - 1) (this.WithMove direction)
                
            r steps this
    
    type Action =
        | Stop of Position
        | Continue of Direction * Position
        | Ignore

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
            Ignore

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
        override this.RenderOn _ = ()
        
    let along (initial: Position) dir length f =
        let offsets = List.init (abs length) id
        
        for offset in offsets do
            let p = initial.WithMoveSteps dir offset
            f p.Value

    let dfunc (display: BoardDisplay) direction =
        match direction with
        | North | South -> display.SetRightWall
        | West | East -> display.SetBottomWall

    [<AbstractClass>]
    type Wall(row: int, col: int, length: int) =
        inherit BoardElement()
        member val Position = Position(row, col) with get, set
        member val Length = length with get, set
        abstract member Direction: unit -> Direction

        override this.RenderOn display =
            let dir = this.Direction()
            along this.Position dir this.Length (dfunc display dir)

    type VerticalWall(row: int, col: int, length: int) =
        inherit Wall(row, col, length)

        override this.Direction() = match this.Length with
                                    | x when x > 0 -> North
                                    | _ -> South
    
    type HorizontalWall(row: int, col: int, length: int) =
        inherit Wall(row, col, length)
        member val Position = Position (row, col) with get, set
        member val Length = length with get, set

        override this.Direction() = match this.Length with
                                    | x when x > 0 -> East
                                    | _ -> West