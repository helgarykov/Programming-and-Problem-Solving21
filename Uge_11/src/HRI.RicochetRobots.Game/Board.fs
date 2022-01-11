namespace HRI.RicochetRobots.Game 

module Board =
    
    open HRI.RicochetRobots.Game.Movement
    open HRI.RicochetRobots.Game.BoardElements
    
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
                let p = robot.Position.WithMove direction
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