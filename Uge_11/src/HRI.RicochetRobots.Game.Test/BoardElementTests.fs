namespace HRI.RicochetRobots.Game.Test

    module BoardElementTests =

        open Xunit
        open HRI.RicochetRobots.Game.Movement
        open HRI.RicochetRobots.Game.BoardElements

        [<Fact>]
        let ``robotsOf can filter a list of BoardElements for robots`` () =
            let g = Goal (3, 3)
            let r1 = Robot (4, 3, "aa")
            let r2 = Robot (2, 2, "bb")
            let r3 = Robot (3, 3, "cc")
            let w = VerticalWall(3, 3, 1)

            let elements : BoardElement list = [g; r1; r2; r3; w]

            let filtered = robotsOf elements

            Assert.Equal(3, filtered.Length)

        [<Fact>]
        let ``A Goal returns GameOver if, and only if occupied by robot`` () =
            let g = Goal (3, 3)
            let r1 = Robot (4, 3, "aa")
            let r2 = Robot (2, 2, "bb")

            let mutable elements : BoardElement list = [g; r1; r2]

            Assert.False(g.GameOver elements)

            // A wall occupies same space ...
            let w = VerticalWall(3, 3, 1)
            elements <- w :: elements

            // ... but this does not count, since it is not a robot.
            Assert.False(g.GameOver elements)

            let r3 = Robot (3, 3, "cc")
            elements <- r3 :: elements

            Assert.True(g.GameOver elements)
           
        [<Fact>]
        let ``A vertical wall returns Ignore for a robot outside its sphere`` () =
            let w = VerticalWall (2, 2, 5)
            let r = Robot (2, 4, "aa")

            Assert.Equal(Ignore, w.Interact r West)

        [<Fact>]
        let ``A vertical wall returns Stop for a robot outside trying to pass in`` () =
            let w = VerticalWall (2, 2, 5)
            let r = Robot (2, 3, "aa")

            Assert.Equal(Stop (Position (2, 3)), w.Interact r West)

        [<Fact>]
        let ``A vertical wall ignores a robot outside, if it goes along the wall or away from it`` () =
            let w = VerticalWall (2, 2, 5)
            let r = Robot (2, 3, "aa")

            Assert.Equal(Ignore, w.Interact r North)
            Assert.Equal(Ignore, w.Interact r East)
            Assert.Equal(Ignore, w.Interact r South)

        [<Fact>]
        let ``A vertical wall correctly returns all positions it stretches along`` () =
            let w = VerticalWall (2, 2, 5)
            let positions = w.AllPositions()

            Assert.Equal(5, positions.Length)
            
            Assert.DoesNotContain(Position (1, 2), positions)
            Assert.Contains(Position (2, 2), positions)
            Assert.Contains(Position (3, 2), positions)
            Assert.Contains(Position (4, 2), positions)
            Assert.Contains(Position (5, 2), positions)
            Assert.Contains(Position (6, 2), positions)
            Assert.DoesNotContain(Position (7, 2), positions)

        [<Fact>]
        let ``A vertical wall returns Stop for a robot outside trying to pass in for the full length of it`` () =
            let w = VerticalWall (2, 2, 5)
            let r = Robot (6, 3, "aa")

            Assert.Equal(Stop (Position (6, 3)), w.Interact r West)

        [<Fact>]
        let ``A vertical wall returns Stop for a robot inside trying to go out`` () =
            let w = VerticalWall (2, 2, 5)
            let r = Robot (2, 2, "aa")

            Assert.Equal(Stop (Position (2, 2)), w.Interact r East)

            let rb = Robot (4, 2, "aa")

            Assert.Equal(Stop (Position (4, 2)), w.Interact rb East)

        [<Fact>]
        let ``A horizontal wall works like a vertical wall but north<->south`` () =
            let w = HorizontalWall (2, 2, 5)

            Assert.Equal(Stop (Position (2, 2)), w.Interact (Robot (2, 2, "aa")) South)
            Assert.Equal(Ignore, w.Interact (Robot (2, 2, "aa")) West)
            Assert.Equal(Ignore, w.Interact (Robot (2, 2, "aa")) North)
            Assert.Equal(Ignore, w.Interact (Robot (2, 2, "aa")) East)

            Assert.Equal(Stop (Position (2, 6)), w.Interact (Robot (2, 6, "bb")) South)
            Assert.Equal(Ignore, w.Interact (Robot (2, 6, "bb")) West)
            Assert.Equal(Ignore, w.Interact (Robot (2, 6, "bb")) North)
            Assert.Equal(Ignore, w.Interact (Robot (2, 6, "bb")) East)

            Assert.Equal(Stop (Position (3, 2)), w.Interact (Robot (3, 2, "cc")) North)
            Assert.Equal(Ignore, w.Interact (Robot (3, 2, "cc")) West)
            Assert.Equal(Ignore, w.Interact (Robot (3, 2, "cc")) South)
            Assert.Equal(Ignore, w.Interact (Robot (3, 2, "cc")) East)

            Assert.Equal(Stop (Position (3, 6)), w.Interact (Robot (3, 6, "dd")) North)
            Assert.Equal(Ignore, w.Interact (Robot (3, 6, "dd")) West)
            Assert.Equal(Ignore, w.Interact (Robot (3, 6, "dd")) South)
            Assert.Equal(Ignore, w.Interact (Robot (3, 6, "dd")) East)

        [<Fact>]
        let ``A robot will stop another robot trying to pass through it`` () =
            let r1 = Robot (2, 2, "aa")
            let r2 = Robot (2, 3, "bb")
            let r3 = Robot (5, 5, "cc")
            let r4 = Robot (4, 5, "dd")

            Assert.Equal(Stop (Position (2, 3)), r1.Interact r2 West)
            Assert.Equal(Ignore, r1.Interact r2 East)
            Assert.Equal(Ignore, r1.Interact r2 North)
            Assert.Equal(Ignore, r1.Interact r3 West)
            Assert.Equal(Stop (Position (4, 5)), r3.Interact r4 South)

        [<Fact>]
        let ``A BoardFrame will stop a robot trying to move outside bounds`` () =
            let frame = BoardFrame (5, 4)

            Assert.Equal(Ignore, frame.Interact (Robot (2, 2, "aa")) North)
            Assert.Equal(Ignore, frame.Interact (Robot (2, 2, "aa")) East)
            Assert.Equal(Ignore, frame.Interact (Robot (2, 2, "aa")) South)
            Assert.Equal(Ignore, frame.Interact (Robot (2, 2, "aa")) West)
            
            Assert.Equal(Stop (Position (0, 2)), frame.Interact (Robot (0, 2, "bb")) North)
            Assert.Equal(Ignore, frame.Interact (Robot (0, 2, "bb")) East)
            
            Assert.Equal(Stop (Position (4, 2)), frame.Interact (Robot (4, 2, "cc")) South)
            
            Assert.Equal(Stop (Position (2, 3)), frame.Interact (Robot (2, 3, "dd")) East)
            
            Assert.Equal(Stop (Position (-2, -3)), frame.Interact (Robot (-2, -3, "ee")) East)
            Assert.Equal(Stop (Position (-2, -3)), frame.Interact (Robot (-2, -3, "ee")) South)
            Assert.Equal(Stop (Position (-2, -3)), frame.Interact (Robot (-2, -3, "ee")) West)
            Assert.Equal(Stop (Position (-2, -3)), frame.Interact (Robot (-2, -3, "ee")) North)

            Assert.Equal(Stop (Position (8, 3)), frame.Interact (Robot (8, 3, "ff")) East)
            Assert.Equal(Stop (Position (8, 3)), frame.Interact (Robot (8, 3, "ff")) South)
            Assert.Equal(Stop (Position (8, 3)), frame.Interact (Robot (8, 3, "ff")) West)
            Assert.Equal(Stop (Position (8, 3)), frame.Interact (Robot (8, 3, "ff")) North)


        [<Fact>]
        let ``Two different robots are not structurally equal`` () =
            let r1 = Robot (1, 1, "aa")
            let r2 = Robot (1, 1, "aa")

            Assert.True((not (r1 = r2)))
            Assert.True(r1 <> r2)

            