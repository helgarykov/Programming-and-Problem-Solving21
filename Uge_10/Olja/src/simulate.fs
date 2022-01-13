namespace HRI.Drones

///A two-dimentional vector-as-a-primitive-type library.
///Includes its properties and additional methods of operator overloading.
module Primitive =

    type Vector(x: float, y: float) =
        member this.X = x
        member this.Y = y


///<summary> Defines current length of a vector as a float.</summary>
        member this.Length =
            sqrt (this.X ** 2.0 + this.Y ** 2.0)


///<summary> Given a vector and its length, returns a unit vector.</summary>
        member this.Normalized() =
            this / this.Length

  
///<summary> Given two vectors a and b, checks whether they are equal.</summary>
///<param name="b"> A vector to the right of the function Equals, which is matched with a vector to the left.</param>
///<returns> TRUE if a = b, otherwise FALSE.</retyrns>
        override this.Equals(b) =
            match b with
            | :? Vector as v -> this.X = v.X && this.Y = v.Y
            | _ -> false


///<summary>Get the sum of two vectors.</summary>
///<param name="l">A vector to the left of the binary operator(+).</param>
///<param name="r">A vector to the right of the binary operator(+).</param>
///<returns>A new vector as a float.</returns>
        static member (+) (l: Vector, r: Vector): Vector =
            Vector(l.X + r.X, l.Y + r.Y)


///<summary>Get the difference between two vectors.</summary>
///<param name="l">A vector to the left of the binary operator(-).</param>
///<param name="r">A vector to the right of the binary operator(-).</param>
///<returns>A new vector as a float.</returns>
        static member (-) (l: Vector, r: Vector): Vector =
            Vector(l.X - r.X, l.Y - r.Y)


///<summary>Get the product of a vector and a float number.</summary>
///<param name="l">A vector to the left of the binary operator(*).</param>
///<param name="r">A constant to the right of the binary operator(*).</param>
///<returns>A new vector as a float.</returns>
        static member (*) (l: Vector, r: float): Vector =
            Vector(l.X * r, l.Y * r)


///<summary>Get the quotient of a vector and a float number.</summary>
///<param name="l">A vector to the left of the binary operator(/).</param>
///<param name="r">A constant to the right of the binary operator(/).</param>
///<returns>A new vector as a float.</returns>
        static member (/) (l: Vector, r: float): Vector =
            Vector(l.X / r, l.Y / r)


///<summary>Round a long float off.</summary>    
    let round (decimals:int) (x:float) =
        System.Math.Round(x, decimals)


/// A 2 dimentional drone library.
/// Drones are defined as objects represented by two vectors (position and destination) and speed.
module Simulation =

    type Drone (position: Primitive.Vector, destination: Primitive.Vector, speed: float) =

///<summary> The position of a drone as a vector before one flight.</summary>
        let mutable position = position

///<summary> The position of a drone as a vector after one flight.</summary>
        let mutable destination = destination

///<summary>The constant speed of a drone.</summary>
        let mutable speed = speed

///<summary>Current position of the drone in the airspace</summary>       
        member this.Position = position

///<summary>The destination point of the drone</summary>
        member this.Destination = destination

///<summary>The speed of a particular drone.</summary>
        member this.Speed = speed
         
 
///<summary>Move the drone toward the destination by one second.</summary>
///<returns>Position of the drone after one second flight.</returns> 
        member this.Fly() =
            let toTarget = destination - position

            position <- 
                match toTarget.Length with
                | d when d <= speed -> destination
                | _ -> position + (toTarget.Normalized() * speed)


///<summary>Given the drone's current position and destination, return TRUE if the destination is reached and FALSE otherwise.</summary>
        member this.AtDestination() =
            position = destination

       
///<summary>Given a list of elements 'a, combine them into pairs and return a list of all permutation pairs ('a * 'a).</summary>
///<param name="l"> A list of 'a.</param>
///<returns> A list of tuples (a'*'a).</returns> 
    let pairs (l: 'a list) =
        let rec p (i: 'a) (l: 'a list) (acc: ('a * 'a) list) =
            match l with
            | [] -> acc
            | head :: tail -> p i tail ((i, head) :: acc)

        let rec r (l: 'a list) (acc: ('a * 'a) list) =
            match l with
            | [] -> acc
            | head :: tail -> r tail (p head tail acc)

        let e: ('a * 'a) list = []
        r l e


///<summary>Given a list of elements 'a and an element of the type 'a, add this element at the head of the list when not on the list, otherwise do nothing.</summary> 
///<param name="l"> A list of 'a.</param> 
///<param name="i"> A single element of 'a.</param>
///<returns> If i is not already on the list l, returns a new list with the added i, if i is on the list, returns the input list.</returns>  
    let addifmissing (l: 'a list) (i: 'a) =
        match List.contains i l with
        | false -> i :: l
        | true -> l


///<summary>Given two lists of elements 'a, return a list of 'a with no repetitions of 'a.</summary> 
///<param name="l"> A list of 'a which starts to be empty and to which the elements from the list n are being added one by one, if they're not alredy present in l.</param> 
///<param name="n"> A list of elements 'a which is to be sorted for dublicates.</param>
///<returns> The list l, which is a sorted for dublicates list n.</returns> 
    let rec addallmissing (l: 'a list)  (n: 'a list) =
        match n with
        | [] -> l
        | head :: tail -> addallmissing (addifmissing l head) tail


    

    type Airspace(d: Drone list) = 

///<summary>A list of drones.</summary>
        let mutable drones: Drone list = d

///<summary>Get the distance between two drones.</summary>
///<param name="a">The first of the two drones .</param> 
///<param name="a">The second of the two drones .</param>
///<returns> The distance between the drones as float.</returns> 
        member this.DroneDist (a: Drone) (b: Drone) =
            (a.Position - b.Position).Length


///<summary>Given a list of drones, returns a list of the pairs of drones that are in collision because the distance between which is less than 5 meters.</summary>
        member this.PairsInCollisionRange() = 
            drones |> pairs |> List.filter (fun (x, y) -> this.DroneDist x y < 5.0)


///<summary> Given a list of drones d, adds a new drone to the list.</summary>
        member this.AddDrone(d) =
            drones <- d :: drones


///<summary>Given a list of drones, make them perform a one second flight.</summary>
        member this.FlyDrones() =
            drones |> List.iter (fun x -> x.Fly())


///<summary>Get a list of drones that will eventually collide within mins minutes.</summary>
///<param name="mins"> Minutes as an int.</param>
///<returns>A list of pairs of drones.</returns>
        member this.WillCollide (mins: int) =
            let increments = mins * 60

            let rec tick (i: int) (acc: (Drone * Drone) list) =
                let colliding = this.PairsInCollisionRange()
                this.FlyDrones()
                
                match i with
                | x when x > 0 -> tick (i-1) (addallmissing acc colliding)
                | _ -> acc

            tick increments []

            