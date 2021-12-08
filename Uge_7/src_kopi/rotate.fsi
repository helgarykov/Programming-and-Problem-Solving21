/// The Rotate library.
/// The puzzle is represented as a list of characters from 'a' to 'z'.


module Rotate

///<summary>The sum-type Board is defined as a list of characters from 'a' to 'z'.</summary>

type Board = Board of char list  

///<summary> The sum-type Position is defined as an unsigned integer.</summary>

type Position = Position of uint 


/// Additional functions

///<summary> Given a board, returns its length.</summary>
///<param name="Board"> The sum type Board.</param>
///<returns> The length of the list.</returns>
val boardlength : Board -> uint

///<summary> Given a board, return the n value that it was made from.</summary>
///<param name="b"> The sum type Board.</param>
///<returns> An integer.</returns>
val board2n : b:Board -> int  

///<summary> Given a max value, return a random int between 0 and (max - 1).</summary>
///<param name="max"> The int corresponding to the position of the last item in Board.</param>
///<returns> A random integer.</returns>
val random : max:int -> int 

///<summary> Given a list, get a slice of it to a max of the n first items.</summary>
///<param name="list"> A list of type 'a elements.</param>
///<param Name="n"> An integer, corresponding to the length of the sliced list.</param>
///<returns> A list of the length n.</returns>
val slice : list:list<'a> -> n:int -> list<'a>



/// Obligatory functions

///<summary> Given an integer n, returns an n times n board.</summary>
///<param name="n"> The unsigned integer.</param>
///<returns> The board based on a slice of the alphabet, n times n in size.</returns>
val create : n:uint -> Board

///<summary> Given a board, returns a string of characters.</summary>
///<param name="b"> The sum type Board.</param>
///<returns>The string of characters.</returns>
val board2Str : b:Board -> string

///<summary> Given a board and a rotation position, returns true if the position is valid and false if the position is invalid.</summary>
///<param name="b"> The sum type Board.</param>
///<param name="p"> The sum type Position.</param>
///<returns> A boolian.</returns>
val validRotation : b:Board -> p:Position -> bool

///<summary> Given a board and and a rotation position, returns a new board.</summary>
///<param name="b"> The sum type Board.</param>
///<param name="p"> The sum type Position.</param>
///<returns> Board in its formated or original state.</returns>
val rotate : b:Board -> p:Position -> Board

///<summary>Given a board, returns a new board after having rotated all the elements m times by using the function rotate.</summary>
///<param name="b"> The sum type Board.</param>
///<param name="m"> The unsigned integer.<param> 
///<returns> Board in its formatted state.</returns>
val scramble : b:Board -> m:uint -> Board

///<summary> Given a board, returns true if its configuration is solved and false if not.</summary>
///<param name="b"> The sum type Board.</param>
///<returns> A boolian.</returns>
val solved : b:Board -> bool




