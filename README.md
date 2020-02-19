# Simple-Compiler
Basic Compiler 
## Description
Compiler based on recieving code files and execute tokenizing to code lines according to Jack language, supporting SyntaxErrorException meaning recognize compilation errors.
Atfer tokeinzing phase, the Parser recives tokens stack and return parse tree. This Parser does not support arrays definitions. Notic that this compiler uses prefix for expressions for example instead of writing "3+x" we will write "+3x".

## Uses
Examples are provided to check the compilations errors but not all of them, you can make your own compilation errors files and check wether the Parser is finding the errors and throws the right syntax error.

## Credits
Prof. Guy Shani developed the main sheild of the code. Written and solved by Daniel Ben Simon, 4th year student for Software and Information Systems Engineering, Ben Gurion University of the Negev, Israel.
