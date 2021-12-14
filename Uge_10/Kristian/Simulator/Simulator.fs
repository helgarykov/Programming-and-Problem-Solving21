namespace HRI.Drones

module Primitive =

    type Vector(x: float, y: float) =
        member this.X = x
        member this.Y = y

        member this.Length =
            sqrt (this.X ** 2.0 + this.Y ** 2.0)

        member this.Normalized() =
            this / this.Length

        override this.GetHashCode() =
            hash (this.X, this.Y)

        override this.Equals(b) =
            match b with
            | :? Vector as v -> this.X = v.X && this.Y = v.Y
            | _ -> false

        static member (+) (l: Vector, r: Vector): Vector =
            Vector(l.X + r.X, l.Y + r.Y)

        static member (-) (l: Vector, r: Vector): Vector =
            Vector(l.X - r.X, l.Y - r.Y)

        static member (*) (l: Vector, m: float): Vector =
            Vector(l.X * m, l.Y * m)

        static member (/) (l: Vector, r: float): Vector =
            Vector(l.X / r, l.Y / r)

module Simulation =

    type Drone(position: Primitive.Vector, destination: Primitive.Vector, speed: float) =
        let mutable position = position
        let mutable destination = destination
        let mutable speed = speed
        
        member this.Position = position
        member this.Destination = destination
        member this.Speed = speed
         
        member this.Fly() =
            let toTarget = destination - position

            position <- 
                match toTarget.Length with
                | d when d <= speed -> destination
                | _ -> position + (toTarget.Normalized() * speed)

        member this.AtDestination() =
            position = destination

        override this.ToString() =
            sprintf "X = %f, Y = %f, Speed = %f" position.X position.Y speed

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

    
    let addifmissing (l: 'a list) (i: 'a) =
        match List.contains i l with
        | false -> i :: l
        | true -> l

    let rec addallmissing (l: 'a list)  (n: 'a list) =
        match n with
        | [] -> l
        | head :: tail -> addallmissing (addifmissing l head) tail

    type Airspace(d: Drone list) = 
        let mutable drones: Drone list = d

        member this.DroneDist (a: Drone) (b: Drone) =
            (a.Position - b.Position).Length

        member this.PairsInCollisionRange() = 
            drones |> pairs |> List.filter (fun (x, y) -> this.DroneDist x y < 5.0)

        member this.AddDrone(d) =
            drones <- d :: drones

        member this.FlyDrones() =
            drones |> List.iter (fun x -> x.Fly())

        member this.WillCollide (mins: int) =
            let increments = mins * 60

            let rec tick (i: int) (acc: (Drone * Drone) list) =
                let colliding = this.PairsInCollisionRange()
                this.FlyDrones()
                
                match i with
                | x when x > 0 -> tick (i-1) (addallmissing acc colliding)
                | _ -> acc

            tick increments []

            