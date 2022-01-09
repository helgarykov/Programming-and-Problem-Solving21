namespace HRI.RicochetRobots.Game

module BoardElements =

    open BoardDisplay
    open Movement
    
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