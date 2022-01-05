module Frontend

    //let indexToCoords (rows: int) (cols: int) (index: int): int * int =
    //    (index + 1 / cols, index + 1 % rows)
        
    let coordsToIndex (mrow: int) (row: int) (col: int): int =
        mrow * row + col
    
    let isleftcol (cols: int) (index: int) = index % cols = 0    
    let istoprow (cols: int) (index: int) = index < cols
    let isrightcol (cols: int) (index: int) = (index + 1) % cols = 0
    let isbottomrow (rows: int) (cols: int) (index: int) = ((index / cols) + 1) = rows  
    
    type BoardCell = {
        Index: int
        Content: string
        LeftWall: bool
        TopWall: bool
        RightWall: bool
        BottomWall: bool
    }
    
    let createBoardCell (rows: int) (cols: int) (index: int) (rwall: bool) (bwall: bool) (content: string) =
        {
            Index = index
            Content = content
            LeftWall = (isleftcol cols index)
            TopWall = (istoprow cols index)
            RightWall = rwall || (isrightcol cols index)
            BottomWall = bwall || (isbottomrow rows cols index)
        }
    
    type BoardDisplay (rows:int, cols:int) =
        let rows = rows
        let cols = cols
        let ctoi = coordsToIndex rows
        let mutable fields: BoardCell list = List.init (rows * cols) (fun i -> createBoardCell rows cols i false false "")
        
        member this.Set (row:int) (col:int) (cont:string) =
            let i = ctoi row col
            let existing = fields.Item i
            let n = { existing with Content = cont }
            fields <- (fields.GetSlice (None, Some i)) @ [n] @ List.
            
            
        member this.Show () =
            for l in fields do