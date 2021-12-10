module SimulateDrones

///<summary> The function this.Fly sets the drone's new position after one second flight.</summary>
///<param name=""> .</param>
///<returns> Drone's new position as int.</returns> 

///<summary> The function this.AtDestination returns TRUE if the destination is reached and FALSE otherwise.</summary>
///<param name=""> .</param>
///<returns> Reached destination or not as a bool.</returns> 


let divideVector (x: float, y: float) (e: float) : float * float =
    let xA = x / e
    let yA = y / e
    (xA, yA)
       
        
let multiplyVector (s: float) (x: float, y: float) : float * float =
    let xB = s * x
    let yB = s * y
    (xB, yB)



type Drone (x:int, y:int, x1:int, y1:int, s:int) =
    let mutable position = (x, y)
    let mutable aDestination = (x1, y1)
    let mutable speed = float (s) 

    member this.fly = 
        let directionVector = ((float (x1 - x)), (float (y1 - y)))                     //finds the vector <Position-Destination>
        let lengthVector = float (sqrt ((float (x1 - x) ** 2.0) + (float (y1 - y) ** 2.0)))  //finds the length of the vector <Position-Destination> 
        let unitVector = divideVector directionVector lengthVector // finds the unit vector
        let newPosition = multiplyVector speed unitVector 
        newPosition  // find the new position of a drone after one second flight                                 
        
    
    member this.destination =
        if aDestination = position then
            position
        else
            aDestination

    member this.atDestination =    
        if position = this.destination then 
            true 
        else 
            false
        

(* let a = Drone (10, 10, 50, 50, 5)
let b = Drone (350, 100, 350, 300, 2)
let c = Drone (100, 350, 300, 330, 10)
let d = Drone (200, 250, 390, 390, 6)
let e = Drone (90, 300, 150, 10, 2) *)



type Airspace (drones: Drone) =
    let mutable drones = Drone list 
    member this.DroneDist =
    member this.FlyDrones =
    member this.AddDrone =
    member this.WillCollide = 
