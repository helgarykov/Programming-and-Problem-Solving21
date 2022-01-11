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
                    