/// A 2 dimentional vector library.
/// Vectors are represented as pairs of floats.

module vec2d

///<summary>Given a vector expressed as a tuple, find the length of the vector.</summary>
///<param name="x">The x-coordinate of a vector.</param>
///<param name="y">The y-coordinate of a vector.</param>
///<returns> The length of the vector, the output is 1 float.</returns>
val len : x:float * y:float -> float

///<summary>Given a vector  expressed as a tuple, find the angle between a vector and a positive x axis.</summary>
///<param name="x">The x-coordiante of a vector.</param>
///<param name="y">The y-coordiante of a vector.</param>
///<returns> The angle of a vector.</returns>
val ang : x:float * y:float -> float

///<summary>Given two vectors, find the sum vector.</sammary>
///<param name="x1">the x-coordinate of the first vector.</param>
///<param name="y1">the y-coordinate of the first vector.</param>
///<param name="x2">the x-coordinate of the second vector.</param>
///<param name="y2">the y-coordinate of the second vector.</param>
///<returns> The sum vector.</returns>
val add : x1:float * y1:float -> x2:float * y2:float -> float * float
