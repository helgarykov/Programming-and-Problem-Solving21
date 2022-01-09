namespace HRI.RicochetRobots.Game

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
            
        member this.WithMove direction = move this direction
        member this.WithMoveSteps direction steps = moveSteps this direction steps