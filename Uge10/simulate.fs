module SimulateDrones

///<summary> The function this.Fly sets the drone's new position after one second flight.</summary>
///<param name="directionVector">Finds the vector Position-Destination and transforms it into a float .</param>
///<param name="lengthVector"> Finds the the length of the vector Position-Destination and transforms it into a float .</param>
///<param name="unitVector"> Finds the unit vector .</param>
///<param name="newPosition"> Finds the new position of a drone after one second flight.</param>
///<returns> Drone's new position as float.</returns> 

///<summary> The function this.destination returns the present position of a drone in (x,y) coordinates. If the drone is not flying, its present position and its destiantion are the same.</summary>

///<summary> The function this.AtDestination returns TRUE if the destination is reached and FALSE otherwise.</summary>

///<summary> The function divideVector takes a set of coordinates x and y and a float and returns a new set of coordinates x and y.</summary>
let divide (x: float, y: float) (e: float) : float * float =
        let xA = x / e
        let yA = y / e
        (xA, yA)
       
 ///<summary> The function multiply takes a set of coordinates x and y and a float s and returns a new set of coordinates x and y.</summary>       
let multiply (s: float) (x: float, y: float) : float * float =
        let xB = s * x
        let yB = s * y
        (xB, yB)

type Drone (x:int, y:int, x1:int, y1:int, s:int) =
    let mutable X = x
    let mutable Y = y
    let mutable X1 = x1
    let mutable Y1 = y1
    let mutable position = (x, y)
    let mutable aDestination = (x1, y1)
    let mutable speed = float (s) 

    (*member this.divide (x: float, y: float) (e: float) : float * float =
        let xA = x / e
        let yA = y / e
        (xA, yA)
    
    member this.multiply (s: float) (x: float, y: float) : float * float =
        let xB = s * x
        let yB = s * y
        (xB, yB)*)
    
    member this.fly = 
        let directionVector = ((float (x1 - x)), (float (y1 - y)))                     
        let lengthVector = float (sqrt ((float (x1 - x) ** 2.0) + (float (y1 - y) ** 2.0)))  
        let unitVector = divide directionVector lengthVector 
        let newPosition = multiply speed unitVector 
        newPosition                                
        
    
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



(* type Airspace (drones: Drone) =
    let mutable drones = []
    
    member this.DroneDist = 
        let drone1 = frs.destination
        let drone2 = sec.destination

        



    member this.FlyDrones = 
    member this.AddDrone =
    member this.WillCollide = *)
