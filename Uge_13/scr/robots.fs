module Robots

type Direction = North | South | East | West 
type Position = int * int
type Action =
    | Stop of Position
    | Continue of Direction * Position
    | Ignore
///<summary>BoardDisplay works by storing the individual fields in an Array2D as a 2-tuple of strings with 3 characters each. It has methods for setting the content of the fields, and a BuildString method which converts the array to a string</summary>
///<remarks>An example of a field is ("  |","  +"). Here, the first string shows the top row, and the second shows the bottom row. The first string is used for showing board elements, robots, etc, while the bottom row is exclusively used to show vertical walls. A '+' is always shown in the bottom right corner to make the grid of the game clearly visible</remarks>
///<param name="rows">The amount of rows in the board</param>
///<param name="cols">The amount of columns in the board</param>
type BoardDisplay(rows:int,columns:int) =
    let EmptyArrayBuilder x y =    
        match (x,y) with
            | (x,y) when x = (rows-1) && y = (columns-1) -> ("  |","--+") //bottom right corner
            | (_,y) when y = (columns - 1) -> ("  |","  +") 
            | (x,_) when x = (rows - 1) -> ("   ","--+")
            | _,_ -> ("   ","  +")
    let EmptyArray = Array2D.init rows columns (fun x y -> EmptyArrayBuilder x y)
///<summary>The amount of rows in the board</summary>
    member val Rows = rows with get
///<summary>The amount of columns in the board</summary>
    member val Columns = columns with get
///<summary>Runs EmptyArrayBuilder on every field, effectively clearing the array and showing only the walls. This is used to avoid duplicates when robots are moved</summary>
    member this.ResetArray() = Array2D.iteri (fun x y _ -> Array2D.set EmptyArray x y (EmptyArrayBuilder x y)) EmptyArray
///<summary>Modifies a field in the array so the second string in the tuple shows a wall</summary>
///<param name="row">The row of the field which is changed</param>
///<param name="column">The column of the field which is changed</param>
///<returns>Nothing, but modifies the field in the array.</returns>
    member this.SetBottomWall(row:int,column:int) =
        let (top,_) = EmptyArray.[row-1,column-1]
        EmptyArray.[row-1,column-1] <- (top,"--+")
///<summary>Modifies a field in the array so the first string in the tuple shows a wall to the right side of the field</summary>
///<param name="row">The row of the field which is changed</param>
///<param name="column">The column of the field which is changed</param>
///<returns>Nothing, but modifies the field in the array.</returns>
    member this.SetRightWall(row:int,column:int) =
        let (top,bot) = EmptyArray.[row-1,column-1]
        EmptyArray.[row-1,column-1] <- (top.[..1] + "|",bot)
///<summary>Sets the content of a field in the array by modifying the first two characters of the first string in the tuple</summary>
///<param name="row">The row of the field which is changed</param>
///<param name="column">The column of the field which is changed</param>
///<param name="row">The row of the field which is changed</param>
///<param name="cont">The content which is written on the field. Must have a length of exactly two characters</param>
///<returns>Nothing, but modifies the field in the array.</returns>
    member this.Set(row:int,column:int,cont:string) = //cont must have length of 2 chars
        let (top,bot) = EmptyArray.[row-1,column-1]
        EmptyArray.[row-1,column-1] <- (cont + (string top.[2]),bot)
    member this.Print() = do printfn "%A" EmptyArray //for testing only ________________________________________________________________________________________________________________________________________________________________________________________________________________
///<summary>Builds the string which shows the board by showing the content of all fields and adding the outer borders of the board</summary>
///<returns>The string which shows the entire board</returns>
    member this.BuildString() =
        let mutable board = "\n+" + (String.replicate columns "--+") + "\n"
        for r=0 to (rows-1) do
            board <- board + "|"
            for c=0 to (columns-1) do
                board <- board + fst EmptyArray.[r,c]
            board <- board + "\n+"
            for c=0 to (columns-1) do
                board <- board + snd EmptyArray.[r,c]
            board <- board + "\n"
        board
///<summary>Prints the content that BuildString returns</summary>
///<returns>(), but prints the string.</returns>
    member this.Show() = do printfn "%A" (this.BuildString())

///<summary>An abstract class which all board members inherit from. Has methods for rendering on the display, interacting with the robot, and checking if the game is over.</summary>
[<AbstractClass>] 
type BoardElement () =
///<summary>A method for rendering elements on the board display</summary>
    abstract member RenderOn : BoardDisplay -> unit
///<summary>A method for interacting with the element</summary>
    abstract member Interact : Robot -> Direction -> Action 
    default __.Interact _ _ = Ignore
///<summary>A method for rendering elements on the board display</summary>
    abstract member GameOver : Robot list -> bool
    default __.GameOver _ = false

///<summary>The robot class. The Interact method stops other robots from entering the same field, the RenderOn method places the robots name on the board, and the Steop method changes the position of the robot by 1 in a given direction</summary>
///<param name="row">The row of the robots position</param>
///<param name="col">The column of the robots position</param>
///<param name="name">The robots name - this must always be exactly two characters.</param>
and Robot(row:int, col:int, name:string) = 
    inherit BoardElement()
    let mutable position = (row, col)
///<summary>The position of the robot, of the type Position</summary>
    member this.Position with get() = position and set(p) = position <- p
///<summary>Lets the robot interact with other robots. If another robot is about to collide with this robot, it is told to stop in its current position. Otherwise, nothing happens</summary>
///<param name="other">The robot which is moving, and whose trajectory is compared with the location of this robot</param>
///<param name="dir">The direction of the other robots movement</param>
///<returns>Stop if the other robot is about to collide, Ignore otherwise</returns>
    override this.Interact (other:Robot) (dir:Direction) = 
        let robotrow,robotcol = other.Position
        match dir with
            | North when robotrow = (fst position + 1) && robotcol = (snd position) -> Stop(other.Position) 
            | South when robotrow = (fst position - 1)  && robotcol = (snd position) -> Stop(other.Position) 
            | East when robotcol = (snd position - 1)  && robotrow = (fst position) -> Stop(other.Position) 
            | West when robotcol = (snd position + 1)  && robotrow = (fst position) -> Stop(other.Position) 
            | _ -> Ignore
///<summary>Shows the robot on the display by calling the Set method from the board</summary>
///<param name="board">the board display</param>
    override this.RenderOn (board:BoardDisplay) = board.Set((fst this.Position),(snd this.Position),name)
///<summary>The robots name</summary>
    member val Name = name
///<summary>Moves the robot one step in a specified direction</summary>
///<param name="dir">The direction, of type Direction, in which the robot is moved.</param>
///<returns>Changes the current position of the robot</returns>
    member robot.Step (dir:Direction) = 
        match dir with
            | North -> position <- ((fst position) - 1,snd position)
            | South -> position <- ((fst position) + 1,snd position)
            | East -> position <- (fst position,(snd position) + 1)
            | West -> position <- (fst position,(snd position) - 1)

///<summary>The goal object has the GameOver method, which returns true if one of the robots has stopped in the goal.</summary>
///<param name="row">The row of the goals position</param>
///<param name="col">The column of the goals position</param>
and Goal(row:int, col:int) =
    inherit BoardElement()
    member this.Position with get() = (row,col)
///<summary>Returns true if any of the robots that are passed are currently located in this field</summary>
///<param name="r">The robot list, all of the robots positions are compared to this position</param>
///<returns>True if any robots are located in the goal, false otherwise</returns>
    override this.GameOver (r:Robot list) : bool =
        let mutable gameover = false
        for robot in r do
            if robot.Position = this.Position then gameover <- true
        gameover
///<summary>Shows the goal on the display by calling the Set method from the board</summary>
///<param name="board">the board display</param>
    override this.RenderOn (board:BoardDisplay) =
        board.Set((fst this.Position),(snd this.Position),"GO") 
///<summary>The boardframe is the outer edge of the board. The interact method returns Stop when the robot is at the edge of the board</summary>
///<param name="row">The total amount of rows in the board</param>
///<param name="col">The total amount of columns in the board</param>
and BoardFrame(row:int,col:int) =
    inherit BoardElement()
///<summary>RenderOn does nothing, as board frames are rendered by default</summary>
///<param name="board">the board display</param>
    override this.RenderOn (board:BoardDisplay) = ()
///<summary>Checks if the robot is about to collide with the frame</summary>
///<param name="other">The robot which is checked for collisions</param>
///<param name="dir">The direction of the robot</param>
///<returns>Stop if the robot is about to collide, Ignore otherwise</returns>
    override this.Interact (other:Robot) (dir:Direction) =
        let robotrow,robotcol = other.Position
        match dir with
            | North when robotrow = 1 -> Stop (robotrow,robotcol)
            | South when robotrow = row -> Stop (robotrow,robotcol)
            | East when robotcol = col -> Stop (robotrow,robotcol)
            | West when robotcol = 1 -> Stop (robotrow,robotcol)
            | _ -> Ignore

///<summary>The VerticalWall is a wall in the playing field with length n. The RenderOn method sets walls in the n fields that are adjacent to the starting location, and the Interact function returns Stop when the robot tries to move into the wall </summary>
///<param name="row">The row of the walls position</param>
///<param name="col">The column of the walls position</param>
///<param name="n">The length of the wall. If n is negative, the wall goes towards north from the starting point, if it is positive, it goes towards south.</param>
and VerticalWall(row:int,col:int,n:int) =
    inherit BoardElement()
///<summary>The length of the wall. Can be negative, depending on the direction of the wall</summary>
    member this.Length = n
///<summary>The position of the starting point of the wall</summary>
    member this.Position with get() = (row,col)
///<summary>Shows the wall on the display. Goes through the length of the wall in a for loop and sets the wall on all fields that are part of the wall</summary>
///<param name="board">the board display</param>
    override this.RenderOn (board:BoardDisplay) = 
        let (+) a k =  //helper function to calculate location on wall depending on whether n is positive or negative
            if this.Length >= 0 then a+k else a-k
        for k=0 to (abs this.Length) do
            if 0 < ((+) row k) && ((+) row k) <= board.Rows then board.SetRightWall(((+) row k),col) //checks if the field of the wall is still within the bounds of the board
///<summary>Checks if the robot is about to collide with the wall</summary>
///<param name="other">The robot which is checked for collisions</param>
///<param name="dir">The direction of the robot</param>
///<returns>Stop if the robot is about to collide, Ignore otherwise</returns>
    override this.Interact (other:Robot) (dir:Direction) =
        let robotrow,robotcol = other.Position
        match dir with
            | East when robotcol = col && (min row (row+this.Length)) <= robotrow && robotrow <= (max row (row+this.Length)) -> Stop (robotrow,robotcol)
            | West when robotcol = col+1 && (min row (row+this.Length)) <= robotrow && robotrow <= (max row (row+this.Length)) -> Stop (robotrow,robotcol)
            | _ -> Ignore

///<summary>The HorizontalWall is similar to the VerticalWall, except it is horizontal. Positive n-values indicate that the wall goes towards east, and vice versa. </summary>
///<param name="row">The row of the walls position</param>
///<param name="col">The column of the walls position</param>
///<param name="n">The length of the wall. If n is negative, the wall goes towards north from the starting point, if it is positive, it goes towards south</param>
and HorizontalWall(row:int,col:int,n:int) =
    inherit BoardElement()
///<summary>The length of the wall. Can be negative, depending on the direction of the wall</summary>
    member this.Length = n
///<summary>The position of the starting point of the wall</summary>
    member this.Position with get() = (row,col) 
///<summary>Shows the wall on the display. Goes through the length of the wall in a for loop and sets the wall on all fields that are part of the wall</summary>
///<param name="board">the board display</param>
    override this.RenderOn (board:BoardDisplay) = 
        let (+) a k = 
            if this.Length >= 0 then a+k else a-k
        for k=0 to (abs this.Length) do
            if 0 < ((+) col k) && ((+) col k) <= board.Columns then board.SetBottomWall(row,((+) col k))
///<summary>Checks if the robot is about to collide with the wall</summary>
///<param name="other">The robot which is checked for collisions</param>
///<param name="dir">The direction of the robot</param>
///<returns>Stop if the robot is about to collide, Ignore otherwise</returns>
    override this.Interact (other:Robot) (dir:Direction) =
        let robotrow,robotcol = other.Position
        match dir with
            | South when robotrow = row && (min col (col+this.Length)) <= robotcol && robotcol <= (max col (col+this.Length)) -> Stop (robotrow,robotcol)
            | North when robotrow = row+1 && (min col (col+this.Length)) <= robotcol && robotcol <= (max col (col+this.Length)) -> Stop (robotrow,robotcol)
            | _ -> Ignore

///<summary>Backslashwall bounces the robot in a new direction.</summary>
///<param name="row">The row of the walls position</param>
///<param name="col">The column of the walls position</param>
///<param name="board">a board of type (int,int)</param>
and Backslashwall(row:int,col:int,board:int*int) =
    inherit BoardElement()
///<summary>The position of the element</summary>
    member this.Position with get() = (row,col)
///<summary>Shows the wall on the display.</summary>
///<param name="board">the board display</param>
    override this.RenderOn (board:BoardDisplay) =
        board.Set((fst this.Position),(snd this.Position)," \\") 
///<summary>Checks if the robot is about to hit the wall, and bounces the robot in a new direction if it is</summary>
///<param name="other">The robot which is checked</param>
///<param name="dir">The direction of the robot</param>
///<returns>Continue with a new direction and location (resulting from the bounce) if the robot hits the wall, Ignore otherwise</returns>
    override this.Interact (other:Robot) (dir:Direction) =
        let robotrow,robotcol = other.Position
        match dir with
            | North when (robotrow,robotcol) = (row,col) && row <> 1 -> Continue (West, (robotrow,robotcol - 1)) 
            | South when (robotrow,robotcol) = (row,col) && row <> snd board -> Continue (East, (robotrow,robotcol + 1))
            | East when (robotrow,robotcol) = (row,col) && col <> fst board -> Continue (South, (robotrow + 1,robotcol))
            | West when (robotrow,robotcol) = (row,col) && col <> 1 -> Continue (North, (robotrow - 1,robotcol))
            | _ -> Ignore

///<summary>A teleporter which teleports the robot if accessed</summary>
///<param name="row">The row of the teleporter position</param>
///<param name="col">The column of the teleporter position</param>
///<param name="board">a board of type (int,int)</param>
and Telepoter(row:int,col:int,tp:Position,board:int*int) =
    inherit BoardElement()
///<summary>The position of the element</summary>
    member this.Position with get() = (row,col)
///<summary>Shows the teleporter on the display.</summary>
///<param name="board">the board display</param>
    override this.RenderOn (board:BoardDisplay) =
        board.Set((fst this.Position),(snd this.Position),"TP") 
///<summary>Checks if the robot is about to hit the teleporter, moves the robot to the other teleporter if this is the case, where the robot then continues its trajectory</summary>
///<param name="other">The robot which is checked</param>
///<param name="dir">The direction of the robot</param>
///<returns>Continue with a new location (resulting from the teleportation) if the robot hits the teleporter, Ignore otherwise</returns>
    override this.Interact (other:Robot) (dir:Direction) =
        let mutable robotrow,robotcol = other.Position
        match dir with
            | North when (robotrow,robotcol) = (row,col) && fst tp <> 1 -> robotrow <- fst tp; robotcol <- snd tp; Continue (North, (robotrow - 1,robotcol))
            | South when (robotrow,robotcol) = (row,col) && fst tp <> snd board -> robotrow <- fst tp; robotcol <- snd tp; Continue (South, (robotrow + 1,robotcol))
            | East when (robotrow,robotcol) = (row,col) && snd tp <> fst board -> robotrow <- fst tp; robotcol <- snd tp; Continue (East, (robotrow,robotcol + 1)) 
            | West when (robotrow,robotcol) = (row,col) && snd tp <> 1 -> robotrow <- fst tp; robotcol <- snd tp; Continue (West, (robotrow,robotcol - 1)) 
            | _ -> Ignore

///<summary>The Board of the game, it holds all elements to make the game playable</summary>
///<param name="rows">The number of rows of a Board</param>
///<param name="cols">The number of columns of a Board</param>
type Board(rows:int,cols:int) = 
    let SetupBoard () = 
        let frame = BoardFrame(rows,cols)
        let goal = Goal(System.Random().Next(2,rows),System.Random().Next(1,cols))
        let hwall = HorizontalWall(System.Random().Next(1,rows),System.Random().Next(1,cols),System.Random().Next(-4,4))
        let vwall = VerticalWall(System.Random().Next(1,rows),System.Random().Next(1,cols),System.Random().Next(-4,4))
        let hwall2 = HorizontalWall(System.Random().Next(1,rows),System.Random().Next(1,cols),System.Random().Next(-4,4))
        let vwall2 = VerticalWall(System.Random().Next(1,rows),System.Random().Next(1,cols),System.Random().Next(-4,4))
        let backslash = Backslashwall(System.Random().Next(2,rows),System.Random().Next(1,cols),(rows,cols))
        
        let tp1 = (System.Random().Next(2,rows), System.Random().Next(1,cols))
        let tp2 = (System.Random().Next(2,rows), System.Random().Next(1,cols))
        let Telepoter1 = Telepoter(fst tp1,snd tp1, tp2, (rows,cols))
        let Telepoter2 = Telepoter(fst tp2,snd tp2, tp1, (rows,cols))

        let startelements : list<BoardElement> = [(frame :> BoardElement); (goal :> BoardElement); (hwall :> BoardElement); (vwall :> BoardElement); (hwall2 :> BoardElement); (vwall2 :> BoardElement); (backslash :> BoardElement); (Telepoter1 :> BoardElement); (Telepoter2 :> BoardElement)]
        startelements
    let mutable elements = SetupBoard()
    let mutable robots = []
    let display = BoardDisplay(rows,cols)
///<summary>The robots in the board</summary>
    member this.Robots with get() = robots and set(r) = robots <- robots @ r
///<summary>The board-elements</summary>
    member this.Elements with get() = elements and set(e) = elements <- elements @ e
///<summary>The boards display, an instance of BoardDisplay</summary>
    member this.Display with get() = display
///<summary>Adds a robot to the board by adding to the robots list and the elements properties</summary>
///<param name="robot">An instance of the robot which is to be added </param>
///<returns>Adds the robot to the robots and elements properties</returns>
    member this.AddRobot(robot:Robot) = 
        this.Robots <- [robot]
        this.Elements <- [robot]
///<summary>Adds an element to the elements property</summary>
///<param name="element">The element to be added, which is an instance of BoardElement</param>
    member this.AddElement(element:BoardElement) = this.Elements <- [element]
///<summary>Checks if any of the elements return true when GameOver is called. In our game, this only happens when the robot stops in the Goal field.</summary>
///<returns>True if the game is won, false otherwise</returns>
    member this.GameOver =
        let mutable res = false
        for el in this.Elements do
            if (el.GameOver robots = true) then res <- true
        res
///<summary> </summary>
///<param> </param>
///<returns> </returns>
    member this.Move(r:Robot,dir:Direction) = 
        let rec mover (r:Robot) (dir:Direction) (index:int) = 
            match ((this.Elements.[index]).Interact r dir) with 
            | Stop (pos) -> r.Position <- (pos)
            | Continue (d,pos) -> 
                r.Position <- pos
                mover r d 0
            | Ignore when index = (List.length this.Elements) - 1 -> //when all elements have been checked
                r.Step dir
                mover r dir 0
            | Ignore -> mover r dir (index+1)
        mover r dir 0
///<summary>Shows all the elements on the display by calling RenderOn for every element in the boards Elements property</summary>
///<returns>Prints the board display</returns>
    member this.RenderElements() =
        this.Display.ResetArray() //resets so previous positions aren't still shown
        for e in this.Elements do e.RenderOn (this.Display)
        this.Display.Show()

///<summary>The game class handles all user-interaction. Upon instantiation, it creates a board instance and adds robots to it, after which the game is ready to be played</summary>
///<param name="rows">The number of rows of a Board</param>
///<param name="cols">The number of columns of a Board</param>
///<param name="n">A string list contatining the names of the desired robots. All strings must have a length of exaclty two characters</param>
type Game(rows:int,cols:int,robotnames:string list) =
    let b = Board(rows,cols)
    let rec setup =
        let mutable counter = 1
        for name in robotnames do
            let name = Robot(1,counter,name) //amount of robots must be less than amount of columns, otherwise it will overflow
            counter <- counter + 1
            b.AddRobot name
    do setup
///<summary>Prints the robot names and indexes . the indexes printed are used to select a robot</summary>
///<returns>unit, and prints the robots on the screen</returns>
    member this.PrintRobots() =
        List.iteri (fun x _ -> printfn "%A: %A" (x+1) (b.Robots.[x].Name)) b.Robots 
///<summary>Prints the game-over message and exits the game</summary>
///<returns>Exits the program with exit code 0</returns>
    member this.PrintGameOver() =
        System.Console.Clear()
        do printfn "----------------------\nCongratulations, you won!\n----------------------"
        exit 0
///<summary>Plays the game, and handles all interaction with the game. Allows the user to select a robot and move it around inside a game-loop, and ends when the board method GameOver returns true</summary>
///<returns>When GameOver returns true, the PrintGameOver method is called, which exits the program with the exit code 0.</returns>
    member this.Play() =
        let mutable selectedrobot = b.Robots.[0] //selects robot 0 as default
        b.RenderElements()
        let rec chooserobot () =
            do this.PrintRobots()
            printfn "Select a robot number:"
            let robotnr = System.Console.ReadKey(true)
            try
                selectedrobot <- b.Robots.[((robotnr.KeyChar.ToString()) |> int)-1]
            with _ -> 
                do printfn "Please enter a valid robot number"
                chooserobot ()
        let rec moveloop () =
            let input = System.Console.ReadKey(true)
            match input with
                | key when key.Key = System.ConsoleKey.UpArrow -> b.Move(selectedrobot,North)
                | key when key.Key = System.ConsoleKey.DownArrow -> b.Move(selectedrobot,South)
                | key when key.Key = System.ConsoleKey.RightArrow -> b.Move(selectedrobot,East)
                | key when key.Key = System.ConsoleKey.LeftArrow -> b.Move(selectedrobot,West)
                | key when key.Key = System.ConsoleKey.Enter -> 
                    chooserobot ()
                    moveloop ()
                | _ -> ()
            System.Console.Clear()
            b.RenderElements()
            if b.GameOver then this.PrintGameOver()
            else moveloop ()
        printfn "%A" (this.PrintRobots())
        moveloop ()

//To start the game:
let g = Game(7,7,["AA";"BB";"CC";"DD"])
g.Play() 