module Simulate

///<summary> The function this.Fly sets the drone's new position after one second flight.</summary>
///<param name="f">A .</param>
///<returns> Drone's new position as int.</returns> 

///<summary> The function this.AtDestination returns TRUE if the destination is reached and FALSE otherwise.</summary>
///<param name="f">A .</param>
///<returns> Reached destination or not as a bool.</returns> 

type Drone (x:int,y:int, x1:int, y1:int, s:int) =
    let mutable x = x
    let mutable x1 = x1
    let mutable y = y
    let mutable y1 = y1
    let mutable position = (x, y)
    let mutable speed = s 
    member this.Destination = (x1, y1)
    //member this.Fly = x <- (sin (x - x1 +s)) ; y <- (sin (y - y1, s)) // FORKERT


    member this.Fly = 
        if FrameCount % 60 = 0 then
            position <- position + 2 * speed

    member this.AtDestination = 
        //match this.AtDestination with
        //| position -> true
        //|      
    if position = this.Destination then true else false



type Airspace (drons:drone) =
    let mutable drones = 
    member this.DroneDist =
    member this.FlyDrones =
    member this.AddDrone =
    member this.WillCollide =
