namespace HRI.Drones

open System
open Xunit

open Primitive
open Simulation

module DroneTests =

    [<Fact>]
    let ``Can construct a drone`` () =
        let d = Drone(Vector(1.0, 2.0), Vector(4.0, 4.0), 1.0)

        Assert.Equal(Vector(1.0, 2.0), d.Position)
        Assert.Equal(Vector(4.0, 4.0), d.Destination)
        Assert.Equal(1.0, d.Speed)

    [<Fact>]
    let ``A drone can fly`` () = 
        let initP = Vector(1.0, 2.0)
        let d = Drone(initP, Vector(4.0, 4.0), 1.0)

        d.Fly()

        Assert.NotEqual(d.Position, initP)
        Assert.Equal(1.8321, d.Position.X, 4)
        Assert.Equal(2.5547, d.Position.Y, 4)

        Assert.False(d.AtDestination())

    [<Fact>]
    let ``A drone will stop at its destination`` () = 
        let initP = Vector(1.0, 2.0)
        let d = Drone(initP, Vector(4.0, 4.0), 1.0)

        d.Fly()
        d.Fly()
        d.Fly()
        d.Fly()

        Assert.NotEqual(d.Position, initP)
        Assert.Equal(4.0, d.Position.X, 4)
        Assert.Equal(4.0, d.Position.Y, 4)

        Assert.True(d.AtDestination())

    [<Fact>]
    let ``We can construct a list of all pairs of drones`` () =
        let a = Drone(Vector(0.0, 0.0), Vector(100.0, 0.0), 1.0)
        let b = Drone(Vector(20.0, 0.0), Vector(100.0, 0.0), 1.0)
        let c = Drone(Vector(-50.0, -50.0), Vector(25.0, 50.0), 1.0)
        let d = Drone(Vector(-200.0, 15.0), Vector(-75.0, 100.0), 1.0)
        let e = Drone(Vector(350.0, 855.0), Vector(0.0, 0.0), 1.0)

        let l = [a; b; c; d; e]

        let p = pairs l

        Assert.Equal(10, p.Length)