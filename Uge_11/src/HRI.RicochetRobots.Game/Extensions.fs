namespace HRI.RicochetRobots.Game

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