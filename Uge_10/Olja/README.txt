To compile and execute the program simulate.fs as well as to run the black-box tests testSimulate.fsx, I run the following commands:

    fsharpc --nologo -a simulate.fs 
	fsharpc -r simulate.dll testSimulate.fsx
	mono testSimulate.exe