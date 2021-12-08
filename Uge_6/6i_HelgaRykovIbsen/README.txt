1. I started out  by writing my functions in the implementation document - continuedFraction.fs.

2. I created a signature document - continuedFraction.fsi, containing the description of :1) the summary, 2) functions, 3) parameters, 4) the type of input and the type of the  output (e.g. integers or floats).

3. I compiled both the .fsi and .fs files and translated them into a library. I did that by running the command on the console:
fsharpc -a continuedFraction.fsi continuedFraction.fs

4. I created the application file - continuedFractionTest.fsx - containing my Black-box and White-box tests for the tasks 6i0 & 6i1.

5. I linked the newly created library file - continuedFraction.dll - to my application document - continuedFractionTest.fsx. I did that by running the command on the console:
fsharpc -r continuedFraction.dll continuedFractionTest.fsx

6. I got the output on the console by running:
mono continuedFractionTest.exe