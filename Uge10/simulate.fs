module SimulateDrones

///<summary> The function this.Fly sets the drone's new position after one second flight.</summary>
///<param name="f">A .</param>
///<returns> Drone's new position as int.</returns> 

///<summary> The function this.AtDestination returns TRUE if the destination is reached and FALSE otherwise.</summary>
///<param name="f">A .</param>
///<returns> Reached destination or not as a bool.</returns> 

type Drone (x:int, y:int, x1:int, y1:int, s:int) =
    //let mutable x = x
    //let mutable x1 = x1
    //let mutable y = y
    //let mutable y1 = y1
    let mutable position = (x, y)
    let mutable destination = (x1, y1)
    let mutable speed = s 

    member this.fly = 
        let direction = ((x1 - x), (y1 - y))
        let scala = ((s * (x1 - x)), (s * (y1 - y))) 
        



        //let directionNorm = ((s * (x1 - x)), (s * (y1 - y))) 
        let distance = sqrt (((x1 - x) ** 2) + ((y1 - y) ** 2))


        let distanceNorm = s * distance
        let newPosition = position + distanceNorm 
        newPosition
        
    member this.destination =
        if destination = position then
            position
        else
            destination


    member this.atDestination =    
        if position = this.destination then 
            true 
        else 
            false

let drone1 = Drone (10, 10, 50, 50, 5)

(* type Airspace (drons:drone) =
    let mutable drones = 
    member this.DroneDist =
    member this.FlyDrones =
    member this.AddDrone =
    member this.WillCollide = *)
