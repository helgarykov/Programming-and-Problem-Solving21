
/// A 3 dimentional color library.
/// Colors are represented as tuples of integers.

module color

/// Takes 1 integer and returns 1 integer. Ensures that the values
///are within the color spectrum from 0 to 255.
val trunc : int -> int

///Takes two tuples of 3 integers and returns 1 tuple of 3 integers. It combines two colors.
///Uses 6 input, (r1, g1, b1, r2,g2, b2).
val add :r1: int * g1: int * b1: int * r2: int * g2:int * b2:int -> int * int * int 

///Takes a constant and multiplies it with each of the color-channels.
///Uses the input (r,g,b,c), where c is the constant.
val scale : x:int * y:int * z:int * c:int -> int * int * int

///Takes a color constant and converts color to grey, the input (r,g,b).
val grey : grey1:int * grey2:int * grey3:int  -> int * int * int 






