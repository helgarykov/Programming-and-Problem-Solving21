namespace HRI.Drones

open System
open Xunit

open Primitive
open Simulation

module VectorTests =

    [<Fact>]
    let ``Can construct a vector`` () =
        let v = Vector(2.0, 4.0)
        
        Assert.Equal(2.0, v.X)
        Assert.Equal(4.0, v.Y)

    [<Fact>]
    let ``Vector has correct length`` () =
        let v = Vector(2.0, 2.0)
        
        Assert.Equal(2.828427, v.Length, 4)

    [<Fact>]
    let ``Negative vectors also give correct length`` () =
        let v = Vector(-2.0, -2.0)

        Assert.Equal(2.828427, v.Length, 4)

    [<Fact>]
    let ``Can compare two vectors`` () =
        let a = Vector(2.0, 2.0)
        let b = Vector(2.0, 2.0)
        let c = Vector(3.0, 2.0)
        
        Assert.True((a = b))
        Assert.False((b = c))

    [<Fact>]
    let ``Can add two vectors`` () =
        let a = Vector(3.0, 5.0)
        let b = Vector(2.0, 4.0)
              
        Assert.Equal(Vector(5.0, 9.0), a + b)
        Assert.NotEqual(Vector(3.0, 5.0), a + b)
        Assert.Equal(a + b, b + a)