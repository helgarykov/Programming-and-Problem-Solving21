namespace HRI.Drones

open System
open Xunit

open Primitive
open Simulation

module AirspaceTests =

    [<Fact>]
    let ``DroneTuple Equality`` () =
        let a = Drone(Vector(0.0, 0.0), Vector(100.0, 0.0), 1.0)
        let b = Drone(Vector(20.0, 0.0), Vector(100.0, 0.0), 1.0)
        let c = Drone(Vector(22.0, -1.0), Vector(25.0, 50.0), 1.0)
        let d = Drone(Vector(-200.0, 15.0), Vector(-75.0, 100.0), 1.0)
        let e = Drone(Vector(-202.0, 17.0), Vector(0.0, 0.0), 1.0)

        let t1 = (a, b)
        let t2 = (c, d)
        let t3 = (a, b)
        let t4 = (b, a)

        Assert.Equal(t1, t1)
        Assert.Equal(t1, t3)
        Assert.NotEqual(t1, t2)
        Assert.NotEqual(t3, t4)

    [<Fact>]
    let ``Can detect immediate collisions in airspace`` () =
        let a = Drone(Vector(0.0, 0.0), Vector(100.0, 0.0), 1.0)
        let b = Drone(Vector(20.0, 0.0), Vector(100.0, 0.0), 1.0)
        let c = Drone(Vector(22.0, -1.0), Vector(25.0, 50.0), 1.0)
        let d = Drone(Vector(-200.0, 15.0), Vector(-75.0, 100.0), 1.0)
        let e = Drone(Vector(-202.0, 17.0), Vector(0.0, 0.0), 1.0)

        let l = [a; b; c; d; e]

        let air = Airspace(l)

        let collisions = air.PairsInCollisionRange()

        Assert.Equal(2, collisions.Length)

    [<Fact>]
    let ``Can detect collisions in airspace over time`` () =
        let a = Drone(Vector(0.0, 0.0), Vector(100.0, 0.0), 1.0)
        let b = Drone(Vector(100.0, 1.0), Vector(0.0, 1.0), 1.0)
        let c = Drone(Vector(0.0, -200.0), Vector(100.0, -200.0), 1.0)
        let d = Drone(Vector(-200.0, -800.0), Vector(400.0, -800.0), 1.0)
        let e = Drone(Vector(50.0, -250.0), Vector(50.0, -150.0), 1.0)

        let l = [a; b; c; d; e]

        let air = Airspace(l)

        let collisions = air.WillCollide 10

        Assert.Equal(2, collisions.Length)

        Assert.Contains((a, b), collisions)
        Assert.Contains((c, e), collisions)
