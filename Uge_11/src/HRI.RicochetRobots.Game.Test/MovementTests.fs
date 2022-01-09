namespace HRI.RicochetRobots.Game.Test

module MovementTests =

    open Xunit
    open HRI.RicochetRobots.Game.Movement
    
    [<Fact>]
    let ``Can extract single coordinates`` () =
        let p = Position (3, 2)
        Assert.Equal(3, p.Row)
        Assert.Equal(2, p.Column)
        Assert.Equal((3, 2), p.Value)

    [<Fact>]
    let ``Can determine opposite directions`` () =
        Assert.Equal(West, opposite East)
        Assert.Equal(East, opposite West)
        Assert.Equal(North, opposite South)
        Assert.Equal(South, opposite North)

    [<Fact>]
    let ``Can move single steps`` () =
        let start = Position(2, 2)
        Assert.Equal(Position (3, 2), move start South)
        Assert.Equal(Position (1, 2), move start North)
        Assert.Equal(Position (2, 3), move start East)
        Assert.Equal(Position (2, 1), move start West)

        // Member variant
        Assert.Equal(Position (3, 2), start.WithMove South)


    [<Fact>]
    let ``Can move multiple steps`` () =
        let start = Position(2, 2)
        Assert.Equal(Position (2, 5), moveSteps start East 3)

        // Member variant
        Assert.Equal(Position (2, 5), start.WithMoveSteps East 3)
