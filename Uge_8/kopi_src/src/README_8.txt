 I started out by transferring the .fs and .fsi files of the mini version of the ImgUtil library that contain the descriptions of all the types and additional functions necessary for the task at hand.

2. I wrote a program <8i0.fsx> that contains the implementation and application of the functions for tasks 8i0 - 8i5. 

3. The signature file <img_util.fsi> and its implementation file <img_util.fs> have been translated into a library <img_util.dll> by running the command on the console:
fsharpc -a img_util.fsi img_util.fs

4. The application file <8i0.fsx> was linked to the library file <img_util.dll> via:
fsharpc -r img_util.dll 8i0.fsx 

5. I got the output on the console by running:
mono 8i0.exe
