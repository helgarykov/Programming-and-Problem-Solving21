namespace HRI.RicochetRobots.Game.Test

module BoardDisplayTests =
    
    open Xunit
    open HRI.RicochetRobots.Game.BoardDisplay
    
    [<Fact>]
    let ``Can translate coords to index`` () =
        Assert.Equal(13, coordsToIndex 5 2 3)
        Assert.Equal(0, coordsToIndex 5 0 0)
        Assert.Equal(15, coordsToIndex 5 3 0)
    
    [<Fact>]
    let ``Can locate left borders`` () =
        let cols = 5
        Assert.True(isleftcol cols (coordsToIndex cols 0 0))
        Assert.True(isleftcol cols (coordsToIndex cols 2 0))
        Assert.False(isleftcol cols (coordsToIndex cols 0 3))
        Assert.False(isleftcol cols (coordsToIndex cols 1 1))
    
    [<Fact>]
    let ``Can locate top borders`` () =
        let cols = 5
        Assert.True(istoprow cols (coordsToIndex cols 0 0))
        Assert.False(istoprow cols (coordsToIndex cols 2 0))
        Assert.True(istoprow cols (coordsToIndex cols 0 3))
        Assert.False(istoprow cols (coordsToIndex cols 1 1))
     
    [<Fact>]
    let ``Can locate right borders`` () =
        let cols = 5
        Assert.True(istoprow cols (coordsToIndex cols 0 0))
        Assert.False(istoprow cols (coordsToIndex cols 2 0))
        Assert.True(istoprow cols (coordsToIndex cols 0 3))
        Assert.False(istoprow cols (coordsToIndex cols 1 1))
        