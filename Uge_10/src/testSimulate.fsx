namespace HRI.Drones

open System
open Primitive
open Simulation

module VectorTests =

    printfn "Black-box Test for Vector"

    let a = Vector(2.0, 2.0)
    let roundedLength = round 3 a.Length
    let b = Vector(-2.0, -2.0)
    let roundedLengt = round 3 b.Length
    let c = Vector(2.0, 2.0)

    printfn " %5b: Can construct a vector with x-coordinate" (a.X = 2.0)
    printfn " %5b: Can construct a vector with y-coordinate" (a.Y = 2.0)
    printfn " %5b: Vactor has correct length" (roundedLength = 2.828)
    printfn " %5b: Negative vectors also give correct length" (roundedLengt = 2.828)
    printfn " %5b: Can compare two vectors" (a.Equals c = true)
    printfn " %5b: Can compare two vectors" (a.Equals b = false)
    printfn " %5b: Can add two vectors" (a + c = Vector(4.0, 4.0))
    printfn " %5b: Can add two vectors" (a + c = c + a)

module DroneTests =

    printfn "Black-box Test for Drone"

    let initP = Vector(1.0, 2.0)
    let d = Drone(initP, Vector(4.0, 4.0), 1.0)
    let roundPosition = round 3 d.Position.X

    let e = Drone(Vector(0.0, 0.0), Vector(100.0, 0.0), 1.0)
    let f = Drone(Vector(20.0, 0.0), Vector(100.0, 0.0), 1.0)
    let g = Drone(Vector(-50.0, -50.0), Vector(25.0, 50.0), 1.0)
    let i = Drone(Vector(-200.0, 15.0), Vector(-75.0, 100.0), 1.0)
    let h = Drone(Vector(350.0, 855.0), Vector(0.0, 0.0), 1.0)

    let l = [e; f; g; i; h]
    let p = pairs l

    let q = [1; 4; 7; 9]

    printfn " %5b: Can construct a drone with position" (d.Position = Vector(1.0, 2.0))
    printfn " %5b: Can construct a drone with destination" (d.Destination = Vector(4.0, 4.0))
    printfn " %5b: Can construct a drone with speed" (d.Speed = 1.0)
    printfn " %5b: A drone changes position (X) when flying" (roundPosition = 1.832)

    printfn " %5b: We can construct a list of all pairs of drones" (p.Length = 10)

    //printfn "%f: position after flying x" (d.Position.X)


    


   
