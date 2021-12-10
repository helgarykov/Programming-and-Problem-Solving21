module Simulate

///<summary> The function this.Fly sets the drone's new position after one second flight.</summary>
///<param name="f">A .</param>
///<returns> Drone's new position as int.</returns> 

///<summary> The function this.AtDestination returns TRUE if the destination is reached and FALSE otherwise.</summary>
///<param name="f">A .</param>
///<returns> Reached destination or not as a bool.</returns> 

type Drone (x:int,y:int, x_1:int, y_1:int, s:int) =
    let mutable position = (x, y)
    let mutable destination = (x_1, y_1)
    let mutable speed = s
    member this.Fly =
    member this.AtDestination = 



type Airspace (drons:drone) =
    let mutable drones = 
    member this.DroneDist =
    member this.FlyDrones =
    member this.AddDrone =
    member this.WillCollide =
