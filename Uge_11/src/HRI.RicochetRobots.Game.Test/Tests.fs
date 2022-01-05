module Tests

open System
open Xunit

[<Fact>]
let ``My test`` () =
    Assert.True(false)
   
[<Fact>]
let ``Division of ints should return an int`` () =
    Assert.Equal(3, 17 / 5)
    Assert.Equal(3, 19 / 5)
