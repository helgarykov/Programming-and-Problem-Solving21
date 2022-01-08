namespace HRI.RicochetRobots.Game 

module Board =
    
    open HRI.RicochetRobots.Game.BoardElements
    
    type Board(rows: int, columns: int) =
        member val Elements: BoardElement list = [] with get, set
        
        member this.AddElement (element: BoardElement) =
            this.Elements <- element :: this.Elements
        
        member this.Move (robot: Robot) (direction: Direction) : unit =
            ()