1. I started out by writing the interface file for the library Rotate in the file <rotate.fsi> that contains two user-defined types Board and Position and several functions as well as their description in terms of :1) the summary, 2) parameters, 4) the type of the input and output.

2. I wrote a program <blackboxtest.fsx> to perform a blackbox test on the functions of the Rotate library. That was done by switching between running the blackboxtests and writing the respective functions in the implementation file <rotate.fs>. That, in turn, would not be possible without having complied the signature file and its implementation file and having translated 
 them into a library Rotate by running the command on the console:
fsharpc -a rotate.fsi rotate.fs

3. The application file for the blackboxtests as well as the application file for the whitebox test <whiteboxtest.fsx> were linked to the library file <rotate.dll> via:
fsharpc -r rotate.dll blackboxtest.fsx 
fsharpc -r rotate.dll whiteboxtest.fsx

4. I got the output on the console by running:
mono blackboxtest.exe
mono whiteboxtest.exe

5. To launch the game Rotate, I created a short program containing the game logic <game.fsx>. 

6. The file <game.fsx> was linked to the library file <rotate.dll> and executed on the console via the following commands: 
crun:
	fsharpc --doc:rotate.xml -a rotate.fsi rotate.fs
	fsharpc -r rotate.dll game.fsx
	mono game.exe

7. Finally, to see whether the game Rotate performs as expected, it was tested on various board size inputs and rotation position inputs directly from the console. 